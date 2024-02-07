using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float gravity = 9.81f;

    CharacterController controller;
    Vector3 input, moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        
        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized * moveSpeed;

        if (controller.isGrounded)
        {
            moveDirection = input;
            moveDirection.y = 0;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
