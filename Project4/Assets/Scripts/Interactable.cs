using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string[] textToDisplay;

    public void DisplayText(GameObject hitObject)
    {
        FindObjectOfType<TextDisplay>().DisplayText(textToDisplay, hitObject);
    }
}
