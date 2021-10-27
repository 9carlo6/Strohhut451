using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletedState : LevelBaseState
{
    private GameObject player;
    private GameObject levelCompletedCanvas;

    //Per gestire l'animazione della transizione tra un livello e un altro
    public float transitionTime = 1f;

    public override void EnterState(LevelStateManager level)
    {
        Debug.Log("Stato Livello = Livello completato");
        player = GameObject.FindWithTag("Player");
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<CapsuleCollider>().enabled = true;
        levelCompletedCanvas = level.gameObject.transform.Find("LevelCompletedCanvas").gameObject;
        levelCompletedCanvas.SetActive(true);
        level.isLevelCompleted = true;
    }

    public override void UpdateState(LevelStateManager level)
    {
      if (player.GetComponent<PlayerController>().nextLevelPlaneCollision)
      {
          Debug.Log("Passaggio al livello successivo");
          level.StartCoroutine(LoadLevel(level, SceneManager.GetActiveScene().buildIndex + 1));
          levelCompletedCanvas.SetActive(false);
          level.isLevelCompleted = false;
          player.GetComponent<Rigidbody>().isKinematic = true;
          player.GetComponent<CapsuleCollider>().enabled = false;
      }

      if (Input.GetKeyDown(KeyCode.Escape))
      {
          Debug.Log("Passaggio dallo stato iniziale del livello allo stato pause");
          levelCompletedCanvas.SetActive(false);
          level.SwitchState(level.PauseState);
      }

    }

    //Per gestire l'animazione della transizione tra un livello e un altro
    IEnumerator LoadLevel(LevelStateManager level, int levelIndex){
      level.transition.SetTrigger("Start");
      yield return new WaitForSeconds(transitionTime);
      SceneManager.LoadScene(levelIndex);
    }

    public override void OnCollisionEnter(LevelStateManager level, Collision collision)
    {

    }
}
