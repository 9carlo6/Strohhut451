using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAliveState : EnemyBaseState
{

    EnemyHealthManager enemyHealthManager;
    GameObject playerGameObject;



    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato Nemico = Vivo");
        enemyHealthManager = enemy.GetComponent<EnemyHealthManager>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");


    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if(enemyHealthManager.currentHealth <= 0)
        {
            enemy.SwitchState(enemy.DeathState);
        }
        else
        {
            if(playerGameObject.transform.GetComponent<PlayerHealthManager>().currentHealth > 0)
            {
                enemy.SwitchState(enemy.PatrollingState);

            }
        }
        
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
