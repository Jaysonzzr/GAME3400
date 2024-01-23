using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecticleInteractive : MonoBehaviour
{
    public Text infoText;

    public float maxDistance; // Maximum distance at which object info will be displayed

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.CompareTag("Clothes"))
            {
                float distance = Vector3.Distance(transform.position, hitObject.transform.position);
                if (distance <= maxDistance)
                {
                    // Make sure Players cannot interact with the clothes they are dressing 
                    if (!hitObject.GetComponent<ClothesBehavior>().dressed)
                    {
                        infoText.text = hitObject.name + " (E)";

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            if (!hitObject.GetComponent<ClothesBehavior>().collected) // Haven't been collected
                            {
                                hitObject.GetComponent<ClothesBehavior>().collected = true;
                                hitObject.GetComponent<ClothesBehavior>().PutToCloset();
                            }
                            else
                            {
                                // Find all clothes
                                ClothesBehavior[] allClothes = FindObjectsOfType<ClothesBehavior>();
                                foreach (ClothesBehavior clothes in allClothes)
                                {
                                    // Put the previous clothes back
                                    if (clothes != hitObject.GetComponent<ClothesBehavior>())
                                    {
                                        clothes.PutToCloset();
                                    }
                                    // Dress the clothes
                                    else
                                    {
                                        clothes.DressOn();
                                    }
                                }
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
            else
            {
                // Clear the information text if the player is too far away from the inspectable object
                infoText.text = "";
            }
        }
    }
}
