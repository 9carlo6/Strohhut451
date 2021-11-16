using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    public GameObject[] enemies;

    //Per gestire il testo
    public RadioController radioController;

    public void EnableEffect()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //Calcola un numero random da 0 al numero dei nemici -1
        var i = Random.Range(0, enemies.Length - 1);

        //Porta nello stato morto il nemico selezionato
        enemies[i].GetComponent<EnemyHealthManager>().currentHealth = 0;
    }

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        //Per gestire il testo
        radioController = GameObject.FindWithTag("RadioController").GetComponent<RadioController>();
        radioController.SetRadioText("The Skull is used to kill one of the enemies in the level.");

        //Suono
        FindObjectOfType<AudioManager>().Play("Pickup");

        EnableEffect();
        Destroy(gameObject);
    }
}
