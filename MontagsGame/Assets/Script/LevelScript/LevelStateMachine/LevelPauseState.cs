using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPauseState : LevelBaseState
{
    private PlayerInput playerInput;
    private GameObject pauseMenuCanvas;
    public static bool gameIsPaused = false;
    private GameObject player;

    public override void EnterState(LevelStateManager level)
    {
        Debug.Log("Stato Livello = Check Restart");
        player = GameObject.FindWithTag("Player");
        pauseMenuCanvas = level.gameObject.transform.Find("PauseMenuCanvas").gameObject;
        pauseMenuCanvas.SetActive(true);
        gameIsPaused = true;
        pause();
        player.GetComponent<PlayerController>().isStopped = true;
    }

    public override void UpdateState(LevelStateManager level)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Passaggio dallo stato pause allo stato iniziale del livello con reload della scena");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            pauseMenuCanvas.SetActive(false);
            player.GetComponent<PlayerController>().isStopped = false;
            gameIsPaused = false;
            resume();
            level.SwitchState(level.InitialState);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Passaggio dallo stato pause allo stato iniziale del livello senza reload della scena");
            pauseMenuCanvas.SetActive(false);
            player.GetComponent<PlayerController>().isStopped = false;
            gameIsPaused = false;
            resume();

            if(level.isLevelCompleted){
              level.SwitchState(level.CompletedState);
            }
            else
            {
              level.SwitchState(level.InitialState);
            }
        }

    }

    void pause(){
      Time.timeScale = 0f;
    }

    void resume(){
      Time.timeScale = 1f;
    }

    public override void OnCollisionEnter(LevelStateManager level, Collision collision)
    {

    }
}
