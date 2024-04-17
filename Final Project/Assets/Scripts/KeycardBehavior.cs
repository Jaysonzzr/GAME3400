using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardBehavior : MonoBehaviour
{
    public static bool unlockExit = false;
    bool triggered = false;

    public AudioSource audioSource;
    public AudioClip audioClip;

    public void CollectKeycard()
    {
        if (!triggered)
        {
            unlockExit = true;

            audioSource.PlayOneShot(audioClip);
            
            gameObject.GetComponent<Outline>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;

            triggered = true;
        }
    }
}
