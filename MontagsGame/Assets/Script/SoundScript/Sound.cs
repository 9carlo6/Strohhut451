using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] //In questo modo compare nell'inspector
public class Sound
{
    public AudioClip audioClip;

    public string name;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;
}

