using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitialPhaseState : LevelBaseState
{
    private int initialNumberOfEnemies;
    private int currentNumberOfEnemies;
    private GameObject player;

    public override void EnterState(LevelStateManager level)
    {
        Debug.Log("Stato Livello = Fase Iniziale");

        initialNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        player = GameObject.FindWithTag("Player");

        Debug.Log("Numero di nemici iniziale nel livello = " + initialNumberOfEnemies);
    }

    public override void UpdateState(LevelStateManager level)
    {
        currentNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (currentNumberOfEnemies == 0 && player.GetComponent<PlayerHealthManager>().currentHealth > 0)
        {
            Debug.Log("Passaggio dallo stato iniziale del livello allo stato livello completato");
            level.SwitchState(level.CompletedState);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restart del livello");
            SceneManager.LoadScene("Level1");
        }

        if (player.GetComponent<PlayerHealthManager>().currentHealth <= 0)
        {
            Debug.Log("Passaggio dallo stato iniziale del livello allo stato game over");
            level.SwitchState(level.GameOverState);
        }
    }

    public override void OnCollisionEnter(LevelStateManager level, Collision collision)
    {

    }
}