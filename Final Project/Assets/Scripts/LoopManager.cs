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

    IEnumerator PlayAfterDelay(AudioSource source, AudioClip clip, float waitSecond)
    {
        yield return new WaitForSeconds(waitSecond);
        source.clip = clip;
        source.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && audioSource != null && !audioSource.isPlaying && gameObject.GetComponent<AudioSource>() == null && !FindObjectOfType<TextDisplay>().isTextTyping)
        {
            FindObjectOfType<TextDisplay>().DisplayText(textToDisplay);
            
            if (audioClip != null)
            {
                StartCoroutine(PlayAfterDelay(audioSource, audioClip, 0.1f));
            }

            triggered = true;
        }
    }
}
