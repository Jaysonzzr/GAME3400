using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 5.0f;
    public float runMultiplier = 2.0f;
    public float gravity = 9.81f;
    private Vector3 moveDirection = Vector3.zero;
    public Transform cameraTransform;

    private float bobbingFrequency = 8f;
    private float bobbingAmplitude = 0.08f;
    private float bobTimer = 0.0f;
    private float originalCameraY;
    private bool isMoving = false;

    public float energy = 100f;
    public float energyDepletionRate = 10f;
    public float energyRecoveryRate = 5f;

    private float lastCameraY;
    public AudioClip[] footsteps;
    private int currentFootstepIndex = 0;
    private bool canPlayFootstep = true;
    public AudioSource audioSource;

    void Start()
    {
        originalCameraY = cameraTransform.localPosition.y;
    }

    void Update()
    {
        if (!PauseMenuManager.isGamePaused)
        {
            PlayerMovement();
            ResetCameraPosition();
            ManageEnergy();
        }
    }

    void PlayerMovement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized * moveSpeed;
        isMoving = input.magnitude > 0f;

        if (isMoving && Input.GetKey(KeyCode.LeftShift) && energy > 0)
        {
            input *= runMultiplier;
            energy -= energyDepletionRate * Time.deltaTime;
            energy = Mathf.Max(energy, 0);
            bobbingFrequency = 16;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            bobbingFrequency = 8;
        }

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

            if (cameraTransform.localPosition.y < lastCameraY && canPlayFootstep)
            {
                audioSource.PlayOneShot(footsteps[currentFootstepIndex]);
                currentFootstepIndex = (currentFootstepIndex + 1) % footsteps.Length;
                canPlayFootstep = false;
            }
            else if (cameraTransform.localPosition.y > lastCameraY)
            {
                canPlayFootstep = true;
            }

            lastCameraY = cameraTransform.localPosition.y;
        }
        else
        {
            bobTimer = 0f;
            lastCameraY = cameraTransform.localPosition.y;
        }
    }

    void ManageEnergy()
    {
        if (!isMoving || (isMoving && !Input.GetKey(KeyCode.LeftShift)))
        {
            energy += energyRecoveryRate * Time.deltaTime;
            energy = Mathf.Min(energy, 100);
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
