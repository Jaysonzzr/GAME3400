using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasementEnding : MonoBehaviour
{
    bool triggered = false;

    public Image imageToFade;
    public Text text;
    public float fadeDuration = 2f;
    public float waitTime = 2f;

    public Outline outline;

    public void EscapeRoom()
    {
        if (!triggered)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
            Camera.main.GetComponent<CameraController>().enabled = false;

            outline.enabled = false;

            triggered = true;
            StartCoroutine(FadeImageAndLoadMainMenu());
        }
    }

    IEnumerator FadeImageAndLoadMainMenu()
    {       
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

        yield return new WaitForSeconds(3);

        Application.Quit();
    }
}
