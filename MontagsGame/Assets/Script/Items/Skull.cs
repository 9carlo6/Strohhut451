using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    public GameObject[] enemies;

    [HideInInspector] public GameObject levelController;
    [HideInInspector] public LevelController lc;

    public void Awake()
    {
        levelController = GameObject.FindWithTag("LevelController");
        lc = levelController.GetComponent<LevelController>();
    }

    public void EnableEffect()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //Calcola un numero random da 0 al numero dei nemici -1
        var i = Random.Range(0, enemies.Length - 1);

        //Porta nello stato morto il nemico selezionato
        enemies[i].GetComponent<EnemyHealthManager>().currentHealth = 0;
    }
}
