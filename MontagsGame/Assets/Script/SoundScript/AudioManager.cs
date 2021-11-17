using UnityEngine.Audio;
using System;
using UnityEngine;

//In questa classe inseriamo tutti i suoni presenti nel gioco, gestiamo le proprietà degli stessi ed il metodo per riprodurre i suoni
public class AudioManager : MonoBehaviour
{
    //Definiamo un array di suoni a partire dalla classe Sound
    public Sound[] sounds;

    //Riferimento static all'istanza corrente dell'audioManager
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        //Per fare in modo che l'audio Manager persita tra le diverse scene e che ci sia una sola istanza di audioManager
        //Se non c'è l'audioManager nella scena corrente impostiamo l'istanza uguale a this (gameObject dell'audioManager)
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //In caso contrario andiamo a distruggere il gameObject
        else
        {
            Destroy(gameObject);
        //  return;
        }


        //Per ogni suono inserito andiamo ad aggiungere la proprietà relativa all'audioSource
        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();  //All'audioSource del suono i-esimo che stiamo guardando settiamo la componente AudioSource

            s.audioSource.clip = s.audioClip;

            s.audioSource.outputAudioMixerGroup = s.audioMixerGroup;

            s.audioSource.volume = s.volume;

            s.audioSource.pitch = s.pitch;

            s.audioSource.loop = s.loop;

            s.audioSource.panStereo = s.panStereo;

            s.audioSource.spatialBlend = s.spatialBlend;

            s.audioSource.reverbZoneMix = s.reverbZoneMix;

            s.audioSource.minDistance = s.minDistance;

            s.audioSource.maxDistance = s.maxDistance;
        }
    }

    //Play la canzone principale
    void Start()
    {
        Play("SoundTrack");
    }

    public void Play(string name)
    {
        //Scorriamo tutti i suoni presenti nell'array, parametri -> Nome array, restituirci il suono tale che il nome del suono è uguale al nome passato come parametro del metodo
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " Not Found");
            return;
        }

        s.audioSource.Play();
    }

    public void PlayDelayed(string name, float time)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " Not Found");
            return;
        }

        s.audioSource.PlayDelayed(time);
    }


    public void Stop(String name)
    {
        //Scorriamo tutti i suoni presenti nell'array, parametri -> Nome array, restituirci il suono tale che il nome del suono è uguale al nome passato come parametro del metodo
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " Not Found");
            return;
        }

        s.audioSource.Stop();
    }
}