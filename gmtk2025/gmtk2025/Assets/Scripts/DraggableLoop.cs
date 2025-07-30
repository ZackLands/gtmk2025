using UnityEngine;

public class DraggableLoop : MonoBehaviour
{
    private float dragSensitivity;
    private float rotationSpeed;
    private float rotationSmoothTime;

    private bool isDragging = false;
    private Vector3 offset;
    private float zCoord;

    private float pitch = 0f;
    private float yaw = 0f;

    private float pitchVelocity = 0f;
    private float yawVelocity = 0f;

    // Called once to initialize with settings
    public void Initialize(LoopSettings settings)
    {
        dragSensitivity = settings.dragSensitivity;
        rotationSpeed = settings.rotationSpeed;
        rotationSmoothTime = settings.rotationSmoothTime;
    }

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        pitch = angles.x;
        yaw = angles.y;
    }

    void OnMouseDown()
    {
        isDragging = true;
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        HandleDrag();
        HandleRotation();
    }

    void HandleDrag()
    {
        if (isDragging)
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = zCoord;

            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePoint) + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * dragSensitivity);
            transform.position = smoothedPos;
        }
    }

    void HandleRotation()
    {
        float targetPitch = pitch;
        float targetYaw = yaw;

        if (Input.GetKey(KeyCode.W))
            targetPitch -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            targetPitch += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            targetYaw -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            targetYaw += rotationSpeed * Time.deltaTime;

        targetPitch = Mathf.Clamp(targetPitch, -80f, 80f);

        pitch = Mathf.SmoothDamp(pitch, targetPitch, ref pitchVelocity, rotationSmoothTime);
        yaw = Mathf.SmoothDamp(yaw, targetYaw, ref yawVelocity, rotationSmoothTime);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
