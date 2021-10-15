using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{


    public bool run = true, stopped = false, melee = false;

    float distanceToTarget;
    Transform enemyTransform;
    Transform targetTransform;
    PlayerController playerController;


    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato Nemico = Attacca");
        enemyTransform = enemy.gameObject.transform;
        targetTransform = playerController.transform;

        //NON SO SE SI DEVE FARE QUI
        distanceToTarget = Vector3.Distance(enemyTransform.position, targetTransform.position);

    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (distanceToTarget <= 1.5)
        {
            run = false;
            stopped = false;
            melee = true;
            //agent.isStopped = true; 
        }
        else
        {
            if (distanceToTarget >= 4)
            {
                run = true;
                stopped = false;
                melee = false;

                //agent.isStopped = false; 
            }
            else
            {
                // fra 1.5 e 4 combatti 
                run = false;
                stopped = true;
                melee = false;
                //agent.isStopped = true; 
            }
        }
    //} 
   
}

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
