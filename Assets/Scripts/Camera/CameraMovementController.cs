using Unity.Cinemachine;
using UnityEngine;

// IMPORTANT THE CURRENT PROJECT INPUT SETTINGS ARE SET TO BOTH, WE SHOULD CHANGE THEM BACK TO EITHER THE NEW OR OLD ONE
public class CameraMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float rotationSpeed = 120f;

    [Header("Zoom Settings")]
    public CinemachineCamera CinemachineCamera;
    [SerializeField] private float minHeight = 5f;
    [SerializeField] private float maxHeight = 50f;
    [SerializeField] private float zoomSpeed = 200f;

    private void Update()
    {
        HandleMovement();
        HandleHeightZoom();
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

    private void HandleHeightZoom() // maybe we can add a camera zoom (change the fov) when really close to the floor
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            float newY = transform.position.y - (scroll * zoomSpeed * Time.deltaTime);
            newY = Mathf.Clamp(newY, minHeight, maxHeight);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime, Space.World);
        }
    }

}
