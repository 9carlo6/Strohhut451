using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] //In questo modo compare nell'inspector e possiamo inserire tutti i suoni
public class Sound
{
    //Clip
    public AudioClip audioClip;

    //Nome audio
    public string name;

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
    public AudioSource audioSource;
}

