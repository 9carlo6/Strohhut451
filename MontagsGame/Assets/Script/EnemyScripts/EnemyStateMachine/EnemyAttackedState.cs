using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackedState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato = Nemico Attaccato");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
