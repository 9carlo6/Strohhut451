using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] //In questo modo compare nell'inspector e possiamo inserire tutti i suoni
public class Sound
{
    //Nome audio
    public string name;

    //Clip che vogliamo riprodurre
    public AudioClip audioClip;

    //Volume
    [Range(0f, 1f)]
    public float volume;

    //Tono
    [Range(.1f, 3f)]
    public float pitch;

    [Range(-1f, 1f)]
    public float panStereo;

    [Range(0f, 1f)]
    public float spatialBlend;

    [Range(0f, 1.1f)]
    public float reverbZoneMix;

    public bool loop;

    [HideInInspector]
    public AudioSource audioSource; //Non vogliamo che compaia nell'inspector perchè viene impostato nell'Awake dell'audioManager
}

