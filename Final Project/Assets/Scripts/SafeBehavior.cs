using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBehavior : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public string[] textToDisplay;

    bool triggered = false;

    public void OpenSafe()
    {
        if (!triggered && audioClip != null && audioSource != null && !audioSource.isPlaying && gameObject.GetComponent<AudioSource>() == null)
        {
            FindObjectOfType<TextDisplay>().DisplayText(textToDisplay);
            StartCoroutine(PlayAfterDelay(audioSource, audioClip, 0.5f));

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
}
