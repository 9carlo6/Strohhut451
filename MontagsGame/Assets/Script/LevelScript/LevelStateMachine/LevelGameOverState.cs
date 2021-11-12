using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGameOverState : LevelBaseState
{
    public GameObject gameOverCanvas;

    public override void EnterState(LevelStateManager level)
    {
        Debug.Log("Stato Livello = Game Over");
        gameOverCanvas = level.gameObject.transform.Find("GameOverCanvas").gameObject;
        gameOverCanvas.SetActive(true);
    }

    public override void UpdateState(LevelStateManager level)
    {
        //Ricomincia il livello se viene premuto il tasto R
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Serve per aggiornare le info relative alla sessione
            level.UpdateSessionInfo();

            Debug.Log("Passaggio dallo stato game over allo stato iniziale del livello");

            gameOverCanvas.SetActive(false);

            //Per resettare i parametri
            level.lc.ParametersReset();

            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            //Per gestire il passaggio da uno stato all'altro quando si carica un livello
            level.StartCoroutine(level.LoadLevel(level.InitialState, levelIndex));
        }
    }

    public override void OnCollisionEnter(LevelStateManager level, Collision collision)
    {

    }
}
