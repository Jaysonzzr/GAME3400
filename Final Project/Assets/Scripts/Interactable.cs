using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public AudioClip audioClip;
    public float cooldownTime = 2.0f;

    public string[] textToDisplay;

    public AudioSource audioSource;
    private float lastPlayTime = -Mathf.Infinity;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void PlayClip()
    {
        if (Time.time - lastPlayTime >= cooldownTime)
        {
            FindObjectOfType<TextDisplay>().DisplayText(textToDisplay);
            if (audioClip != null)
            {
                StartCoroutine(PlayAfterDelay(audioSource, audioClip, 0.1f));
            }
            lastPlayTime = Time.time;
        }
    }

    IEnumerator PlayAfterDelay(AudioSource source, AudioClip clip, float waitSecond)
    {
        yield return new WaitForSeconds(waitSecond);
        source.clip = clip;
        source.Play();
    }
}
