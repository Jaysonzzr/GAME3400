using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteractive : MonoBehaviour
{
    public Outline outline;

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
                                    hitObject.GetComponent<ExitBehavior>().EscapeRoom();
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
                            else if (hitObject.name == "Key")
                            {
                                outline.OutlineWidth = 3;
                                hitObject.GetComponent<KeyBehavior>().CollectKey();
                            }
                            else if (hitObject.name == "Basement")
                            {
                                if (KeyBehavior.getKey)
                                {
                                    hitObject.GetComponent<BasementEnding>().EscapeRoom();
                                }
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
