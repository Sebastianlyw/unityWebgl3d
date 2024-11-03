using UnityEngine;
using System.Text;
using TMPro;

public class StatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI statsText; 
    
    public float updateInterval = 0.5f; 
    private float deltaTime = 0.0f;
    private float fps;
    private int drawCalls;
    private float lastUpdate = 0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;

        if (Time.time - lastUpdate > updateInterval)
        {
            lastUpdate = Time.time;

            #if UNITY_EDITOR
            drawCalls = UnityEditor.UnityStats.drawCalls;
            #endif
            float memoryUsage = (System.GC.GetTotalMemory(false) / 1024f) / 1024f; 

            // Display stats
            UpdateStatsText(fps, drawCalls, memoryUsage);
        }
    }

    private void UpdateStatsText(float fps, int drawCalls, float memoryUsage)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"FPS: {fps:F1}");
        //sb.AppendLine($"Draw Calls: {drawCalls}");
        sb.AppendLine($"Memory Usage: {memoryUsage:F2} MB");

        statsText.text = sb.ToString();
    }
}
