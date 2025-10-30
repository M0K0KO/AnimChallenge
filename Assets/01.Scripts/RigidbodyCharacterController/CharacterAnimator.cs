using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CharacterAnimator : MonoBehaviour
{
    private CharacterMotor motor;

    public Dictionary<int, AnimationCurve> rootPosCurves;
    public List<string> bindingNames;
    
    public Animator animator { get; private set; }
    
    private void Awake()
    {
        motor = GetComponent<CharacterMotor>();
        animator = GetComponent<Animator>();
        
        rootPosCurves = new Dictionary<int, AnimationCurve>();
    }

    private void Start()
    {
        Debug.Log(rootPosCurves.Keys.Count);
        
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            var bindings = AnimationUtility.GetCurveBindings(clip);
            foreach (var binding in bindings)
            {
                if (binding.path == "" && bindingNames.Contains(binding.propertyName))
                {
                    AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
                    rootPosCurves.Add(Animator.StringToHash(clip.name), curve);
                }
            }
        }
        Debug.Log(rootPosCurves.Keys.Count);

    }

    private void OnAnimatorMove()
    {
        return;
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
