using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDelay : MonoBehaviour
{
    public AudioClip gearhead;

    private float counter = 0.0f;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (counter > 10)
        {
            AudioSource.PlayClipAtPoint(gearhead, transform.position);
            counter = 0;
        }
    }
}
