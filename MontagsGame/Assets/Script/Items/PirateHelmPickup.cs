using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateHelmPickup : MonoBehaviour
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
        radioController.SetRadioText("The Helm is used to stun all enemies in play");

        //Suono
        FindObjectOfType<AudioManager>().Play("Pickup");

        //Per gestire l'aggiornamento dell'ammontare dei timoni posseduti
        lc.sc.helms_amount++;
        lc.UpdateGameItemsAmountText();

        Destroy(gameObject);
    }
}
