using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    public GameObject[] enemies;

    //Per la gestione del numero di skull correnti
    [HideInInspector] public GameObject levelController;
    [HideInInspector] public LevelController lc;
    [HideInInspector] public PlayerController playerController;


    void Awake()
    {
        levelController = GameObject.FindWithTag("LevelController");
        lc = levelController.GetComponent<LevelController>();

    }

    public void EnableEffect()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (lc.sc.skulls_amount > 0 && !levelController.GetComponent<LevelStateManager>().getCurrentState().Equals("LevelPauseState") && !playerController.Death())
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");

                FindObjectOfType<AudioManager>().Play("DropItem");
                //Calcola un numero random da 0 al numero dei nemici -1
                var i = Random.Range(0, enemies.Length - 1);

                //Porta nello stato morto il nemico selezionato
                enemies[i].GetComponent<EnemyHealthManager>().currentHealth = 0;

                //Per gestire l'aggiornamento dell'ammontare dei teschi posseduti
                lc.sc.skulls_amount--;
                lc.UpdateGameItemsAmountText();
            }
            else
            {
                Debug.Log("Il giocatore non possiede Teschi");
            }
        }
    }
}
