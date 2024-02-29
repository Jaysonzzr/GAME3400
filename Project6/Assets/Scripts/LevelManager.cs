using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player;

    public string[] textToDisplay;

    public Image background;
    public Image reticle;

    public Vector3 targetPlayerPosition;

    public RectTransform imageUp;
    public RectTransform imageDown;
    public float moveDistance = 100f;
    public float duration = 2f;

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainCamera.GetComponent<CameraController>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<TextDisplay>().DisplayNonInteractableText(textToDisplay);
            StartCoroutine(FadeToAlpha(0, 7f));
            StartCoroutine(MoveCameraThenPlayer(Quaternion.Euler(new Vector3(0, 0, 0)), targetPlayerPosition, 1.3f, 7));
        }
    }

    IEnumerator FadeToAlpha(float targetAlpha, float duration)
    {
        yield return new WaitForSeconds(1f);

        Color color = background.color;
        float startAlpha = color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float normalizedTime = time / duration;

            color.a = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            background.color = color;

            yield return null;
        }

        color.a = targetAlpha;
        background.color = color;
    }

    IEnumerator MoveCameraThenPlayer(Quaternion targetCameraRotation, Vector3 targetPlayerPosition, float rotationDuration, float moveDuration)
    {
        yield return new WaitForSeconds(6f);

        Quaternion startRotation = mainCamera.transform.rotation;
        float rotationTime = 0;

        while (rotationTime < rotationDuration)
        {
            rotationTime += Time.deltaTime;
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, targetCameraRotation, rotationTime / rotationDuration);
            yield return null;
        }

        mainCamera.transform.rotation = targetCameraRotation;

        Vector3 startPosition = player.transform.position;
        float moveTime = 0;

        float amplitude = 0.08f;
        float frequency = 8f;

        Vector3 cameraStartPosition = mainCamera.transform.localPosition;

        yield return new WaitForSeconds(1f);

        while (moveTime < moveDuration)
        {
            moveTime += Time.deltaTime;
            player.transform.position = Vector3.Lerp(startPosition, targetPlayerPosition, moveTime / moveDuration);

            float deltaY = Mathf.Sin(moveTime * frequency) * amplitude;
            mainCamera.transform.localPosition = new Vector3(cameraStartPosition.x, cameraStartPosition.y + deltaY, cameraStartPosition.z);

            yield return null;
        }

        player.transform.position = targetPlayerPosition;

        mainCamera.transform.localPosition = cameraStartPosition;

        StartCoroutine(FadeReticle());
    }

    IEnumerator FadeReticle()
    {
        yield return new WaitForSeconds(0.5f);
        Color color = reticle.color;
        float startAlpha = color.a;
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime;
            float normalizedTime = time / 1;

            color.a = Mathf.Lerp(startAlpha, 1, normalizedTime);
            reticle.color = color;

            yield return null;
        }

        color.a = 1;
        reticle.color = color;

        StartCoroutine(MoveImagesCoroutine());
        mainCamera.GetComponent<CameraController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
    }

    IEnumerator MoveImagesCoroutine()
    {
        float elapsedTime = 0;
        Vector2 startPositionUp = imageUp.anchoredPosition;
        Vector2 startPositionDown = imageDown.anchoredPosition;
        Vector2 targetPositionUp = startPositionUp + new Vector2(0, moveDistance);
        Vector2 targetPositionDown = startPositionDown + new Vector2(0, -moveDistance);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / duration;

            imageUp.anchoredPosition = Vector2.Lerp(startPositionUp, targetPositionUp, normalizedTime);
            imageDown.anchoredPosition = Vector2.Lerp(startPositionDown, targetPositionDown, normalizedTime);

            yield return null;
        }

        imageUp.anchoredPosition = targetPositionUp;
        imageDown.anchoredPosition = targetPositionDown;
    }
}
