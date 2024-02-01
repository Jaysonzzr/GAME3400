using UnityEngine;
using UnityEngine.UI;

public class DistanceBasedTextVisibility : MonoBehaviour
{
    public Transform cameraTransform;
    public Text text;
    public float fadeStartDistance = 10f;
    public float fadeEndDistance = 20f;

    void Update()
    {
        if (text != null && cameraTransform != null)
        {
            float distance = Vector3.Distance(transform.position, cameraTransform.position);

            if (distance > fadeStartDistance)
            {
                float alpha = Mathf.InverseLerp(fadeEndDistance, fadeStartDistance, distance);
                Color color = text.color;
                color.a = alpha;
                text.color = color;
            }
            else
            {
                Color color = text.color;
                if (color.a != 1)
                {
                    color.a = 1;
                    text.color = color;
                }
            }
        }
    }
}
