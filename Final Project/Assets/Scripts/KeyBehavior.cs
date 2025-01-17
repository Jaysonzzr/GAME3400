using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    public static bool getKey = false;
    bool triggered = false;

    public AudioSource audioSource;
    public AudioClip audioClip;

    public void CollectKey()
    {
        if (!triggered)
        {
            getKey = true;
            
            audioSource.PlayOneShot(audioClip);

            gameObject.GetComponent<Outline>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;

            triggered = true;
        }
    }
}
