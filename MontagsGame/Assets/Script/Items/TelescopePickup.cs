using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanFeatures;


public class TelescopePickup : MonoBehaviour
{
    //Per gestire il testo
    public RadioController radioController;

    [HideInInspector] public GameObject levelController;
    [HideInInspector] public LevelController lc;

    void Awake()
    {
        levelController = GameObject.FindWithTag("LevelController");
        lc = levelController.GetComponent<LevelController>();
    }

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        //Per gestire il testo
        radioController = GameObject.FindWithTag("RadioController").GetComponent<RadioController>();
        radioController.SetRadioText("The Telescope is used to increase the field of view. Press [SHIFT] to use it.");

        //Suono
        FindObjectOfType<AudioManager>().Play("Pickup");

        //Per gestire l'aggiornamento dell'ammontare dei telescopi posseduti
        lc.sc.telescopes_amount++;
        lc.UpdateGameItemsAmountText();

        Destroy(gameObject);
    }
}
