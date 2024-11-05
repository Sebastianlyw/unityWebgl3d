using UnityEngine;

[CreateAssetMenu(fileName = "TreeSpawnerConfig", menuName = "Config/TreeSpawnerConfig")]
public class TreeSpawnerConfig : ScriptableObject
{
    public int density;
    public float radius;
    public float minScale = 2f;
    public float maxScale = 5f;
    public float maxYRotation = 360f;
    public float cullingDistance = 1000f;
}
