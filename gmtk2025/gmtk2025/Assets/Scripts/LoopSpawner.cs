using UnityEngine;

public class LoopSpawner : MonoBehaviour
{
    public static DraggableLoop Instance;
    public GameObject loopPrefab;
    public Vector2 screenPositionPercent = new Vector2(0.1f, 0.5f);

    [Tooltip("Assign your LoopSettings ScriptableObject here")]
    public LoopSettings loopSettings;

    void Start()
    {
        Vector3 spawnPos = GetFixedWorldPosition(screenPositionPercent);

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; // flatten to horizontal plane
        cameraForward.Normalize();

        Quaternion initialRotation;

        if (cameraForward.sqrMagnitude > 0.001f)
        {
            initialRotation = Quaternion.LookRotation(cameraForward, Vector3.up);
        }
        else
        {
            initialRotation = Quaternion.identity;
        }

        GameObject loopInstance = Instantiate(loopPrefab, spawnPos, initialRotation);

        DraggableLoop draggableLoop = loopInstance.GetComponent<DraggableLoop>();

        if (loopSettings != null)
        {
            draggableLoop.Initialize(loopSettings);
        }

        Instance = draggableLoop;
    }

    Vector3 GetFixedWorldPosition(Vector2 percent)
    {
        Vector3 screenPos = new Vector3(
            Screen.width * percent.x,
            Screen.height * percent.y,
            10f
        );

        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}