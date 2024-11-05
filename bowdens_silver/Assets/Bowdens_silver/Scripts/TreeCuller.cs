using UnityEngine;

public class TreeCuller : MonoBehaviour
{
    private Transform cameraTransform;
    private float cullingDistance;
    private Renderer treeRenderer;

    public void Initialize(Transform cam, float distance)
    {
        cameraTransform = cam;
        cullingDistance = distance;
        treeRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (treeRenderer != null)
        {
            float distance = Vector3.Distance(transform.position, cameraTransform.position);
            // Enable/disable the renderer based on distance
            treeRenderer.enabled = distance < cullingDistance;
        }
    }
}
