using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato = Nemico morto");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
