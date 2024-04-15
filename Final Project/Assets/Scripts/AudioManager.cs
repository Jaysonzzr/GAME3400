using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;

    private void Update()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!PauseMenuManager.isGamePaused)
            {
                audioSource.UnPause();
            }
            else
            {
                audioSource.Pause();
            }
        }
    }
}
