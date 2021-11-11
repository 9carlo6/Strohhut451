using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public PlayerController playerController;

    void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Pickup");

        playerController.increasedVisualField = true;
        playerController.features["increasedVisualField"].currentValue = true;
        Destroy(gameObject);
    }
}
