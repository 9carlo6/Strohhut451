using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    public PlayerAliveState AliveState = new PlayerAliveState();
    public PlayerDeathState DeathState = new PlayerDeathState();
    public PlayerAttackState AttackState = new PlayerAttackState();

    //Per individuare lo stato corrente del Player
    public string getCurrentState()
    {
        return currentState.GetType().Name;
    }
    
    public void Start()
    {
        //Stato di partenza
        currentState = AliveState;
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

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

}
