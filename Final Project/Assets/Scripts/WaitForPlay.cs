using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPlay : MonoBehaviour
{
    public float waitSeconds;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayClip(waitSeconds));
    }

    IEnumerator PlayClip(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);

        GetComponent<AudioSource>().Play();
    }
}
