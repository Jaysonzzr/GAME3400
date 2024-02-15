using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float typingSpeed = 0.05f;
    public bool isTextTyping = false;

    private int index;
    private string[] sentences;

    public CameraInteractive cameraInteractive;
    
    public void DisplayText(string[] newText, GameObject hitObject)
    {
        if (isTextTyping) return;

        StopAllCoroutines();
        ResetTextAlpha();
        textComponent.text = "";
        sentences = newText;
        index = 0;
        StartCoroutine(TypeSentence(hitObject));
    }

    IEnumerator TypeSentence(GameObject hitObject)
    {
        isTextTyping = true;

        cameraInteractive.itemCount++;
        hitObject.GetComponent<MeshRenderer>().enabled = false;
        hitObject.GetComponent<MeshCollider>().enabled = false;

        while (index < sentences.Length)
        {
            textComponent.text = "";
            string sentence = sentences[index];

            foreach (char letter in sentence.ToCharArray())
            {
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
    }

    IEnumerator FadeTextAlpha(float fadeDuration = 2f)
    {
        float startAlpha = textComponent.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, time / fadeDuration);
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);
            yield return null;
        }

        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0);
    }

    private void ResetTextAlpha()
    {
        Color32 originalColor = textComponent.color;
        textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }
}
