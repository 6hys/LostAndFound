using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private bool isFlickering = false;

    private float timeDelay;

    private Light lightComponent;

    public float timeDelayLow = 0.2f;
    public float timeDelayHigh = 0.6f;

    public float dimIntensityLow = 0.1f;
    public float dimIntensityHigh = 0.4f;
    public float brightIntensityLow = 0.5f;
    public float brightIntensityHigh = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        lightComponent = this.gameObject.GetComponent<Light>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        // Prevent from running twice
        isFlickering = true;

        // Turn off light
        float elapsedTime = 0f;
        float startingIntensity = lightComponent.intensity;
        float newIntensity = Random.Range(dimIntensityLow, dimIntensityHigh);

        // Lerp over values
        timeDelay = Random.Range(timeDelayLow, timeDelayHigh);

        // Coroutine lerp info from here: https://answers.unity.com/questions/1502190/help-using-lerp-inside-of-a-coroutine.html

        while (elapsedTime < timeDelay)
        {
            lightComponent.intensity = Mathf.Lerp(startingIntensity, newIntensity, elapsedTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        lightComponent.intensity = newIntensity;

        // Turn on light
        startingIntensity = newIntensity;
        newIntensity = Random.Range(brightIntensityLow, brightIntensityHigh);

        // Lerp over new values
        timeDelay = Random.Range(timeDelayLow, timeDelayHigh);

        while (elapsedTime < timeDelay)
        {
            lightComponent.intensity = Mathf.Lerp(startingIntensity, newIntensity, elapsedTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        lightComponent.intensity = newIntensity;

        isFlickering = false;
    }
}
