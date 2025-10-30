using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Scriptable Objects/MovementData")]
public class MovementData : ScriptableObject
{
    [Header("Grounded Movement")]
    public float moveSpeed = 6f;
    public float walkSpeed = 6f;
    public float sprintSpeed = 10f;
    public float airControl = 4f;
    public float rotationSpeed = 4f;
    public float maxSlopeAngle = 60f;
    public float wallAngle = 80f;
}   
