using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float gravity = 9.81f;
    public GameObject sword;

    CharacterController controller;
    Vector3 input, moveDirection;

    public Slider healthSlider;
    
    public float swipeRate = 0.5f;
    private float elapsedTime = 0.0f;
    private bool sword_attack = false;
    private Vector3 sword_pos;
    public int swipeRadius;
    
    public int maxHealth = 10;
    private int currentHealth;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        healthSlider.value = maxHealth;
        currentHealth = maxHealth;
    }

    void Update()
    {
        PlayerMovement();
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && elapsedTime > swipeRate)
        {
            SwipeAttack();

            sword_attack = true;
            
            elapsedTime = 0.0f;
        }

        if (elapsedTime <= swipeRate / 2 && sword_attack)
        {
            sword.transform.localPosition = Vector3.Lerp(sword.transform.localPosition,
                new Vector3(0, sword.transform.localPosition.y, sword.transform.localPosition.z), 0.5f * Time.deltaTime);
        } else if (elapsedTime <= swipeRate)
        {
            sword.transform.localPosition = Vector3.Lerp(sword.transform.localPosition,
                new Vector3(0.25f, sword.transform.localPosition.y, sword.transform.localPosition.z), 0.5f * Time.deltaTime);
        } else
        {
            sword_attack = false;
            sword.transform.localPosition =
                new Vector3(0.25f, sword.transform.localPosition.y, sword.transform.localPosition.z);
        }

        elapsedTime += Time.deltaTime;
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
    
    private void SwipeAttack()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, new Vector3(swipeRadius, swipeRadius, swipeRadius));

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Slime"))
            {
                GameObject obj = hit.gameObject;
                SlimeBehavior sb = obj.GetComponent<SlimeBehavior>();
                sb.TakeDamage(1);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthSlider.value = currentHealth;
        
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
