using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    private Image buttonImage;               
    public float blinkSpeed = 1f;            
    public Color highlightColor = Color.yellow; 
    public float scaleSpeed = 1f;               
    public float scaleAmount = 1.1f;            

    private Color originalColor;
    private Vector3 originalScale;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
        originalScale = transform.localScale;

        StartCoroutine(BlinkAndPulse());
    }

    private System.Collections.IEnumerator BlinkAndPulse()
    {
        while (true)
        {
            // Step 1: Highlight color and scale up
            buttonImage.color = highlightColor;
            yield return StartCoroutine(ScaleTo(originalScale * scaleAmount));

            // Step 2: Original color and scale down
            buttonImage.color = originalColor;
            yield return StartCoroutine(ScaleTo(originalScale));
        }
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 targetScale)
    {
        float progress = 0f;
        Vector3 initialScale = transform.localScale;

        while (progress < 1f)
        {
            progress += Time.deltaTime * scaleSpeed;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
            yield return null;
        }
    }
}
