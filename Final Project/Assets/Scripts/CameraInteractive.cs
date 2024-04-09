using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteractive : MonoBehaviour
{
    public float maxDistance; // Maximum distance at which object info will be displayed

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuManager.isGamePaused)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Interactable"))
                {
                    float distance = Vector3.Distance(transform.position, hitObject.transform.position);
                    if (distance <= maxDistance)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            if (hitObject.name == "Exit")
                            {
                                if (KeycardBehavior.unlockExit)
                                {
                                    Debug.Log("ESCAPE");
                                }
                                else
                                {
                                    hitObject.GetComponent<Interactable>().PlayClip();
                                }
                            }
                            else if (hitObject.name == "Safe")
                            {
                                if (LoopManager.unlockSafe)
                                {
                                    hitObject.GetComponent<SafeBehavior>().OpenSafe();
                                }
                                else
                                {
                                    hitObject.GetComponent<Interactable>().PlayClip();
                                }
                            }
                            else if (hitObject.name == "Keycard")
                            {
                                hitObject.GetComponent<KeycardBehavior>().CollectKeycard();
                            }
                            else
                            {
                                hitObject.GetComponent<Interactable>().PlayClip();
                            }
                        }
                    }
                }
            }
        }
    }
}
