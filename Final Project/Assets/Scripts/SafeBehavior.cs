using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBehavior : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioClip unlockSafeSFX;
    public AudioSource audioSource;
    public AudioSource unlockAudioSource;
    public string[] textToDisplay;

    bool triggered = false;

    public Transform safeTrans;
    public float rotationDuration = 2f;

    public Color color;

    public void OpenSafe()
    {
        if (!triggered && audioSource != null && !audioSource.isPlaying && gameObject.GetComponent<AudioSource>() == null && !FindObjectOfType<TextDisplay>().isTextTyping)
        {
            FindObjectOfType<TextDisplay>().textColor = color;
            FindObjectOfType<TextDisplay>().DisplayText(textToDisplay);

            StartCoroutine(RotateSafeDoor());
            
            if (audioClip != null)
            {
                StartCoroutine(PlayAfterDelay(audioSource, audioClip, 0.1f));
            }
            unlockAudioSource.PlayOneShot(unlockSafeSFX);

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

    IEnumerator RotateSafeDoor()
    {
        float timeElapsed = 0;
        float startAngle = 109f;
        float endAngle = 230f;

        while (timeElapsed < rotationDuration)
        {
            float zRotation = Mathf.Lerp(startAngle, endAngle, timeElapsed / rotationDuration);
            
            safeTrans.localRotation = Quaternion.Euler(safeTrans.localRotation.eulerAngles.x, safeTrans.localRotation.eulerAngles.y, zRotation);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        safeTrans.localRotation = Quaternion.Euler(safeTrans.localRotation.eulerAngles.x, safeTrans.localRotation.eulerAngles.y, endAngle);
    }
}
