using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public AudioClip audioClip;
    public float cooldownTime = 2.0f;

    public string[] textToDisplay;

    private AudioSource audioSource;
    private float lastPlayTime = -Mathf.Infinity;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!PauseMenuManager.isGamePaused)
        {
            audioSource.UnPause();
        }
        else
        {
            audioSource.Pause();
        }
    }

    public void PlayClip()
    {
        if (Time.time - lastPlayTime >= cooldownTime)
        {
            if (audioSource != null && !audioSource.isPlaying && gameObject.GetComponent<AudioSource>() == null && !FindObjectOfType<TextDisplay>().isTextTyping)
            {
                FindObjectOfType<TextDisplay>().DisplayText(textToDisplay);
                // StartCoroutine(PlayAfterDelay(audioSource, audioClip, 0.5f));
                lastPlayTime = Time.time;
            }
        }
        else
        {
            Debug.Log("Still in cooldown. Please wait.");
        }
    }

    IEnumerator PlayAfterDelay(AudioSource source, AudioClip clip, float waitSecond)
    {
        yield return new WaitForSeconds(waitSecond);
        source.clip = clip;
        source.Play();
    }
}
