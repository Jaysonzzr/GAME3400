using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitBehavior : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public string[] textToDisplay;

    bool triggered = false;

    public Image imageToFade;
    public Text text;
    public float fadeDuration = 2f;
    public float waitTime = 2f;

    public Color color;

    public void EscapeRoom()
    {
        if (!triggered && audioSource != null && !audioSource.isPlaying && gameObject.GetComponent<AudioSource>() == null && !FindObjectOfType<TextDisplay>().isTextTyping)
        {
            FindObjectOfType<TextDisplay>().textColor = color;
            FindObjectOfType<TextDisplay>().DisplayText(textToDisplay);

            if (audioClip != null)
            {
                StartCoroutine(PlayAfterDelay(audioSource, audioClip, 0.1f));
            }

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
            Camera.main.GetComponent<CameraController>().enabled = false;

            gameObject.GetComponent<Outline>().enabled = false;

            triggered = true;
            StartCoroutine(FadeImageAndLoadMainMenu());
        }
    }

    IEnumerator PlayAfterDelay(AudioSource source, AudioClip clip, float waitSecond)
    {
        yield return new WaitForSeconds(waitSecond);
        source.clip = clip;
        source.Play();
    }

    IEnumerator FadeImageAndLoadMainMenu()
    {       
        yield return new WaitForSeconds(5);

        imageToFade.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            while (PauseMenuManager.isGamePaused)
            {
                yield return null;
            }

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, 1f);

        yield return new WaitForSeconds(2);

        float elapsedTime1 = 0f;
        while (elapsedTime1 < fadeDuration)
        {
            while (PauseMenuManager.isGamePaused)
            {
                yield return null;
            }

            elapsedTime1 += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(elapsedTime1 / fadeDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(0);
    }
}
