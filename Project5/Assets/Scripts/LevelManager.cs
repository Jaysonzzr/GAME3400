using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject mainCamera;
    public PlayerController playerController;
    public CameraController cameraController;
    public AudioClip explodeSFX;
    [Header("Eye Binlks")]
    public RectTransform upperEyelid;
    public RectTransform lowerEyelid;
    public float openDuration1 = 1f;
    public float openDuration2 = 1f;
    public Vector2 upperOpenPosition1;
    public Vector2 lowerOpenPosition1;
    public Vector2 upperOpenPosition2;
    public Vector2 lowerOpenPosition2;
    private float openEyeTime = 0f;
    private bool isEyelidsMoving = false;
    [Header("Lights")]
    public Light[] lights;
    public float blinkingDuration = 10f;
    public float minBlinkInterval = 0.1f;
    public float maxBlinkInterval = 0.5f;
    [Header("StandUp")]
    public Transform liePosition;
    public Transform sitPosition;
    public Transform standPosition;
    public float transitionDuration = 2.0f;

    private bool isTransitioning = false;
    private bool startTransitioning = false;


    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerController.enabled = false;
        cameraController.enabled = false;
        mainCamera.transform.position = liePosition.position;
        mainCamera.transform.rotation = liePosition.rotation;
        foreach (Light light in lights)
        {
            light.intensity = 1f;
        }
        StartCoroutine(Explode());
        StartCoroutine(BlinkLights());
    }

    // Update is called once per frame
    void Update()
    {
        openEyeTime += Time.deltaTime;
        OpenEyes();
        StartStandUpTransition();
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2f);

        AudioSource.PlayClipAtPoint(explodeSFX, Camera.main.transform.position);
    }

    void OpenEyes()
    {
        if (isEyelidsMoving) return;

        if (openEyeTime > 6f && !isEyelidsMoving)
        {
            isEyelidsMoving = true;
            StartCoroutine(MoveEyelid(upperEyelid, upperOpenPosition2, openDuration2, () => isEyelidsMoving = false));
            StartCoroutine(MoveEyelid(lowerEyelid, lowerOpenPosition2, openDuration2, null));
        }
        else if (openEyeTime > 4f && !isEyelidsMoving)
        {
            isEyelidsMoving = true;
            StartCoroutine(MoveEyelid(upperEyelid, upperOpenPosition1, openDuration1, () => isEyelidsMoving = false));
            StartCoroutine(MoveEyelid(lowerEyelid, lowerOpenPosition1, openDuration1, null));
        }
    }

    IEnumerator MoveEyelid(RectTransform eyelid, Vector2 targetPosition, float duration, Action onComplete)
    {
        float time = 0;
        Vector2 startPosition = eyelid.anchoredPosition;

        while (time < duration)
        {
            time += Time.deltaTime;
            eyelid.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, time / duration);
            yield return null;
        }

        eyelid.anchoredPosition = targetPosition;

        onComplete?.Invoke();
    }

    IEnumerator BlinkLights()
    {
        float startTime = Time.time;

        while (Time.time - startTime < blinkingDuration)
        {
            foreach (Light light in lights)
            {
                light.enabled = !light.enabled;
            }

            float randomInterval = UnityEngine.Random.Range(minBlinkInterval, maxBlinkInterval);
            yield return new WaitForSeconds(randomInterval);
        }

        foreach (Light light in lights)
        {
            light.intensity = 0f;
            light.enabled = true;
            startTransitioning = true;
        }
    }

    void StartStandUpTransition()
    {
        if (!isTransitioning && startTransitioning)
        {
            StartCoroutine(FullTransition());
        }
    }

    IEnumerator FullTransition()
    {
        isTransitioning = true;

        yield return StartCoroutine(TransitionCamera(liePosition, sitPosition, transitionDuration));

        yield return new WaitForSeconds(2f);
        
        yield return StartCoroutine(TransitionCamera(sitPosition, standPosition, transitionDuration));

        isTransitioning = false;
        playerController.enabled = true;
        cameraController.enabled = true;
        startTransitioning = false;
    }

    IEnumerator TransitionCamera(Transform fromPosition, Transform toPosition, float duration)
    {
        float time = 0;

        Vector3 startPosition = fromPosition.position;
        Quaternion startRotation = fromPosition.rotation;
        Vector3 endPosition = toPosition.position;
        Quaternion endRotation = toPosition.rotation;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            mainCamera.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            yield return null;
        }

        mainCamera.transform.position = endPosition;
        mainCamera.transform.rotation = endRotation;
    }
}
