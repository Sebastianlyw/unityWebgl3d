using UnityEngine;

[CreateAssetMenu(fileName = "OrbitCameraConfig", menuName = "Config/OrbitCamera")]
public class OrbitCameraConfig : ScriptableObject
{
    public float panSpeed = 50f;
    public float distance = 100.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float zoomSpeed = 50.0f;
    public float minZoom = 5.0f;
    public float maxZoom = 5000.0f;
    public float minYAngle = 10.0f;
    public float maxYAngle = 90.0f;
}