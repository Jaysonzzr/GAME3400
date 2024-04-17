using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public string[] textToDisplay;

    bool triggered = false;

    public static bool unlockSafe = false;

    public Vector3 targetRotation;

    IEnumerator PlayAfterDelay(AudioSource source, AudioClip clip, float waitSecond)
    {
        yield return new WaitForSeconds(waitSecond);
        source.clip = clip;
        source.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
            Camera.main.GetComponent<CameraController>().enabled = false;

            StartCoroutine(RotateToAndBackRoutine(Quaternion.Euler(targetRotation), 2f));
        
            FindObjectOfType<TextDisplay>().DisplayText(textToDisplay);
            
            if (audioClip != null)
            {
                StartCoroutine(PlayAfterDelay(audioSource, audioClip, 0.1f));
            }

            triggered = true;
        }
    }

    private IEnumerator RotateToAndBackRoutine(Quaternion targetRotation, float duration)
    {
        ShaderEffect_BleedingColors shader = Camera.main.GetComponent<ShaderEffect_BleedingColors>();

        yield return new WaitForSeconds(0.5f);

        Quaternion originalRotation = Camera.main.transform.rotation;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            Camera.main.transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.rotation = targetRotation;

        yield return new WaitForSeconds(1f);

        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            Camera.main.transform.rotation = Quaternion.Lerp(targetRotation, originalRotation, elapsedTime / duration);
            shader.intensity = Mathf.Lerp(0f, 4f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.rotation = originalRotation;
        shader.intensity = 4;

        GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;
        Camera.main.GetComponent<CameraController>().enabled = true;
    }
}
