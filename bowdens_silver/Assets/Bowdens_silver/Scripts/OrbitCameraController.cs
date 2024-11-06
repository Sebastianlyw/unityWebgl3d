using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class OrbitalCameraController : MonoBehaviour
{
    public Transform cameraPivot;
    public OrbitCameraConfig config;
    private float x = 0.0f;
    private float y = 0.0f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        config.distance = Vector3.Distance(cam.transform.position, cameraPivot.position);
        Vector3 angles = cam.transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }


    private void OnConfigLoaded(AsyncOperationHandle<OrbitCameraConfig> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            config = handle.Result;
            
        }
        else
        {
            Debug.LogError("Failed to load OrbitCameraConfig");
        }
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
            x += Input.GetAxis("Mouse X") * config.xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * config.ySpeed * 0.02f;

            // Clamp the y angle to be between minYAngle and maxYAngle
            y = Mathf.Clamp(y, config.minYAngle, config.maxYAngle);
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -config.distance) + cameraPivot.position;

        cam.transform.rotation = rotation;
        cam.transform.position = position;

        if (Input.GetMouseButton(1)) // Right Click + Drag for panning
        {
            float panX = Input.GetAxis("Mouse X") * -config.panSpeed;
            float panY = Input.GetAxis("Mouse Y") * -config.panSpeed;

            Vector3 panDirection = cam.transform.right * panX + cam.transform.up * panY;
            cameraPivot.position += panDirection;
           
            if (cameraPivot.position.y < config.minYPanning)
            {
                cameraPivot.position = new Vector3(cameraPivot.position.x, config.minYPanning, cameraPivot.position.z);
            }
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        config.distance -= scroll * config.zoomSpeed;

        // Clamp zoom distance
        config.distance = Mathf.Clamp(config.distance, config.minZoom, config.maxZoom);

        // Update camera position based on distance
        Vector3 direction = cam.transform.position - cameraPivot.position;
        cam.transform.position = cameraPivot.position + direction.normalized * config.distance;
    }
}