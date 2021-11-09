using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGameOverState : LevelBaseState
{
    private PlayerInput playerInput;
    private GameObject gameOverCanvas;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameOverCanvas.SetActive(false);
            level.SwitchState(level.InitialState);
        }
    }

    public override void OnCollisionEnter(LevelStateManager level, Collision collision)
    {

    }
}
