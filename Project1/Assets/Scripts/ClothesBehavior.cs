using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesBehavior : MonoBehaviour
{
    public bool collected = false;
    public bool dressed = false;

    [Header("Settings of Clothes in the Closet")]
    public Vector3 closetPosition;
    public Vector3 closetRotation;
    public Vector3 closetScale;

    [Header("Settings of Clothes when Dressing")]
    public Vector3 dressPosition;
    public Vector3 dressRotation;
    public Vector3 dressScale;

    public void PutToCloset()
    {
        dressed = false;

        gameObject.layer = 0; // Make it visible in the main camera
        gameObject.GetComponent<MeshCollider>().enabled = true;

        if (collected)
        {
            transform.SetParent(GameObject.FindGameObjectWithTag("ClothesBox").transform);

            transform.localPosition = closetPosition;
            transform.localRotation = Quaternion.Euler(closetRotation);
            transform.localScale = closetScale;
        }
    }

    public void DressOn()
    {
        dressed = true;

        gameObject.layer = 6; // Make it invisible in the main camera
        gameObject.GetComponent<MeshCollider>().enabled = false;

        transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);

        transform.localPosition = dressPosition;
        transform.localRotation = Quaternion.Euler(dressRotation);
        transform.localScale = dressScale;
    }
}
