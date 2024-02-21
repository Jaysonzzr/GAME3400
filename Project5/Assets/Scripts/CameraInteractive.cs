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
    public AudioClip lightSFX;

    [Header("Flash Light Text")]
    public Text textToFade;
    public Image backgroundToFade;
    public float fadeInDuration = 1.5f;
    public float fadeOutDuration = 1.0f;

    private bool isFadingIn = false;
    private bool isFadingOut = false;

    private int collectCount = 0;

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

                        FadeIn();
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
                collectCount++;

                AudioSource.PlayClipAtPoint(lightSFX, Camera.main.transform.position);

                if (collectCount == 1)
                {
                    FadeOut();
                }

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

    void SetAlpha(float alpha)
    {
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, alpha);
        backgroundToFade.color = new Color(backgroundToFade.color.r, backgroundToFade.color.g, backgroundToFade.color.b, alpha);
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0, 1, fadeInDuration));
    }

    public void FadeOut()
    {
        float currentAlpha = textToFade.color.a;
        StartCoroutine(Fade(currentAlpha, 0, fadeOutDuration));
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            SetAlpha(newAlpha);
            yield return null;
        }
        SetAlpha(endAlpha);
    }
}
