using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SoundSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    private static readonly string firstPlay = "firstPlay";
    private static readonly string volumePref = "volumePref";
    private int firstPlayInt;
    public Slider slider;
    private float volumeFloat;

    //Play la canzone principale
    void Start()
    {
         firstPlayInt = PlayerPrefs.GetInt(firstPlay);

        if (firstPlayInt == 0)
         {
             volumeFloat = .00f;
             slider.value = volumeFloat;
             PlayerPrefs.SetFloat(volumePref, volumeFloat);
             PlayerPrefs.SetInt(firstPlay, -1);
         }
         else
         {
             volumeFloat = PlayerPrefs.GetFloat(volumePref);
             slider.value = volumeFloat;
         }


        //INSERENDO SOLO QUESTE DUE LINEE DI CODICE VIENE EVITATO IL SALVATAGGIO DEL SUONO QUANDO USCIAMO DAL GIOCO
        //PlayerPrefs.GetFloat(volumePref, volumeFloat);
        //slider.value = volumeFloat;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

        PlayerPrefs.SetFloat(volumePref, volume);
    }
}
