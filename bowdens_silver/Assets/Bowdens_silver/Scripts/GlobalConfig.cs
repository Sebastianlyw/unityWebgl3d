using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "Config/Global")]
public class GlobalConfig : ScriptableObject
{
    public bool isPreMining = true;
    public bool isDay = true;
}
