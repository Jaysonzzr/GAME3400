using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteractive : MonoBehaviour
{
    public BackgroundSounds backgroundSounds;

    public AudioSource audioSource;

    public float maxDistance; // Maximum distance at which object info will be displayed

    public Light spotlight;
    public Light[] lights;

    bool haveSpotLight = false;

    public AudioClip collectSFX;
    public AudioClip lightSFX;
    public AudioClip buttonClick;

    [Header("Flash Light Text")]
    public Text textToFade;
    public Image backgroundToFade;
    public float fadeInDuration = 1.5f;
    public float fadeOutDuration = 1.0f;

    private int collectCount = 0;

    bool interacted = false;

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
                        hitObject.GetComponent<BoxCollider>().enabled = false;

                        foreach (Transform childTransform in hitObject.transform)
                        {
                            MeshRenderer meshRenderer = childTransform.GetComponent<MeshRenderer>();
                            if (meshRenderer != null)
                            {
                                meshRenderer.enabled = false;
                            }
                        }

                        AudioSource.PlayClipAtPoint(collectSFX, Camera.main.transform.position);
                        haveSpotLight = true;

                        FadeIn();
                    }

                    if (hitObject.CompareTag("PowerSwitch") && !interacted)
                    {
                        AudioSource.PlayClipAtPoint(buttonClick, Camera.main.transform.position);

                        backgroundSounds.StopCrew();

                        hitObject.GetComponent<Outline>().enabled = false;

                        StartCoroutine(FadeVolume(1f, 3f));

                        interacted = true;
                        
                        foreach (Light light in lights)
                        {
                            StartCoroutine(FadeLightOn(light, 2f, 4f));
                        }

                        spotlight.enabled = false;
                        haveSpotLight = false;

                        Vector3 newPosition = new Vector3(0.4609286f, 0.345f, -0.07266025f);
                        StartCoroutine(MoveObjectToPosition(hitObject, newPosition, 1f));
                    }
                }
            }
        }
    }

    IEnumerator MoveObjectToPosition(GameObject obj, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = obj.transform.localPosition;
        float time = 0;

        while (time < duration)
        {
            obj.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        obj.transform.localPosition = targetPosition;
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

    IEnumerator FadeVolume(float targetVolume, float duration)
    {
        float currentTime = 0;
        float startVolume = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    IEnumerator FadeLightOn(Light light, float targetIntensity, float duration)
    {
        yield return new WaitForSeconds(3f);

        float startIntensity = 0f;
        float startFogDensity = RenderSettings.fogDensity;
        float time = 0;

        while (time < duration)
        {
            light.intensity = Mathf.Lerp(startIntensity, targetIntensity, time / duration);
            RenderSettings.fogDensity = Mathf.Lerp(startFogDensity, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        light.intensity = targetIntensity;
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
