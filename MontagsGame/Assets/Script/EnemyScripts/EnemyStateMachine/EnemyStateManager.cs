using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    public EnemyAliveState AliveState = new EnemyAliveState();
    public EnemyDeathState DeathState = new EnemyDeathState();
    public EnemyAttackState AttackState = new EnemyAttackState();
    public EnemyAttackedState AttackedState = new EnemyAttackedState();
    public EnemyStunnedState StunnedState = new EnemyStunnedState();
    public EnemyPatrollingState PatrollingState = new EnemyPatrollingState();
    public EnemyChasePlayerState ChasePlayerState = new EnemyChasePlayerState();


    public void Start()
    {
        //Stato di partenza
        currentState = AliveState;
        //"this" è il contesto
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

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

}
