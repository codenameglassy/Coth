using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugFrameRate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI frameRateText;

    private float deltaTime = 0.0f;

    private void Awake()
    {
        // Set target frame rate to 60
        Application.targetFrameRate = 60;

        // Disable VSync
        QualitySettings.vSyncCount = 0;
    }

    void Update()
    {
        // Calculate the frame time
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        // Update the text to display FPS
        if (frameRateText != null)
        {
            frameRateText.text = $"FPS: {Mathf.Ceil(fps)}";
        }
    }
}
