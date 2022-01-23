using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interaction
{
    public List<GameObject> lamps;
    public GameObject switchObject;
    public List<Light> lampLights;
    public int materialIndex;
    public List<Material> lampMats;
    private Color emissionColor;

    bool isOn = false;
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject lamp in lamps)
        {
            lampMats.Add(lamp.GetComponent<Renderer>().materials[materialIndex]);
        }

        emissionColor = new Color(255, 233, 151); //lampMat.GetColor("_EmissionColor");

        initialRotation = switchObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("Light interaction");

        if(isOn)
        {
            // Flip switch
            switchObject.transform.rotation = initialRotation;

            // Turn light off
            for(int i = 0; i < lampLights.Count; i++)
            {
                lampLights[i].enabled = false;
                lampMats[i].DisableKeyword("_EMISSION");
            }

            isOn = false;
        }
        else
        {
            // Flip switch
            // https://forum.unity.com/threads/rotate-a-quaternion-20-degrees-around-y-axis.90990/
            Quaternion newRotation = initialRotation * Quaternion.Euler(Vector3.up * 180f);
            switchObject.transform.rotation = newRotation;

            // Turn light on
            for (int i = 0; i < lampLights.Count; i++)
            {
                lampLights[i].enabled = true;
                lampMats[i].EnableKeyword("_EMISSION");
            }

            isOn = true;
        }
    }
}
