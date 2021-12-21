using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitialPhaseState : LevelBaseState
{
    private int initialNumberOfEnemies;
    private int currentNumberOfEnemies;

    public override void EnterState(LevelStateManager level)
    {
        Debug.Log("Stato Livello = Fase Iniziale");

        initialNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //Per gesitre gli aiuti in caso di troppe morti
        level.lc.LevelHelper();
    }

    public override void UpdateState(LevelStateManager level)
    {
        currentNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //Si fa un controllo sul personaggio
        if (currentNumberOfEnemies == 0 && level.player.GetComponent<PlayerHealthManager>().currentHealth > 0)
        {
            Debug.Log("Passaggio dallo stato iniziale del livello allo stato livello completato");
            level.SwitchState(level.CompletedState);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Passaggio dallo stato iniziale del livello allo stato pause");
            level.SwitchState(level.PauseState);
        }
        if (level.player != null)
        {
            if (level.player.GetComponent<PlayerHealthManager>().currentHealth <= 0)
            {
                Debug.Log("Passaggio dallo stato iniziale del livello allo stato game over");
                level.SwitchState(level.GameOverState);
            }
        }
    }

    public override void OnCollisionEnter(LevelStateManager level, Collision collision)
    {

    }
}