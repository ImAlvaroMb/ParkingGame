using Unity.Cinemachine;
using UnityEngine;

// IMPORTANT THE CURRENT PROJECT INPUT SETTINGS ARE SET TO BOTH, WE SHOULD CHANGE THEM BACK TO EITHER THE NEW OR OLD ONE
public class CameraMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float rotationSpeed = 120f;

    [Header("Height/Zoom Settings")]
    public CinemachineCamera CinemachineCamera;
    public bool ChangesHeight = true;
    [SerializeField] private float minHeight = 5f;
    [SerializeField] private float maxHeight = 50f;
    [SerializeField] private float maxFOV = 75f;
    [SerializeField] private float minFOV = 5f;
    [SerializeField] private float verticalSpeed = 200f;

    [Header("Tilt Settings")]
    [SerializeField] private float minTilt = 15f;
    [SerializeField] private float maxTilt = 70f;
    private float currentTilt = 0f;

    [Header("FocusingElementsSettings")]
    public Transform CameraTarget { get => cameraTarget; }
    private Transform cameraTarget;


    private void Start()
    {
        currentTilt = transform.rotation.x;
    }

    private void Update()
    {
        HandleMovement();
        HandleCameraHeight();
        HandleRotation();
    }

    private void HandleMovement() // will either recieve float values from input controller or be locally subscribed to it
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * z + right * x).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraHeight() 
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            if(ChangesHeight)
            {
                float newY = transform.position.y - (scroll * verticalSpeed * Time.deltaTime);
                newY = Mathf.Clamp(newY, minHeight, maxHeight);

                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            } else
            {
                float currentFOV = CinemachineCamera.Lens.FieldOfView;
                float newFOV = currentFOV - (scroll * verticalSpeed * Time.deltaTime);

                newFOV = Mathf.Clamp(newFOV, minFOV, maxFOV);
                CinemachineCamera.Lens.FieldOfView = newFOV;
            }
         
        }
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime, Space.World);

            float mouseY = Input.GetAxis("Mouse Y");
            currentTilt -= mouseY * rotationSpeed * Time.deltaTime;

            currentTilt = Mathf.Clamp(currentTilt, minTilt, maxTilt);

            transform.localEulerAngles = new Vector3(currentTilt, transform.localEulerAngles.y, 0f);
        }
    }

}
