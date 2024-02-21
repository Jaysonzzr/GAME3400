using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 2.0f;
    public float smoothing = 2.0f;

    GameObject player;
    Vector3 offset;

    Vector2 mouseLook;
    Vector2 smoothV;

    float originalCameraY;
    float targetCameraY;

    void Start()
    {
        // Screen.SetResolution(1920, 1080, true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;

        originalCameraY = transform.localPosition.y;
        targetCameraY = originalCameraY;
    }

    void Update()
    {
        // Get the mouse input and calculate the camera movement
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1f / smoothing);
        mouseLook += smoothV;

        // Clamp the vertical camera movement to prevent flipping
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        // Apply the camera rotation
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}
