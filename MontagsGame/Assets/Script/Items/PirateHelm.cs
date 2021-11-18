using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateHelm : MonoBehaviour
{
    public GameObject[] enemies;
    private EnemyStateManager enemymanager;

    //Per gestire il testo
    public RadioController radioController;

    public void EnableEffect()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemymanager = enemy.GetComponent<EnemyStateManager>();
            enemymanager.SwitchState(enemymanager.StunnedState);
          
        }
      
    }

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        //Per gestire il testo
        radioController = GameObject.FindWithTag("RadioController").GetComponent<RadioController>();
        radioController.SetRadioText("The Helm is used to stun all enemies in play");

        //Suono
        FindObjectOfType<AudioManager>().Play("Pickup");

        EnableEffect();
        Destroy(gameObject);
    }
}
