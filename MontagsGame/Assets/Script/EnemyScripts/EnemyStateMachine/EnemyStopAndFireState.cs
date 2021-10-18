using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStopAndFireState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato = Fermati e spara");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
