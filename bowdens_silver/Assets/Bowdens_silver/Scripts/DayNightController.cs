using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DayNightController : MonoBehaviour
{
    public Light directionalLight;
    public Material skyboxMaterial;
    public DayNightConfig dayConfig;
    public DayNightConfig nightConfig;
    public GlobalConfig globalConfig;
    public Button dayNightButton;

    public void ApplyDayNightConfig()
    {
      
        DayNightConfig conifg = globalConfig.isDay ? dayConfig : nightConfig;
            
        
        if (directionalLight != null)
        {
            directionalLight.intensity = conifg.lightIntensity;
        }

        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetColor("_ZenithColor", conifg.zenithColor);
            skyboxMaterial.SetColor("_HorizonColor", conifg.horizonColor);
        }

        globalConfig.isDay = !globalConfig.isDay;
        dayNightButton.GetComponentInChildren<TextMeshProUGUI>().text = globalConfig.isDay ? "Day" : "Night";
    }
}