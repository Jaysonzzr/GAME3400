using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10;
    public GameObject miniSlimes;
    public int maxHealth = 20;

    public int currentHealth;

    private float hitDelay = 0.5f;
    private float counter;
    private bool canHit = true;
    private bool wasHit;

    private float back = 1;
    private float counterForBack;

    private int damageAmount;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        counter = hitDelay;
        currentHealth = maxHealth;
        damageAmount = 1;
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
            playerController.TakeDamage(damageAmount);
            wasHit = true;
            counterForBack = 0;
        }
    }

    public void SetDamage(int set)
    {
        damageAmount = set;
    }

    public void SetMaxHealth(int set)
    {
        maxHealth = set;
        currentHealth = maxHealth;
    }

    public void SetSpeed(float set)
    {
        moveSpeed = set;
    }
    public void TakeDamage(int damage)
    {
        counterForBack = 0;
        currentHealth -= damage;
        wasHit = true;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if (currentHealth <= maxHealth / 2 && maxHealth > 2)
        {
            GameObject slime1 = Instantiate(miniSlimes, transform.position, transform.rotation);
            GameObject slime2 = Instantiate(miniSlimes, new Vector3(transform.position.x + 1, transform.position.y, 
                transform.position.z + 1), transform.rotation);
            slime1.GetComponent<SlimeBehavior>().SetDamage(damageAmount + 1);
            slime1.GetComponent<SlimeBehavior>().SetMaxHealth(maxHealth / 4);
            slime1.GetComponent<SlimeBehavior>().SetSpeed(moveSpeed * 2);
            slime1.transform.localScale -= new Vector3(1f, 0, 1);
            
            slime2.GetComponent<SlimeBehavior>().SetDamage(damageAmount + 1);
            slime2.GetComponent<SlimeBehavior>().SetMaxHealth(maxHealth / 4);
            slime2.GetComponent<SlimeBehavior>().SetSpeed(moveSpeed * 2);
            slime2.transform.localScale -= new Vector3(1f, 0, 1);
            
            Destroy(gameObject);
        }
    }
}
