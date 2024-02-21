using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteractive : MonoBehaviour
{
    public float maxDistance; // Maximum distance at which object info will be displayed

    public Light spotlight;
    public Light[] lights;

    bool haveSpotLight = false;

    public AudioClip collectSFX;

    // Update is called once per frame
    void Update()
    {
        FlashLight();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            float distance = Vector3.Distance(transform.position, hitObject.transform.position);
            if (distance <= maxDistance)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hitObject.CompareTag("Spotlight"))
                    {
                        hitObject.GetComponent<MeshRenderer>().enabled = false;
                        hitObject.GetComponent<MeshCollider>().enabled = false;
                        AudioSource.PlayClipAtPoint(collectSFX, Camera.main.transform.position);
                        haveSpotLight = true;
                    }

                    if (hitObject.CompareTag("PowerSwitch"))
                    {
                        RenderSettings.fog = false;
                        
                        foreach (Light light in lights)
                        {
                            light.enabled = true;
                        }

                        spotlight.enabled = false;
                        haveSpotLight = false;
                    }
                }
            }
        }
    }

    void FlashLight()
    {
        if (haveSpotLight)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (spotlight.enabled == true)
                {
                    spotlight.enabled = false;
                }
                else
                {
                    spotlight.enabled = true;
                }
            }
        }
    }
}
