using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    public GameObject player;

    public GameObject background;
    Image backgroundImage;

    public AudioSource normalSource;
    public AudioSource playerCamera;
    public AudioClip elevator;
    public AudioClip intro;

    float durationTime = 23;
    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        normalSource.PlayOneShot(elevator);
        player.GetComponent<PlayerController>().enabled = false;
        Camera.main.GetComponent<CameraController>().enabled = false;

        background.SetActive(true);
        backgroundImage = background.GetComponent<Image>();
        StartCoroutine(FadeToAlpha(0, 5));
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuManager.isGamePaused)
        {
            if (durationTime <= 3 && !flag)
            {
                flag = true;
                normalSource.PlayOneShot(intro);
                playerCamera.Play();
                StartCoroutine(RotateCameraToAngle(0));
            }
            else if (!flag)
            {
                durationTime -= Time.deltaTime;
            }
        }
    }

    IEnumerator FadeToAlpha(float targetAlpha, float duration)
    {
        yield return new WaitForSeconds(18f);

        Color color = backgroundImage.color;
        float startAlpha = color.a;
        float time = 0;

        while (time < duration)
        {
            while (PauseMenuManager.isGamePaused)
            {
                yield return null;
            }

            time += Time.deltaTime;
            float normalizedTime = time / duration;

            color.a = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            backgroundImage.color = color;

            yield return null;
        }

        color.a = targetAlpha;
        backgroundImage.color = color;
        background.SetActive(false);
    }

    IEnumerator RotateCameraToAngle(float targetAngle)
    {
        yield return new WaitForSeconds(5f);

        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        Quaternion startRotation = player.transform.rotation;
        float time = 0.0f;

        while (time < 1.0f)
        {
            while (PauseMenuManager.isGamePaused)
            {
                yield return null;
            }
            
            time += Time.deltaTime * 0.15f;

            player.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, time);

            yield return null;
        }

        player.transform.rotation = targetRotation;

        player.GetComponent<PlayerController>().enabled = true;
        Camera.main.GetComponent<CameraController>().enabled = true;    
    }
}
