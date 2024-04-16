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
    public bool reverseRotation = false;

    private void Update()
    {
        if (!PauseMenuManager.isGamePaused)
        {
            DateTime currentTime = DateTime.Now;

            float angle = 0f;
            switch (pointerType)
            {
                case PointerType.Hour:
                    angle = (currentTime.Hour % 12) * 30 + currentTime.Minute * 0.5f;
                    break;
                case PointerType.Minute:
                    angle = currentTime.Minute * 6;
                    break;
                case PointerType.Second:
                    angle = currentTime.Second * 6;
                    break;
            }

            if (reverseRotation)
            {
                angle = -angle;
            }

            transform.localRotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
