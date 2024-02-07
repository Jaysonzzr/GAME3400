using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10;
    public float minDistance = 2;
    public int damageAmount = 20;
    public int maxHealth = 20;

    public int currentHealth;

    private float hitDelay = 0.5f;
    private float counter;
    private bool canHit = true;
    private bool wasHit;

    private float back = 1;
    private float counterForBack;
    private void Start()
    {
        counter = hitDelay;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        if (counter > hitDelay)
        {
            // variable not used right now
            canHit = true;
        }

        if (wasHit && counterForBack < back)
        {
            transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        }
        counter += Time.deltaTime;
        counterForBack += Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && canHit)
        {
            canHit = false;
            counter = 0;
            var playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(1);
            wasHit = true;
            counterForBack = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        counterForBack = 0;
        currentHealth -= damage;
        wasHit = true;

        if (currentHealth <= maxHealth / 2)
        {
            // SPLIT
        }
    }
}
