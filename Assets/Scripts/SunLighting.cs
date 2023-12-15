using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SunLighting : MonoBehaviour
{
    [SerializeField]
    private Light DirectionalLight;
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    private float lastUpdateTime = -1;
    [SerializeField]
    private Gradient AmbientColor;
    [SerializeField]
    private Gradient DirectionalColor;
    [SerializeField]
    private Gradient FogColor;
    [SerializeField]
    private float TimeSpeed = 1.0f;

    private void Awake()
    {
        TimeOfDay += Time.deltaTime;
        TimeOfDay %= 24;
    }

    private void OnValidate()
    {
        if (DirectionalLight == null)
        {
            if (RenderSettings.sun != null)
            {
                DirectionalLight = RenderSettings.sun;
            }
            else
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        DirectionalLight = light;
                        break;
                    }
                }
            }
        }
    }

    private void RenderLight(float timePercent)
    {
        RenderSettings.ambientLight = AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = FogColor.Evaluate(timePercent);
        if (DirectionalLight != null)
        {
            DirectionalLight.color = DirectionalColor.Evaluate(timePercent);
            //DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -180f, 0));
            gameObject.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -180f, 0));
            
            float radius = 1000.0f; // The radius of the sun's rotation around the center point
            Vector3 centerPoint = new Vector3(500, 0, 500); // center pos
            float angle = timePercent * 360f; // angle per day
            Vector3 sunPosition = centerPoint + new Vector3(0,Mathf.Sin(Mathf.Deg2Rad * angle) * radius, Mathf.Cos(Mathf.Deg2Rad * angle) * radius);

            DirectionalLight.transform.position = sunPosition;
            gameObject.transform.position = sunPosition;
            DirectionalLight.transform.LookAt(centerPoint); // Let the sun always face the center point

        }
    }

    private void Update()
    {
        // Custom time control, independent of real time
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime * TimeSpeed; // Modify TimeSpeed value to adjust the speed of time
            TimeOfDay %= 24;
        }

        float timePercent = TimeOfDay / 24f;

            RenderLight(timePercent);
            lastUpdateTime = timePercent;
    }
}
