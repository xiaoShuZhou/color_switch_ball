using UnityEngine;

public class ScalePulse : MonoBehaviour
{
    public float scaleMultiplier = 1.2f;  // Scale multiplier for the maximum size
    public float duration = 1f;           // Duration of one scale cycle (up or down)
    private Vector3 originalScale;        // Original scale of the object
    private Vector3 targetScale;          // Target scale when fully enlarged
    private bool isScalingUp = true;      // Flag to track if currently scaling up or down
    private float elapsedTime = 0f;       // Time elapsed in the current scale cycle

    void Start()
    {
        // Record the initial scale
        originalScale = transform.localScale;
        targetScale = originalScale * scaleMultiplier;  // Calculate the target scale
    }

    void Update()
    {
        if (isScalingUp)
        {
            // Currently scaling up from original to target scale
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);

            // If reached the target scale, start scaling down
            if (t >= 1f)
            {
                isScalingUp = false;
                elapsedTime = 0f;  // Reset elapsed time
            }
        }
        else
        {
            // Currently scaling down from target to original scale
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t);

            // If returned to the original scale, start scaling up again
            if (t >= 1f)
            {
                isScalingUp = true;
                elapsedTime = 0f;  // Reset elapsed time
            }
        }
    }
}