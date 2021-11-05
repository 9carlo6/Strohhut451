using UnityEngine.Audio;
using System;
using UnityEngine;

//In questa classe inseriamo tutti i suoni presenti nel gioco, gestiamo le proprietà degli stessi ed il metodo per riprodurre i suoni
public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        
        //Per fare in modo che persita tra le diverse scene, se ne venisse creato un duplicato lo eliminiamo con questo controllo 
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
            return;
        }

    //    DontDestroyOnLoad(gameObject);
        

        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.audioClip;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;

            s.audioSource.loop = s.loop;

        }

    }

    void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " Not Found");
            return;
        }

        s.audioSource.Play();
    }


}

//FindObjectOfType<AudioManager>().Play("Shot");
