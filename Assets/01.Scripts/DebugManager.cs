using System;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager instance;
    
    [Header("Time Scaler")]
    [SerializeField, Range(0f, 1f)] private float timeScale = 1f;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    private void OnValidate()
    {
        Time.timeScale = timeScale;
    }
}
