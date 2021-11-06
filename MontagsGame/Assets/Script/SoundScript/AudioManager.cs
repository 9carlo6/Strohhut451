using UnityEngine.Audio;
using System;
using UnityEngine;

//In questa classe inseriamo tutti i suoni presenti nel gioco, gestiamo le propriet� degli stessi ed il metodo per riprodurre i suoni
public class AudioManager : MonoBehaviour
{
    //Definiamo un array di suoni a partire dalla classe Sound
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        //Per fare in modo che l'audio Manager persita tra le diverse scene
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  //Non distrugge il gameObject quando carica una nuova scena
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);
        

        //Per ogni suono inserito andiamo ad aggiungere la propriet� relativa all'audioSource
        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.audioClip;

            s.audioSource.volume = s.volume;

            s.audioSource.pitch = s.pitch;

            s.audioSource.loop = s.loop;

            s.audioSource.panStereo = s.panStereo;

            s.audioSource.spatialBlend = s.spatialBlend;

            s.audioSource.reverbZoneMix = s.reverbZoneMix;

        }

    }

    //Play la canzone principale
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
