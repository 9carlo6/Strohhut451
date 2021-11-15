using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;


public class RadioController : MonoBehaviour
{
    //Per gestire il testo nella radio
    public TMP_Text radio_text;

    //Per gestire la grafica della radio
    public GameObject radio_montag;
    public GameObject radio_text_background;

    //Per gestire la durata della radio
    public float radioTimeCounter;
    public float radioDuration = 4.0f;

    void Awake()
    {
        radioTimeCounter = 0;
        HideRadio();
    }

    // Update is called once per frame
    void Update()
    {
        if(radioTimeCounter > 0)
        {
            radioTimeCounter -= Time.deltaTime;
        }
        else
        {
            HideRadio();
        }
    }

    public void SetRadioText(string text)
    {
        ActiveRadio();
        radio_text.text = text;
        radioTimeCounter = radioDuration;
    }

    public void ActiveRadio()
    {
        radio_montag.SetActive(true);
        radio_text_background.SetActive(true);
        radio_text.enabled = true;
    }

    public void HideRadio()
    {
        radio_montag.SetActive(false);
        radio_text_background.SetActive(false);
        radio_text.enabled = false;
    }
}
