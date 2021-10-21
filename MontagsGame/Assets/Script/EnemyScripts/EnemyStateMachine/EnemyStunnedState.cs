using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnedState : EnemyBaseState
{
    EnemyHealthManager enemyHealthManager;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato = Nemico stordito");
        enemyHealthManager = enemy.GetComponent<EnemyHealthManager>();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
      if(enemyHealthManager.currentHealth <= 0)
      {
          enemy.SwitchState(enemy.DeathState);
      }
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
