using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ClockBehavior : MonoBehaviour
{
    public enum PointerType
    {
        Hour,
        Minute,
        Second
    }

    public PointerType pointerType;

    private void Update()
    {
        // Get the current time
        DateTime currentTime = DateTime.Now;

        // Calculate rotation based on the pointer type
        float angle = 0f;
        switch (pointerType)
        {
            case PointerType.Hour:
                angle = (currentTime.Hour % 12) * 30 + currentTime.Minute * 0.5f;  // 30 degrees per hour + 0.5 degrees per minute
                break;
            case PointerType.Minute:
                angle = currentTime.Minute * 6;  // 6 degrees per minute
                break;
            case PointerType.Second:
                angle = currentTime.Second * 6;  // 6 degrees per second
                break;
        }

        // Apply rotation to the pointer object
        transform.localRotation = Quaternion.Euler(0, angle, 0);
    }
}
