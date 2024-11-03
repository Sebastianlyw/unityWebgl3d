using System;
using UnityEngine;

public class OrbitalCameraController : MonoBehaviour
{
    public Transform cameraPivot;
    public float panSpeed = 50f;
    public float distance = 100.0f; 
    public float xSpeed = 120.0f; 
    public float ySpeed = 120.0f;
    public float zoomSpeed = 50.0f;
    public float minZoom = 5.0f;
    public float maxZoom = 5000.0f;
    private float x = 0.0f;
    private float y = 0.0f;

    private Camera cam;
    
    private Vector3 lastMousePosition;


    void Start()
    {
        cam = Camera.main;
        distance = Vector3.Distance(cam.transform.position, cameraPivot.position);
        Vector3 angles = cam.transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        HandleMouseInput();
        HandleZoom();
    }

    private void HandleMouseInput()
    {
        if (cameraPivot == null)
        {
            return;
        }

        if (Input.GetMouseButton(0)) // Left Click + Drag for rotation
        {

            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            // Clamp the y angle to be between 10 and 90 degrees
            y = Mathf.Clamp(y, 10.0f, 90.0f);
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + cameraPivot.position;

        cam.transform.rotation = rotation;
        cam.transform.position = position;

        if (Input.GetMouseButton(1)) // Right Click + Drag for panning
        {
            float panX = Input.GetAxis("Mouse X") * -panSpeed;
            float panY = Input.GetAxis("Mouse Y") * -panSpeed;

            Vector3 panDirection = cam.transform.right * panX + cam.transform.up * panY;
            cameraPivot.position += panDirection;
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;

        // Clamp zoom distance
        distance = Mathf.Clamp(distance, minZoom, maxZoom);

        // Update camera position based on distance
        Vector3 direction = cam.transform.position - cameraPivot.position;
        cam.transform.position = cameraPivot.position + direction.normalized * distance;
    }
}