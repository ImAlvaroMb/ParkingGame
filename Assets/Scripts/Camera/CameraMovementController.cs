using Unity.Cinemachine;
using UnityEngine;
using Utilities;
using Enums;
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
    private float _currentTilt = 0f;

    [Header("FocusingElementsSettings")]
    public Transform TargetTesting1;
    public Transform TargetTesting2;
    public CinemachineCamera TargetCamera;
    public Transform CameraTarget { get => cameraTarget; }
    private Transform cameraTarget;

    public GameObject TemporalAnim;
    private bool _hastTarget = false;
    private Vector3 _lastCameraPosition;
    private Quaternion _lastCameraRotation;

    private void Start()
    {
        _currentTilt = transform.rotation.x;
    }

    private void Update()
    {
        if(!_hastTarget)
        {
            HandleMovement();
            HandleCameraHeight();
            HandleRotation();
        }
    }

    #region Camera Basic Movement

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
            _currentTilt -= mouseY * rotationSpeed * Time.deltaTime;

            _currentTilt = Mathf.Clamp(_currentTilt, minTilt, maxTilt);

            transform.localEulerAngles = new Vector3(_currentTilt, transform.localEulerAngles.y, 0f);
        }
    }

    public void StartTransitionBetweenLevels()
    {
        TemporalAnim.SetActive(true);
        Invoke("StopTransitionBetweenLevels", 0.75f);
    }

    public void StopTransitionBetweenLevels()
    {
        TemporalAnim.SetActive(false);
    }

    public void MoveCameraTo(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    #endregion


    public void ChangeTarget(Transform newTarget)
    {
        if (newTarget == null) return;

        _lastCameraPosition = transform.position;
        _lastCameraRotation = transform.rotation;

        cameraTarget = newTarget;
        _hastTarget = cameraTarget;
        TargetCamera.Follow = cameraTarget;
        TargetCamera.LookAt = cameraTarget;
        TargetCamera.gameObject.SetActive(true);
    }
    [ContextMenu("StopTarget")]
    public void StopTarget()
    {
        cameraTarget = null;
        _hastTarget = false;
        TargetCamera.gameObject.SetActive(false);
        TargetCamera.Follow = null;
        TargetCamera.LookAt = null;
        transform.position = _lastCameraPosition;
        transform.rotation = _lastCameraRotation;
    }

    [ContextMenu("Target1")]
    public void Test1()
    {
        ChangeTarget(TargetTesting1);
    }

    [ContextMenu("Target2")]
    public void Test2()
    {
        ChangeTarget(TargetTesting2);
    }

}
