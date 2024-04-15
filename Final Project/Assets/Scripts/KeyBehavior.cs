using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public string[] textToDisplay;

    bool triggered = false;

    public void CollectKey()
    {
        if (!triggered && audioSource != null && !audioSource.isPlaying && gameObject.GetComponent<AudioSource>() == null && !FindObjectOfType<TextDisplay>().isTextTyping)
        {            
            FindObjectOfType<TextDisplay>().DisplayText(textToDisplay);
            if (audioClip != null)
            {
                StartCoroutine(PlayAfterDelay(audioSource, audioClip, 0.1f));
            }

            gameObject.GetComponent<Outline>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;

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