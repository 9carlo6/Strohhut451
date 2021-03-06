using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPauseState : LevelBaseState
{
    private GameObject pauseMenuCanvas;
    private GameObject OptionsMenuCanvas;
    private GameObject ControlsMenuCanvas;
    public bool gameIsPaused = false;

    public override void EnterState(LevelStateManager level)
    {
        Debug.Log("Stato Livello = Check Restart");

        pauseMenuCanvas = level.gameObject.transform.Find("PauseMenuCanvas").gameObject;
        pauseMenuCanvas.SetActive(true);
        OptionsMenuCanvas = level.gameObject.transform.Find("OptionsMenuCanvas").gameObject;
        ControlsMenuCanvas = level.gameObject.transform.Find("ControlsMenuCanvas").gameObject;

        gameIsPaused = true;
        level.player.GetComponent<PlayerController>().isStopped = true;

        //Per mettere il gioco in pausa
        pause();
    }

    public override void UpdateState(LevelStateManager level)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Serve per aggiornare le info relative alla sessione
            level.UpdateSessionInfo();

            Debug.Log("Passaggio dallo stato pause allo stato iniziale del livello con reload della scena");

            pauseMenuCanvas.SetActive(false);
            OptionsMenuCanvas.SetActive(false);
            ControlsMenuCanvas.SetActive(false);

            level.player.GetComponent<PlayerController>().isStopped = false;

            gameIsPaused = false;

            resume();

            //Per resettare i parametri
            level.lc.ParametersReset();

            int levelIndex = SceneManager.GetActiveScene().buildIndex;

            //Per gestire il passaggio da uno stato all'altro quando si carica un livello
            level.StartCoroutine(level.LoadLevel(level.InitialState, levelIndex));

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Passaggio dallo stato pause allo stato iniziale del livello senza reload della scena");

            pauseMenuCanvas.SetActive(false);
            OptionsMenuCanvas.SetActive(false);
            ControlsMenuCanvas.SetActive(false);

            level.player.GetComponent<PlayerController>().isStopped = false;

            gameIsPaused = false;

            resume();

            level.SwitchState(level.InitialState);
        }

        if(level.player != null)
        {
            if (level.player.GetComponent<PlayerHealthManager>().currentHealth <= 0)
            {
                Debug.Log("Passaggio dallo stato iniziale del livello allo stato game over");
                level.SwitchState(level.GameOverState);
            }
        }
    }

    public void pause()
    {
        Time.timeScale = 0f;
    }

    public void resume()
    {
        Time.timeScale = 1f;
    }

    public override void OnCollisionEnter(LevelStateManager level, Collision collision)
    {

    }
}