using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;  // Reference to TMP text component
    private float deltaTime = 0.0f;  // Time between frames
    private int frameCount = 0;
    private float timeSinceLastUpdate = 0.0f;

    void Update()
    {
        // Count frames per second
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        frameCount++;

        // Update FPS every 1 second
        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate >= 1.0f)
        {
            // Calculate FPS
            float fps = frameCount / timeSinceLastUpdate;
            fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();  // Update TMP text with the FPS value

            // Reset counters for next update
            timeSinceLastUpdate = 0.0f;
            frameCount = 0;
        }
    }
}