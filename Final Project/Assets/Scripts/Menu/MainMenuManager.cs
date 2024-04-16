using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject credits;

    public Image image;
    public Text text;
    float waitTime = 2f;
    float textFadeTime = 2f;
    float imageFadeTime = 3f;
    float currentTime = 3f;
    float textTime = 4f;
    float fadeTime = 3f;

    public Text back;

    public AudioSource audioSource;

    private void Start() 
    {
        Screen.SetResolution(1920, 1080, true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (image != null && !image.gameObject.activeSelf)
            image.gameObject.SetActive(true);
            
        if (text != null && !text.gameObject.activeSelf)
            text.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime < 0)
        {
            StartCoroutine(FadeIn());
            waitTime = 0;
        }
        else if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        if (textTime < 0)
        {
            StartCoroutine(FadeOut());
            textTime = 0;
        }
        else if (textTime > 0)
        {
            textTime -= Time.deltaTime;
        }

        if (fadeTime < 0)
        {
            image.raycastTarget = false;
            text.raycastTarget = false;
            audioSource.Play();
            StartCoroutine(ImageFadeOut());
            fadeTime = 0;
        }
        else if (fadeTime > 0 && textTime == 0)
        {
            fadeTime -= Time.deltaTime;
        }

        if (currentTime < 0)
        {
            image.gameObject.SetActive(false);
        }
        else if (currentTime > 0 && fadeTime == 0)
        {
            currentTime -= Time.deltaTime;
        }
        
        if (credits.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                credits.SetActive(false);
                back.color = Color.white;
            }
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < textFadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, Mathf.Clamp01(elapsedTime / textFadeTime));
            Color color = new Color(255 / 255, 255 / 255, 255 / 255, alpha);
            text.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < textFadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, Mathf.Clamp01(elapsedTime / textFadeTime));
            Color textColor = new Color(255 / 255, 255 / 255, 255 / 255, alpha);
            text.color = textColor;
            yield return null;
        }
    }
    
    IEnumerator ImageFadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < imageFadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, Mathf.Clamp01(elapsedTime / imageFadeTime));
            Color color = new Color(0 / 255, 0 / 255, 0 / 255, alpha);
            image.color = color;
            yield return null;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeColor()
    {
        back.color = Color.white;
    }
}
