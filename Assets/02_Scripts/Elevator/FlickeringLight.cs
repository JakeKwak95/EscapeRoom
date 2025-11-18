using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    Light flickerLight;
    float startIntensity;
    [SerializeField] float flickerSpeed = 10f;
    [SerializeField] float flickerAmount = 0.5f;
    float timer;


    void Start()
    {
        flickerLight = GetComponent<Light>();
        startIntensity = flickerLight.intensity;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f / flickerSpeed)
        {
            flickerLight.intensity = startIntensity + (Mathf.PerlinNoise(Time.time * 10, 0f) - .5f) * flickerAmount;

            timer = 0f;
        }

    }
}
