using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceDisplay : MonoBehaviour
{
    public Text performanceText;
    ProfilerRecorder systemMemory;
    private float deltaTime = 0.0f;

    void Start()
    {
        systemMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
    }

    // Update is called once per frame
    void Update()
    {
        if (systemMemory.Valid)
        {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        float memory = systemMemory.LastValue / (1024 * 1024 * 8); // get memory useage and transfrom to MB

        performanceText.text = $"FPS: {Mathf.Ceil(fps).ToString()} \nMemory: {memory.ToString("F2")} MB";
        }
    }
}
