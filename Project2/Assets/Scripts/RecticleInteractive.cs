using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecticleInteractive : MonoBehaviour
{
    public Text infoText;

    public float maxDistance; // Maximum distance at which object info will be displayed

    private bool getKey = false;
    private bool openDoor = false;

    public Image imageToFade;
    public float fadeInTime = 2.0f;

    [Header("Door Behavior")]
    public float speed = 1.0f;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    void Start()
    {
        startPosition = GameObject.FindGameObjectWithTag("Door").transform.localPosition;
        targetPosition = new Vector3(startPosition.x - 3, startPosition.y, startPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor)
        {
            GameObject door = GameObject.FindGameObjectWithTag("Door");
                                
            door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, targetPosition, speed * Time.deltaTime);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.CompareTag("Key") || hitObject.CompareTag("Trap") || hitObject.CompareTag("Window") || hitObject.CompareTag("Real"))
            {
                float distance = Vector3.Distance(transform.position, hitObject.transform.position);
                if (distance <= maxDistance)
                {
                    infoText.text = hitObject.name + " (E)";

                    if (hitObject.CompareTag("Key"))
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            getKey = true;
                            GameObject.Destroy(hitObject);
                        }
                    }

                    if (hitObject.CompareTag("Real"))
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            if (getKey)
                            {
                                openDoor = true;

                                GameObject.FindGameObjectWithTag("Real").tag = "Untagged";
                            }
                        }
                    }

                    if (hitObject.CompareTag("Window"))
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
                            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().enabled = false;
                            StartCoroutine(FadeIn());
                        }
                    }
                }
                else
                {
                    // Clear the information text if the player is too far away from the inspectable object
                    infoText.text = "";
                }
            }
            else
            {
                // Clear the information text if the player is too far away from the inspectable object
                infoText.text = "";
            }
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;

        Color c = imageToFade.color;
        while (elapsedTime < fadeInTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeInTime);
            imageToFade.color = c;
        }
    }
}
