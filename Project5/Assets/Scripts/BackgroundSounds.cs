using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSounds : MonoBehaviour
{ 
    public AudioClip[] crewmates;

    public AudioClip crushing;

    public GameObject[] soundboxes;

    public GameObject mainSoundbox;
    private float counterCrush = 0f;
    private float counterCrew = 0f;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(crewmates[0], mainSoundbox.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        counterCrush += Time.deltaTime;
        counterCrew += Time.deltaTime;

        if (counterCrush > 15)
        {
            GameObject soundbox = soundboxes[Random.Range(0, soundboxes.Length - 1)];
            AudioSource.PlayClipAtPoint(crushing, soundbox.transform.position, 0.5f);
            counterCrush = 0;
        }

        if (counterCrew > 10)
        {
            AudioClip crew = crewmates[Random.Range(0, crewmates.Length)];
            AudioSource.PlayClipAtPoint(crew, mainSoundbox.transform.position, 2);
            counterCrew = 0;
        }
    }
}
