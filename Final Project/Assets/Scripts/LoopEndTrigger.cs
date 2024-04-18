using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopEndTrigger : MonoBehaviour
{
    bool triggered = false;

    public Color color;

    public GameObject realHallway;
    public GameObject[] deleteObjects;
    public GameObject oldCeiling;
    public GameObject newCeiling;
    public Transform player;

    public Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            LoopManager.unlockSafe = true;
            FindObjectOfType<TextDisplay>().textColor = color;
            interactable.PlayClip();

            realHallway.SetActive(true);

            oldCeiling.SetActive(false);
            newCeiling.SetActive(true);

            StartCoroutine(FadeShader(3));

            foreach (GameObject deleteObject in deleteObjects)
            {
                deleteObject.SetActive(false);
            }

            player.gameObject.GetComponent<PlayerController>().enabled = false;
            player.position = new Vector3(player.position.x + 168, player.position.y, player.position.z);
            Invoke("EnableController", 0.02f);

            triggered = true;
        }
    }
    
    void EnableController()
    {
        player.gameObject.GetComponent<PlayerController>().enabled = true;
    }

    private IEnumerator FadeShader(float duration)
    {
        ShaderEffect_BleedingColors shader = Camera.main.GetComponent<ShaderEffect_BleedingColors>();

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            shader.intensity = Mathf.Lerp(4f, 0f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        shader.intensity = 0;
    }
}
