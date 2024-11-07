using UnityEngine;

[CreateAssetMenu(fileName = "DayNightConfig", menuName = "Config/DayNight")]
public class DayNightConfig : ScriptableObject
{
    public float lightIntensity;
    public Color zenithColor;
    public Color horizonColor;
}