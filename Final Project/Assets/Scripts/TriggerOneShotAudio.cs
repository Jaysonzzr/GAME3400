using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOneShotAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            StartCoroutine(PlayAfterDelay(audioSource, audioClip, 0.1f));
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
