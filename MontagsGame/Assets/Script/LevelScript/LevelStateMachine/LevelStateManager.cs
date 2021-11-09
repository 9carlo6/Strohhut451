using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[HideInInspector] 
public class LevelStateManager : MonoBehaviour
{
    LevelBaseState currentState;
    public LevelInitialPhaseState InitialState = new LevelInitialPhaseState();
    public LevelGameOverState GameOverState = new LevelGameOverState();
    public LevelCompletedState CompletedState = new LevelCompletedState();
    public LevelPauseState PauseState = new LevelPauseState();

    public bool isLevelCompleted = false;
    public Animator transition;
    public GameObject sessionController;
    public SessionController sc;
    public GameObject levelController;
    public LevelController lc;

    //Per individuare lo stato corrente del Livello
    public string getCurrentState()
    {
        return currentState.GetType().Name;
    }

    public void Awake()
    {
        sessionController = GameObject.FindWithTag("SessionController");
        lc = levelController.GetComponent<LevelController>();
        sc = sessionController.GetComponent<SessionController>();
    }

    public void Start()
    {
        //Stato di partenza
        currentState = InitialState;
        //"this" e' il contesto
        currentState.EnterState(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void Update()
    {
        //Richiama qualsiasi logica presente nell'UpdateState dello stato corrente
        currentState.UpdateState(this);
    }

    public void SwitchState(LevelBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    //Per ritornare al menu
    public void backToMenu()
    {
        //Serve per aggiornare le info relative alla sessione
        UpdateSessionInfo();

        SceneManager.LoadScene("Menu");
        if (PauseState.gameIsPaused)
        {
            PauseState.resume();
        }
    }

    //Funzione che Serve per aggiornare le info relative alla sessione
    public void UpdateSessionInfo()
    {
        //Vengono recuperati le info dal LevelController e dal LevelStateManager
        string scene_name = SceneManager.GetActiveScene().name.ToString();
        int scene_restart_numbers = 1;
        int scene_coins = lc.currentCoins;
        int scene_score = lc.levelPoints;
        float scene_time = lc.levelTimeCounter;
        bool scene_is_completed = isLevelCompleted;

        //Questa è la funzione che permette di aggiornare le info
        sc.UpdateCurrentSceneData(
            scene_name,
            scene_restart_numbers,
            scene_coins,
            scene_score,
            scene_time,
            scene_is_completed);
    }
}
