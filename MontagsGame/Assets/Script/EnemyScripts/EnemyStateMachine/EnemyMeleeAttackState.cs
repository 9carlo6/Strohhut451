using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyBaseState
{

    float distanceToTarget;
    Transform enemyTransform;
    Transform targetTransform;
    PlayerController playerController;
    GameObject playerRef;




    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato Nemico = Attacca");
        enemyTransform = enemy.gameObject.transform;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        targetTransform = playerRef.transform;


    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        distanceToTarget = Vector3.Distance(enemyTransform.position, targetTransform.position);


        if (distanceToTarget <= 1.5)
        {
            Attach();
        }
        else
        {
            enemy.SwitchState(enemy.ChasePlayerState);
        }
        //} 

    }

    public void Attach()
    {

        Debug.Log("ZAC ZAC");
        enemyTransform.LookAt(targetTransform);


    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
