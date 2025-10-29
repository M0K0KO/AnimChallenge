using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FootIKController : MonoBehaviour
{
    [Header("IK Targets")]
    [SerializeField] private Transform leftFootTarget;
    [SerializeField] private Transform rightFootTarget;
    [SerializeField] private Transform leftFootballTarget;
    [SerializeField] private Transform rightFootballTarget;
    
    [Header("IK Constraints")]
    [SerializeField] private TwoBoneIKConstraint leftFootIKConstraint;
    [SerializeField] private TwoBoneIKConstraint rightFootIKConstraint;
    [SerializeField] private OverrideTransform leftFootballIKConstraint;
    [SerializeField] private OverrideTransform rightFootballIKConstraint;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private float footOffset = 0.03f;

    [Header("IK Smoothing")]
    [SerializeField] private float ikTargetPositionSmoothTime = 0.1f;
    private Vector3 leftFootVelocity;
    private Vector3 rightFootVelocity;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        float leftFootWeight = animator.GetFloat("LeftFoot_IK_Weight");
        float rightFootWeight = animator.GetFloat("RightFoot_IK_Weight");
        
        leftFootIKConstraint.weight = leftFootWeight;
        rightFootIKConstraint.weight = rightFootWeight;
        leftFootballIKConstraint.weight = leftFootWeight;
        rightFootballIKConstraint.weight = rightFootWeight;
        
        Transform leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        HandleFootIK(leftFoot, leftFootTarget, leftFootWeight, ref leftFootVelocity);
        
        Transform rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        HandleFootIK(rightFoot, rightFootTarget, rightFootWeight, ref rightFootVelocity);

        //Transform leftFootBall = animator.GetBoneTransform(HumanBodyBones.LeftToes);
        //HandleFootBallIK(leftFootBall, leftFootballTarget, leftFootWeight);
        
        //Transform rightFootBall = animator.GetBoneTransform(HumanBodyBones.RightToes);
        //HandleFootBallIK(rightFootBall, rightFootballTarget, rightFootWeight);
    }

    private void HandleFootIK(Transform footTransform, Transform ikTarget, float ikWeight, ref Vector3 footVelocity)
    {
        Vector3 animatedFootPosition = footTransform.position;
        Quaternion animatedFootRotation = footTransform.rotation;
        
        Vector3 targetPosition;
        Quaternion targetRotation;

        if (Physics.Raycast(animatedFootPosition + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, raycastDistance,
                groundLayer))
        {
            Vector3 groundTargetPosition = hit.point + hit.normal * footOffset;
            targetPosition = groundTargetPosition;
            targetPosition.y = Mathf.Lerp(animatedFootPosition.y, groundTargetPosition.y, ikWeight);

            Quaternion groundTargetRotation =
                Quaternion.LookRotation(Vector3.ProjectOnPlane(footTransform.forward, hit.normal), hit.normal);
            targetRotation = Quaternion.Slerp(animatedFootRotation, groundTargetRotation, ikWeight);
        }
        else
        {
            targetPosition = animatedFootPosition;
            targetRotation = animatedFootRotation;
        }

        ikTarget.position = Vector3.SmoothDamp(
            ikTarget.position,
            targetPosition,
            ref footVelocity,
            ikTargetPositionSmoothTime);
        ikTarget.rotation = targetRotation;
    }

    private void HandleFootBallIK(Transform footballTransform, Transform ikTarget, float ikWeight)
    {
        Vector3 animatedFootballPosition = footballTransform.position;
        Quaternion animatedFootballRotation = footballTransform.rotation;
        Quaternion targetRotation;
        
        if (Physics.Raycast(animatedFootballPosition + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, raycastDistance,
                groundLayer))
        {
            Quaternion groundTargetRotation =
                Quaternion.LookRotation(Vector3.ProjectOnPlane(footballTransform.forward, hit.normal), hit.normal);
            targetRotation = Quaternion.Slerp(animatedFootballRotation, groundTargetRotation, ikWeight);
        }
        else
        {
            targetRotation = animatedFootballRotation;
        }
        
        ikTarget.rotation = targetRotation;
    }
}
