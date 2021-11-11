using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateHelm : MonoBehaviour
{
    public GameObject[] enemies;
    private EnemyStateManager enemymanager;

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Pickup");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in enemies)
        {
            enemymanager = enemy.GetComponent<EnemyStateManager>();
            enemymanager.SwitchState(enemymanager.StunnedState);
        }

        Destroy(gameObject);
    }
}
