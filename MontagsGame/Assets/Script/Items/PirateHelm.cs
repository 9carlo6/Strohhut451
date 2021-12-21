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
    [HideInInspector] public PlayerController playerController;


    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController");
        lc = levelController.GetComponent<LevelController>();

    }

    public void EnableEffect()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (playerController != null)
        {
            if (lc.sc.helms_amount > 0 && !lc.GetComponent<LevelStateManager>().getCurrentState().Equals("LevelPauseState") && !playerController.Death())
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                FindObjectOfType<AudioManager>().Play("DropItem");


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
}
