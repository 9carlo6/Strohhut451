using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject levelController;
    //public bool taking;

    private void Awake()
    {

    }
    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {

        FindObjectOfType<AudioManager>().Play("Coin");

        //Aumenta il numero di monete raccolte nel livello
        GameObject.FindWithTag("LevelController").GetComponent<LevelController>().currentCoins += 1;
        // taking = true;


        

        Destroy(gameObject);


    }

    
}
