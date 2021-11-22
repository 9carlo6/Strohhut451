using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullPickup : MonoBehaviour
{
    //Per gestire il testo
    public RadioController radioController;

    [HideInInspector] public GameObject levelController;
    [HideInInspector] public LevelController lc;

    public void Awake()
    {
        levelController = GameObject.FindWithTag("LevelController");
        lc = levelController.GetComponent<LevelController>();
    }

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        //Per gestire il testo
        radioController = GameObject.FindWithTag("RadioController").GetComponent<RadioController>();
        radioController.SetRadioText("The Skull is used to kill one of the enemies in the level.");

        //Suono
        FindObjectOfType<AudioManager>().Play("Pickup");

        //Per gestire l'aggiornamento dell'ammontare dei teschi posseduti
        lc.sc.skulls_amount++;
        lc.UpdateGameItemsAmountText();

        //EnableEffect();
        Destroy(gameObject);
    }
}
