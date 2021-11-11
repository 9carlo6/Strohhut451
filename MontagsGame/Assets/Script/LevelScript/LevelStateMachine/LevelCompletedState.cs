using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompletedState : LevelBaseState
{
	public GameObject levelCompletedCanvas;
	public bool isCollided;

	public override void EnterState(LevelStateManager level)
	{
		Debug.Log("Stato Livello = Livello completato");
		level.player.GetComponent<Rigidbody>().isKinematic = false;
		level.player.GetComponent<CapsuleCollider>().enabled = true;
		levelCompletedCanvas = level.gameObject.transform.Find("LevelCompletedCanvas").gameObject;
		levelCompletedCanvas.SetActive(true);
		level.isLevelCompleted = true;

		//Serve per aggiornare le info relative alla sessione
		level.UpdateSessionInfo();

		//La collisione tra il player e il pavimento � avventua e quindi settiamo il parametro a true
		isCollided = true;
	}

	public override void UpdateState(LevelStateManager level)
	{
	  if (isCollided && level.player.GetComponent<PlayerController>().nextLevelPlaneCollision)
	  {
			Debug.Log("Passaggio al livello successivo");
			//set di alcuni contatori a zero
			
			level.lc.comboTimeCounter = 0;
			level.lc.comboMultiplier = 0;
			level.lc.valid_levelPoints = level.lc.levelPoints;
			level.lc.valid_currentCoins = level.lc.currentCoins;
			level.lc.valid_levelTimeCounter = level.lc.valid_levelTimeCounter;

			//caricamento scena successiva
			level.StartCoroutine(LoadLevel(level, SceneManager.GetActiveScene().buildIndex + 1));
			//levelCompletedCanvas.SetActive(false);
			level.player.GetComponent<Rigidbody>().isKinematic = true;
			level.player.GetComponent<CapsuleCollider>().enabled = false;
			levelCompletedCanvas.SetActive(false);

			//set del parametro dei nunmero dei nemici
			level.lc.currentNumberOfEnemies = level.lc.valid_currentNumberOfEnemies;
			level.lc.NumberOfEnemiesCheck = level.lc.valid_currentNumberOfEnemies;

			//Qui viene settato a false per evitare di entrare continuamente nell'update
			isCollided = false;
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

	public override void OnCollisionEnter(LevelStateManager level, Collision collision)
	{

	}
}
