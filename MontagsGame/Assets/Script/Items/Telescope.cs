using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanFeatures;


public class Telescope : MonoBehaviour
{
    public PlayerController playerController;

    //Per gestire il testo
    public RadioController radioController;

    void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void EnableEffect()
    {
        // qua ci va un modificatore non una modifica alla feature ;
        playerController.increasedVisualField = true;

        ((playerController.features)[HumanFeature.FeatureType.FT_INCREASED_FOV]).currentValue = true;
    
    }

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        //Per gestire il testo
        radioController = GameObject.FindWithTag("RadioController").GetComponent<RadioController>();
        radioController.SetRadioText("The Telescope is used to increase the field of view. Press [SHIFT] to use it.");

        //Suono
        FindObjectOfType<AudioManager>().Play("Pickup");

        EnableEffect();
        Destroy(gameObject);
    }
}
