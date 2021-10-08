using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    public PlayerAliveState AliveState = new PlayerAliveState();
    public PlayerDeathState DeathState = new PlayerDeathState();
    public PlayerMeleeAttackState MeleeAttackState = new PlayerMeleeAttackState();
    public PlayerAttackedState AttackedState = new PlayerAttackedState();

    public void Start()
    {
        //Stato di partenza
        currentState = AliveState;
        //"this" � il contesto
        currentState.EnterState(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void Update()
    {
        //richiama qualsiasi logica presente nell'UpdateState dello stato corrente
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

}
