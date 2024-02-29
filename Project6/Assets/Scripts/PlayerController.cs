using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 5.0f;
    public float gravity = 9.81f;
    private Vector3 moveDirection = Vector3.zero;
    public Transform cameraTransform;

    private float bobbingFrequency = 8f;
    private float bobbingAmplitude = 0.08f;
    private float bobTimer = 0.0f;
    private float originalCameraY;
    private bool isMoving = false;

    void Start()
    {
        originalCameraY = cameraTransform.localPosition.y;
    }

    void Update()
    {
        PlayerMovement();
        ResetCameraPosition();
    }

    void PlayerMovement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized * moveSpeed;

        isMoving = input.magnitude > 0f;

        if (controller.isGrounded)
        {
            moveDirection = input;
            moveDirection.y = 0f;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);

        if (isMoving)
        {
            bobTimer += Time.deltaTime;
            float bobSine = Mathf.Sin(bobTimer * bobbingFrequency) * bobbingAmplitude;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, originalCameraY + bobSine, cameraTransform.localPosition.z);
        }
        else
        {
            bobTimer = 0f;
        }
    }

    void ResetCameraPosition()
    {
        if (!isMoving && Mathf.Abs(cameraTransform.localPosition.y - originalCameraY) > 0.001f)
        {
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, new Vector3(cameraTransform.localPosition.x, originalCameraY, cameraTransform.localPosition.z), Time.deltaTime * 5f);
        }
    }
}
