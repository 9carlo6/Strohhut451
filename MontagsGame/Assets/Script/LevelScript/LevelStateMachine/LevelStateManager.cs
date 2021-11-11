using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//[HideInInspector]
public class LevelStateManager : MonoBehaviour
{
    LevelBaseState currentState;
    public LevelInitialPhaseState InitialState = new LevelInitialPhaseState();
    public LevelGameOverState GameOverState = new LevelGameOverState();
    public LevelCompletedState CompletedState = new LevelCompletedState();
    public LevelPauseState PauseState = new LevelPauseState();

    [HideInInspector]  public bool isLevelCompleted = false;
    [HideInInspector]  public GameObject sessionController;
    [HideInInspector]  public SessionController sc;
    [HideInInspector]  public GameObject levelController;
    [HideInInspector]  public LevelController lc;

    //Per gestire l'animazione della transizione tra un livello e un altro
    [HideInInspector] public float transitionTime = 1f;
    [HideInInspector] public Animator transition;

    //Per gestire il player
    [HideInInspector] public GameObject player;


    //Per individuare lo stato corrente del Livello
    public string getCurrentState()
    {
        return currentState.GetType().Name;
    }

    public void Awake()
    {
        transitionTime = 1f;
        transition = GameObject.FindWithTag("CrossfadeAnimation").GetComponent<Animator>();

        sessionController = GameObject.FindWithTag("SessionController");
        levelController = GameObject.FindWithTag("LevelController");
        lc = levelController.GetComponent<LevelController>();
        sc = sessionController.GetComponent<SessionController>();

        player = GameObject.FindWithTag("Player");
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
        //Quando si passa da uno stato all'altro viene fatto il reset del player
        player = GameObject.FindWithTag("Player");

        currentState = state;
        state.EnterState(this);
    }

    //Per ritornare al menu
    public void backToMenu()
    {
        //Serve per aggiornare le info relative alla sessione
        UpdateSessionInfo();

        StartCoroutine(LoadMenu());

        //Per uscire dall pausa se il gioco � in pausa
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

        //Questa � la funzione che permette di aggiornare le info
        sc.UpdateCurrentSceneData(
            scene_name,
            scene_restart_numbers,
            scene_coins,
            scene_score,
            scene_time,
            scene_is_completed);
    }

    //Per gestire l'animazione della transizione tra un livello e un altro
    IEnumerator LoadMenu()
    {
        this.transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime + 2);
        SceneManager.LoadScene("Menu");
    }

    //Per gestire il passaggio da uno stato all'altro quando si carica un livello
    public IEnumerator LoadLevel(LevelBaseState levelToSwitch, int levelIndex)
    {
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();

        SwitchState(levelToSwitch);
    }

    //Per il reset dei parametri quando si riavvia la scena
    public void ParametersReset()
    {
        lc.currentNumberOfEnemies = lc.valid_currentNumberOfEnemies;
        lc.NumberOfEnemiesCheck = lc.valid_currentNumberOfEnemies;
        lc.enemiesNumberText.GetComponent<TMP_Text>().text = new string('*', lc.valid_currentNumberOfEnemies);
        lc.levelPoints = lc.valid_levelPoints;
        lc.currentCoins = lc.valid_currentCoins;
        lc.levelTimeCounter = lc.valid_levelTimeCounter;
        lc.comboTimeCounter = 0;
        lc.comboMultiplier = 0;

    }

}
