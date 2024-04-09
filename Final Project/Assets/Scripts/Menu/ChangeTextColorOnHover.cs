using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeTextColorOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioClip buttonSFX;
    public Color hoverColor = Color.red;
    private Color originalColor;
    private Text buttonText;
    public float transitionDuration = 0.1f;

    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        originalColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeColorSmoothly(buttonText.color, hoverColor, transitionDuration));
        AudioSource.PlayClipAtPoint(buttonSFX, Camera.main.transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeColorSmoothly(buttonText.color, originalColor, transitionDuration));
    }

    IEnumerator ChangeColorSmoothly(Color startColor, Color endColor, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            buttonText.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }

        buttonText.color = endColor;
    }
}
