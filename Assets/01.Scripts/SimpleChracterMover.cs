using System;
using UnityEngine;

public class SimpleChracterMover : MonoBehaviour
{
    private CharacterController cc;
    private Animator animator;
    private Camera mainCam;
    
    [SerializeField, Range(1f, 20f)] private float moveSpeed = 5f;
    [SerializeField, Range(1f, 20f)] private float rotationSpeed = 5f;

    [SerializeField, Range(0.01f, 1f)] private float timeScale = 1f;
    
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 moveDir = mainCam.transform.right * horizontal + mainCam.transform.forward * vertical;
        moveDir.y = 0f;
        moveDir.Normalize();
        
        animator.SetBool("isMoving", moveDir != Vector3.zero);
        
        cc.Move(moveDir * (moveSpeed * Time.deltaTime));

        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;   
        }
    }

    private void OnValidate()
    {
        Time.timeScale = timeScale;
    }
}
