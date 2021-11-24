using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateHelm : MonoBehaviour
{
    public GameObject[] enemies;
    private EnemyStateManager enemymanager;

    //Per la gestione del numero di skull correnti
    [HideInInspector] public GameObject levelController;
    [HideInInspector] public LevelController lc;

    void Awake()
    {
        levelController = GameObject.FindWithTag("LevelController");
        lc = levelController.GetComponent<LevelController>();
    }

    public void EnableEffect()
    {
        if (lc.sc.helms_amount > 0)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                enemymanager = enemy.GetComponent<EnemyStateManager>();

                if (enemymanager.getCurrentState() != "EnemyStunnedState")
                {
                    enemymanager.SwitchState(enemymanager.StunnedState);
                }
            }

            //Per gestire l'aggiornamento dell'ammontare dei timoni posseduti
            lc.sc.helms_amount--;
            lc.UpdateGameItemsAmountText();
        }
        else
        {
            Debug.Log("Il giocatore non possiede Timoni");
        }
    }
}
