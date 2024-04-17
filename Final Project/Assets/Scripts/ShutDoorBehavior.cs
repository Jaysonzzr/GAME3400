using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutDoorBehavior : MonoBehaviour
{
    public GameObject door;
    public Vector3 targetRotation;

    public AudioSource audioSource;
    public AudioClip audioClip;

    bool triggered = false;

    private void OnTriggerEnter(Collider other) 
    {
        if (DoorBehavior.openDoor && !triggered)
        {
            StartCoroutine(RotateRoutine(Quaternion.Euler(targetRotation), 0.05f));
            triggered = true;
        }
    }

    private IEnumerator RotateRoutine(Quaternion targetRotation, float duration)
    {
        Quaternion originalRotation = door.transform.rotation;

        audioSource.PlayOneShot(audioClip);

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            door.transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        door.transform.rotation = targetRotation;
    }
}
