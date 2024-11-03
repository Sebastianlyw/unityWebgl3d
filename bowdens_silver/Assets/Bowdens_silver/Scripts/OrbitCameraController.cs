using System;
using UnityEngine;

public class OrbitalCameraController : MonoBehaviour
{
    public Transform cameraPivot; // Assign the CameraPivot in the Inspector
    public float rotationSpeed = 5.0f;
    public float panSpeed = 50f;
    public float zoomSpeed = 500.0f;
    public float minZoom = 5.0f;
    public float maxZoom = 5000.0f;

    private Camera cam;
    private float distanceToPivot;
    private Vector3 lastMousePosition;

    void Start()
    {
        cam = Camera.main;
        Console.WriteLine("Camera: " + cam);
        distanceToPivot = Vector3.Distance(cam.transform.position, cameraPivot.position);
    }

    void Update()
    {
        HandleMouseInput();
        HandleZoom();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(0)) // Left Click + Drag for rotation
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed;

            cameraPivot.Rotate(Vector3.up, rotationX, Space.World);
            cameraPivot.Rotate(Vector3.left, rotationY);
        }

        if (Input.GetMouseButton(1)) // Right Click + Drag for panning
        {
            float panX = Input.GetAxis("Mouse X") * panSpeed;
            float panY = Input.GetAxis("Mouse Y") * panSpeed;

            cameraPivot.position += new Vector3(panX, 0, panY);
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distanceToPivot -= scroll * zoomSpeed;

        // Clamp zoom distance
        distanceToPivot = Mathf.Clamp(distanceToPivot, minZoom, maxZoom);

        // Update camera position based on distance
        Vector3 direction = cam.transform.position - cameraPivot.position;
        cam.transform.position = cameraPivot.position + direction.normalized * distanceToPivot;
    }
}
