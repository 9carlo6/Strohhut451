using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HideInInspector] public class LevelStateManager : MonoBehaviour
{
    LevelBaseState currentState;
    public LevelInitialPhaseState InitialState = new LevelInitialPhaseState();
    public LevelGameOverState GameOverState = new LevelGameOverState();
    public LevelCompletedState CompletedState = new LevelCompletedState();
    public LevelPauseState PauseState = new LevelPauseState();

    public bool isLevelCompleted = false;
    public Animator transition;

    //Per individuare lo stato corrente del Livello
    public string getCurrentState()
    {
        return currentState.GetType().Name;
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

}
