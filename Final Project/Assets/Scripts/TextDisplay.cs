using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public Text textComponent;
    public float typingSpeed = 0.05f;
    public bool isTextTyping = false;
    public Color originalColor;
    public Color textColor;

    private int index;
    private string[] sentences;

    public void DisplayText(string[] newText)
    {
        if (isTextTyping) return;

        StopAllCoroutines();
        ResetTextAlpha();
        textComponent.text = "";
        sentences = newText;
        index = 0;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        isTextTyping = true;

        while (index < sentences.Length)
        {
            textComponent.text = "";
            string sentence = sentences[index];

            foreach (char letter in sentence.ToCharArray())
            {
                while (PauseMenuManager.isGamePaused)
                {
                    yield return null;
                }
                textComponent.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            index++;
            yield return new WaitForSeconds(1f);

            if (index >= sentences.Length)
            {
                yield return StartCoroutine(FadeTextAlpha());
                break;
            }
        }

        isTextTyping = false;
        textColor = originalColor;
    }

    IEnumerator FadeTextAlpha(float fadeDuration = 2f)
    {
        float startAlpha = textComponent.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, time / fadeDuration);
            textComponent.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }

        textComponent.color = new Color(textColor.r, textColor.g, textColor.b, 0);
    }

    private void ResetTextAlpha()
    {
        Color32 originalColor = textComponent.color;
        textComponent.color = new Color(textColor.r, textColor.g, textColor.b, 1f);
    }
}
