using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public AudioClip globalAudioClip;

    bool triggered = false;

    public void PlayClip()
    {
        if (!triggered && audioClip != null && audioSource != null && !audioSource.isPlaying && gameObject.GetComponent<AudioSource>() == null)
        {
            StartCoroutine(PlayAfterDelay(audioSource, audioClip, 0.5f));

            gameObject.GetComponent<Outline>().enabled = false;

            triggered = true;
        }

        AudioSource jukebox = gameObject.GetComponent<AudioSource>();
        if (jukebox != null && !jukebox.isPlaying && !triggered)
        {
            jukebox.PlayOneShot(globalAudioClip);
            StartCoroutine(PlayAfterDelay(audioSource, audioClip, 1.5f));
            StartCoroutine(GlitchPitch(jukebox, 2));
            gameObject.GetComponent<Outline>().enabled = false;
            triggered = true;
        }
    }

    IEnumerator PlayAfterDelay(AudioSource source, AudioClip clip, float waitSecond)
    {
        yield return new WaitForSeconds(waitSecond);
        source.clip = clip;
        source.Play();
    }

    IEnumerator GlitchPitch(AudioSource source, float duration)
    {
        float startTime = Time.time;
        float glitchProbability = 0.5f;
        float minPitch = 0.8f;
        float maxPitch = 1.2f;
        float glitchInterval = 0.1f;

        while (Time.time - startTime < duration)
        {
            if (Random.value < glitchProbability)
            {
                source.pitch = Random.Range(minPitch, maxPitch);
            }
            else
            {
                source.pitch = 1.0f;
            }

            yield return new WaitForSeconds(glitchInterval);
        }

        source.pitch = 1.0f;
    }
}
