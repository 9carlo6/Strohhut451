using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCheckRestartState : LevelBaseState
{
    private PlayerInput playerInput;
    private GameObject checkRestartCanvas;

    public override void EnterState(LevelStateManager level)
    {
        Debug.Log("Stato Livello = Check Restart");
        checkRestartCanvas = level.gameObject.transform.Find("CheckRestartCanvas").gameObject;
        checkRestartCanvas.SetActive(true);
    }

    public override void UpdateState(LevelStateManager level)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Passaggio dallo stato check restart allo stato iniziale del livello con reload della scena");
            SceneManager.LoadScene("Level1");
            checkRestartCanvas.SetActive(false);
            level.SwitchState(level.InitialState);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Passaggio dallo stato check restart allo stato iniziale del livello senza reload della scena");
            checkRestartCanvas.SetActive(false);
            level.SwitchState(level.InitialState);
        }

    }

    public override void OnCollisionEnter(LevelStateManager level, Collision collision)
    {

    }
}