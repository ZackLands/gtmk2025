using UnityEngine;

[CreateAssetMenu(fileName = "LoopSettings", menuName = "Settings/LoopSettings")]
public class LoopSettings : ScriptableObject
{
    [Header("Drag Settings")]
    public float dragSensitivity = 10f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 250f;          // degrees per second
    public float rotationSmoothTime = 0.05f;    // smoothing time for rotation
}
