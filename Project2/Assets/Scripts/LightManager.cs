using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public List<Light> lights;
    public int interactionCount = 0;

    public void OnDoorInteraction()
    {
        interactionCount++;

        StartCoroutine(FlickerLightPairs());

        if (interactionCount % 2 == 0)
        {
            TurnOffRandomLightPair();
        }
    }

    public void TurnOffAllLights()
    {
        foreach (Light light in lights)
        {
            if (light != null)
            {
                light.gameObject.SetActive(false);
            }
        }
        lights.Clear();
    }

    IEnumerator FlickerLightPairs()
    {
        for (int i = 0; i < lights.Count; i += 2)
        {
            if (i < lights.Count - 1 && lights[i] != null && lights[i + 1] != null &&
                lights[i].gameObject.activeSelf && lights[i + 1].gameObject.activeSelf)
            {
                lights[i].enabled = false;
                lights[i + 1].enabled = false;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
                lights[i].enabled = true;
                lights[i + 1].enabled = true;
            }
        }
    }

    void TurnOffRandomLightPair()
    {
        int pairCount = lights.Count / 2;
        if (pairCount > 0)
        {
            int pairIndex = Random.Range(0, pairCount) * 2;
            if (lights[pairIndex] != null && lights[pairIndex + 1] != null)
            {
                lights[pairIndex].gameObject.SetActive(false);
                lights[pairIndex + 1].gameObject.SetActive(false);

                lights.RemoveAt(pairIndex);
                if (pairIndex < lights.Count)
                {
                    lights.RemoveAt(pairIndex);
                }
            }
        }
    }
}
