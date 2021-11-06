﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyChasePlayerState : EnemyBaseState
{
    EnemyHealthManager enemyHealthManager;

    public float viewRadius;  //raggio di vista
    public float viewAngle;   //angolo di vista
    private float offsetPosition = 0.3f;    //per determinare la distanza minima dal player
    Vector3 destinationVector = new Vector3();  //per determinare la posizione target da raggiungere

    float meleeDistance; //distanza da quale fare l'attacco melee
    float fireDistance; //distanza massima dalla quale il nemico continua a sparare

    //Dobbiamo fare in modo da inseguire il personaggio e tenere conto degli ostacoli
    //LayerMask permette di specificare i layer da utilizzare in Physics.Raycast
    public LayerMask targetMask;    //bersagli, cioe' il player
    public LayerMask obstructionMask; //ostacoli, ad esempio le pareti

    GameObject playerGameObject;
    GameObject enemyGameObject;
    NavMeshAgent enemyNavMesh;
    private Animator enemyAnimator;
    EnemyController enemyController;

    float agentAcceleration = 1.7f;


    EnemyWeaponController weaponController;

    //ALERT
    public bool playerFire;  //Viene utilizzata per l'alert
    public bool fireInHearRange;

    float distanceToTarget;

    public bool playerInSightRange;  //quando vedo il bersaglio = true

    public bool ingaged = false;


    

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato Nemico = Chasing");

        viewRadius = 10;
        viewAngle = 270;
        playerInSightRange = true;

        meleeDistance = enemy.GetComponent<EnemyController>().meleeDistance;
        fireDistance = enemy.GetComponent<EnemyController>().fireDistance;
        enemyController = enemy.GetComponent<EnemyController>();


        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
        }

        enemyNavMesh = enemy.GetComponent<NavMeshAgent>();

        enemyNavMesh.speed = 2f;

        if (enemyController.enemyWeapon != null)
        {
            enemyController.enemyWeapon.SetActive(true);
        }


        enemyGameObject = enemy.GetComponent<EnemyController>().gameObject;
        targetMask = enemy.GetComponent<EnemyController>().targetMask;
        obstructionMask = enemy.GetComponent<EnemyController>().obstructionMask;
        enemyHealthManager = enemy.GetComponent<EnemyHealthManager>();
        enemyAnimator = enemy.GetComponent<Animator>();
        weaponController = enemy.GetComponentInChildren<EnemyWeaponController>();

       
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        if (playerGameObject != null)
        {
            playerFire = playerGameObject.GetComponent<PlayerController>().getBoolToAlert();


           

            if (playerFire)
            {
                if (Vector3.Distance(enemyGameObject.transform.position, playerGameObject.transform.position) <= 12f)

                    fireInHearRange = true;
                else
                {
                    fireInHearRange = false;
                }
            }

        }

        //aumenta la velocità del nemico progressivamente
        if (enemyNavMesh.speed <= 6.5f)
        {

            enemyNavMesh.speed = enemyNavMesh.speed + Time.deltaTime * agentAcceleration;
        }
       
        if (playerGameObject.transform.GetComponent<PlayerHealthManager>().currentHealth <= 0)
        {
            enemy.SwitchState(enemy.AliveState);

        }
        else
        {
            distanceToTarget = Vector3.Distance(enemyGameObject.transform.position, playerGameObject.transform.position);

            FieldOfViewCheck();

            if (!playerInSightRange && !ingaged && !fireInHearRange)
            {
                enemy.SwitchState(enemy.PatrollingState);
            }
            else
            {
                if (distanceToTarget <= meleeDistance)
                {
                    
                    enemy.SwitchState(enemy.AttackMeleeState);

                }
                else
                {
                    ChasePlayer(distanceToTarget);
                }
            }

            //Gestione passaggio allo stato Stunned del nemico
            if (enemyAnimator.GetBool("isStunned"))
            {
                enemy.SwitchState(enemy.StunnedState);
            }

            if (enemyHealthManager.currentHealth <= 0)
            {
                enemy.SwitchState(enemy.DeathState);
            }
        }
        
    }

    //FOV
    private void FieldOfViewCheck()
    {
        //Inizializziamo un'array con tutti i collider che toccano o sono dentro la sfera con i parametri passati
        //Centro della sfera, raggio, layer di collider da includere nella query
        Collider[] rangeChecks = Physics.OverlapSphere(enemyGameObject.transform.position, viewRadius, targetMask);

        //Se qualcosa collide andiamo in questo if, quindi la lunghezza dell'array sar� diversa da zero
        //Qui basta un semplice if perch� sul layer targetMask c'� solo il player, altrimenti avremmo dovuto fare un for per scorrere l'array
        if (rangeChecks.Length != 0)
        {
            //targetTransform sarà pari alla prima istanza di rangeChecks, cioè la trasform del player
            Transform targetTransform = rangeChecks[0].transform;

            //Definiamo la direzione verso cui il nostro nemico sta guardando
            //Differenza tra la posizione del player (prima istanza del Collider[]) e il nemico
            Vector3 directionToTarget = (targetTransform.position - enemyGameObject.transform.position).normalized; //normalizzato tra 0 e 1

            //Transform.forward ritorna un vettore normalizzato rappresentante l'asse z
            //Quindi verifichiamo se l'angolo tra questi due vettori � minore dell'angolo di visuale fratto 2
            if (Vector3.Angle(enemyGameObject.transform.forward, directionToTarget) < viewAngle / 2)
            {
                //Distanza tra la posizione del nemico e quella del player
                float distanceToTarget = Vector3.Distance(enemyGameObject.transform.position, targetTransform.position);

                //Col RayCast è come se dotassimo il nemico di un occhio, avviene il lancio di un raggio
                //Parametri: origine del raggio, direzione, distanza massima che il raggio deve controllare per le collisioni
                //Maschera di livello utilizzata per ignorare selettivamente i Collider durante la proiezione di un raggio
                //Il RayCast termina nel momento in cui colpisce qualcosa nell'obstrunctionMask (tipo i muri)
                //Facciamo prima il controllo positivo col !, quindi se non stiamo colpendo qualcosa nell'obstructionMask
                if (!Physics.Raycast(enemyGameObject.transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    playerInSightRange = true;
                else
                    playerInSightRange = false;
            }
            else
                playerInSightRange = false;
        }
        //fallisce il controllo se il player non è alla portata, non si trova nemmeno nel raggio
        else
            playerInSightRange = false;
    }


    private void ChasePlayer(float distance)
    {
        //si ferma poco prima del player
        destinationVector.x = playerGameObject.transform.position.x;
        destinationVector.y = playerGameObject.transform.position.y;
        destinationVector.z = playerGameObject.transform.position.z + offsetPosition;

        //Raggiunge la posizione del player, target � il transform del player
        enemyNavMesh.SetDestination(destinationVector);

        // Debug.Log("vado qui : "+ destinationVector.x.ToString() + "-"+destinationVector.y.ToString() + "-"+destinationVector.z.ToString() + "-");

        //Il nemico si gira verso il player
        enemyGameObject.transform.LookAt(playerGameObject.transform);

        // funzione di sparo con precisione in funzione della distanza

        if(distance <= fireDistance && enemyGameObject.name.Contains("Armato"))
        {
            Fire();
        }

    }


    public void Fire()
    {
        weaponController.StartFiring();
        // qui funzione di sparo con calcolo precisione e spawn del proiettile,
        // ovviamente questa viene invocata ogni frame , quindi va gestito il fatto che si spara ogni secondo o mezzo secondo non 30 volte al secondo
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }


}
