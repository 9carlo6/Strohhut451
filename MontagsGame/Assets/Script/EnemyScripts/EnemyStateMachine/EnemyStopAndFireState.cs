using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyStopAndFireState : EnemyBaseState
{
    Vector3 destinationVector = new Vector3();

    //Dobbiamo fare in modo da inseguire il personaggio e tenere conto degli ostacoli
    //LayerMask permette di specificare i layer da utilizzare in Physics.Raycast
    public LayerMask targetMask;    //bersagli, cio� il player
    public LayerMask obstructionMask; //ostacoli, ad esempio le pareti
    GameObject playerRef;

    PlayerController playerController;
    Transform target;
    NavMeshAgent agent;
    Transform enemyTransform;
    float distanceToTarget;

    public bool playerInSightRange;  //quando vedo il bersaglio = true
    public bool playerInAttackRange = false;
    bool isArmed = true;



    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato = Fermati e spara"); 
        playerRef = GameObject.FindGameObjectWithTag("Player");
        target = playerRef.transform;
        agent = enemy.GetComponent<NavMeshAgent>();
        enemyTransform = enemy.gameObject.transform;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        


        distanceToTarget = Vector3.Distance(enemyTransform.position, target.position);


        if (distanceToTarget <= 1.5f)
        {
            enemy.SwitchState(enemy.AttackMeleeState);

        }
        else if (distanceToTarget >= 4f)
        {
            enemy.SwitchState(enemy.ChasePlayerState);

        }
        else
        {
            Fire();
        }


    }

    public void Fire()
    {
        //spara
        Debug.Log(" PEM PEM ");
        enemyTransform.LookAt(target);


    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
