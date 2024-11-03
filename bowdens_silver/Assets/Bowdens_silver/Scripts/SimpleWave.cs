using UnityEngine;

public class SimpleWave : MonoBehaviour
{
    public float waveSpeed = 1f;
    public float waveHeight = 0.5f;

    private Vector3[] baseVertices;

    private void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
    }

    private void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = new Vector3[baseVertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];
            vertex.y += Mathf.Sin(Time.time * waveSpeed + vertex.x + vertex.z) * waveHeight;
            vertices[i] = vertex;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
