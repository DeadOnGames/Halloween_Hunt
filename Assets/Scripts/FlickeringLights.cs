using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlickeringLights : MonoBehaviour
{
    public float MaxReduction;
     public float MaxIncrease;
     public float RateDamping;
     public float Strength;
     public bool StopFlickering;
    public Light2D light;
    private float _baseIntensity;
     private bool _flickering;

    public void Reset()
        {
            MaxReduction = 0.2f;
            MaxIncrease = 0.2f;
            RateDamping = 0.1f;
            Strength = 300;
        }
        
    void Start()
    {
        Reset();
        if (light == null)
         {
             Debug.LogError("Flicker script must have a Light Component on the same GameObject.");
             return;
         }
         _baseIntensity = light.intensity;
         StartCoroutine(DoFlicker());
    }

    // Update is called once per frame
    void Update()
    {
        if (!StopFlickering && !_flickering)
            {
                StartCoroutine(DoFlicker());
            }
    }

    private IEnumerator DoFlicker()
     {
         _flickering = true;
         while (!StopFlickering)
         {
             light.intensity = Mathf.Lerp(light.intensity, Random.Range(_baseIntensity - MaxReduction, _baseIntensity + MaxIncrease), Strength * Time.deltaTime);
             yield return new WaitForSeconds(RateDamping);
         }
         _flickering = false;
     }
}
