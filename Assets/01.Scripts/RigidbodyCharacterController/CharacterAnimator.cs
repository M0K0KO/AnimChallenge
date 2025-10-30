using System;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private CharacterMotor motor;
    private Animator animator;

    private void Awake()
    {
        motor = GetComponent<CharacterMotor>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimatorParam_Bool("isMoving", motor._playerInput.MoveInput != Vector2.zero);
        UpdateAnimatorParam_Bool("sprintInput", motor._playerInput.SprintInput);
    }

    private void UpdateAnimatorParam_Float(string paramName, float value)
    {
        animator.SetFloat(paramName, value);
    }

    private void UpdateAnimatorParam_Bool(string paramName, bool value)
    {
        animator.SetBool(paramName, value);
    }
}
