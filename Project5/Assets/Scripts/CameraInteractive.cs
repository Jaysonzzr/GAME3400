using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteractive : MonoBehaviour
{
    public float maxDistance; // Maximum distance at which object info will be displayed

    public Light light;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.CompareTag("Spotlight"))
            {
                float distance = Vector3.Distance(transform.position, hitObject.transform.position);
                if (distance <= maxDistance)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hitObject.GetComponent<MeshRenderer>().enabled = false;
                        hitObject.GetComponent<MeshCollider>().enabled = false;
                        light.enabled = true;
                    }
                }
            }
        }
    }
}
