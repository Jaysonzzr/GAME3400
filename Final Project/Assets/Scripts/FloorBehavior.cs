using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBehavior : MonoBehaviour
{
    public Transform floor;

    int count = 1;

    private void OnTriggerExit(Collider other)
    {
        if (LoopManager.unlockSafe)
        {
            if (count % 2 != 0)
            {
                floor.position = new Vector3(floor.position.x - 168, 0, 0);
            }
            else
            {
                floor.position = new Vector3(floor.position.x + 168, 0, 0);
            }

            count++;
        }
    }
}
