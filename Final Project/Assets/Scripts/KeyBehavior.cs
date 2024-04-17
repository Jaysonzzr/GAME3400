using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    bool triggered = false;

    public AudioSource audioSource;
    public AudioClip audioClip;

    public void CollectKey()
    {
        if (!triggered)
        {
            audioSource.PlayOneShot(audioClip);

            gameObject.GetComponent<Outline>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;

            triggered = true;
        }
    }
}
