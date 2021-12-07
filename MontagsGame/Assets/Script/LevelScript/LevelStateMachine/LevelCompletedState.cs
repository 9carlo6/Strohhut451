using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompletedState : LevelBaseState
{
	public GameObject levelCompletedCanvas;
	public bool readyToGo;

	public GameObject[] traps;

	public override void EnterState(LevelStateManager level)
	{
		Debug.Log("Stato Livello = Livello completato");

		level.player.GetComponent<Rigidbody>().isKinematic = false;
		level.player.GetComponent<CapsuleCollider>().enabled = true;

		traps = GameObject.FindGameObjectsWithTag("Trap");

		hideTrap();

		levelCompletedCanvas = level.gameObject.transform.Find("LevelCompletedCanvas").gameObject;
		levelCompletedCanvas.SetActive(true);

		level.isLevelCompleted = true;

		//Serve per aggiornare le info relative alla sessione
		level.UpdateSessionInfo();

		//Il livello e' completato
		readyToGo = true;
	}

	public override void UpdateState(LevelStateManager level)
	{
	  if (readyToGo && level.player.GetComponent<PlayerController>().nextLevelPlaneCollision)
	  {
			Debug.Log("Passaggio al livello successivo");

			//Per il reset dei parametri quando si completa il livello
			level.lc.LevelCompletedParametersReset();

			//caricamento scena successiva
			level.StartCoroutine(LoadLevel(level, SceneManager.GetActiveScene().buildIndex + 1));

			level.player.GetComponent<Rigidbody>().isKinematic = true;
			level.player.GetComponent<CapsuleCollider>().enabled = false;

			levelCompletedCanvas.SetActive(false);

			//set del parametro dei nunmero dei nemici
			level.lc.currentNumberOfEnemies = level.lc.valid_currentNumberOfEnemies;
			level.lc.NumberOfEnemiesCheck = level.lc.valid_currentNumberOfEnemies;

			//Qui viene settato a false per evitare di entrare continuamente nell'update
			readyToGo = false;

			level.isLevelCompleted = false;
		}

	  if (Input.GetKeyDown(KeyCode.Escape))
	  {
		  Debug.Log("Passaggio dallo stato iniziale del livello allo stato pause");

		  levelCompletedCanvas.SetActive(false);

		  level.SwitchState(level.PauseState);
	  }

		if (level.player.GetComponent<PlayerHealthManager>().currentHealth <= 0)
		{
			Debug.Log("Passaggio dallo stato iniziale del livello allo stato game over");
			level.SwitchState(level.GameOverState);
		}
	}

	//Per gestire l'animazione della transizione tra un livello e un altro
	IEnumerator LoadLevel(LevelStateManager level, int levelIndex){
		level.transition.SetTrigger("Start");
		yield return new WaitForSeconds(level.transitionTime);

		// Start loading the scene
		AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
		// Wait until the level finish loading
		while (!asyncLoadLevel.isDone)
			yield return null;
		// Wait a frame so every Awake and Start method is called
		yield return new WaitForEndOfFrame();
		level.transition.SetTrigger("End");
		yield return new WaitForSeconds(level.transitionTime);

		level.SwitchState(level.InitialState);
	}

	public void hideTrap()
	{
		foreach (GameObject trap in traps)
		{
			trap.SetActive(false);
		}
	}

	public override void OnCollisionEnter(LevelStateManager level, Collision collision)
	{

	}
}