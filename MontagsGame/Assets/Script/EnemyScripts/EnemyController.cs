using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Questa classe serve per la gestione del'animazione del nemico
public class EnemyController : MonoBehaviour
{

    Transform target;

    private Rigidbody enemyRigidbody;
    public GameObject playerRef;

    //per l'animazione
    Animator animator;
    EnemyStateManager stateManager;

    NavMeshAgent agent;

    //variabili utilizzare per salvare informazioni relative al movimento del nemico
    Vector3 currentPosition;
    bool isMoving;
    float initialSpeed;

    public bool ready = false;



    void Awake()
    {
        animator = GetComponent<Animator>();
        stateManager = GetComponent<EnemyStateManager>();

    }

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        enemyRigidbody = GetComponent<Rigidbody>();
        currentPosition = transform.position;
        target = playerRef.transform;
        agent = GetComponent<NavMeshAgent>();
        initialSpeed = agent.speed;
    }

    //Per gestire le animazioni
    void handleAnimation()
    {



        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        //Prende i parametri dall'animator 
        //bool isWalkingEnemy = animator.GetBool("isWalkingEnemy"); 
        bool attack = animator.GetBool("Attack");
        /* 
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) 
        { 
            ready = true; 
            if (ia.run == true) 
            { 
                animator.SetBool("isWalkingEnemy", true); 
                animator.SetBool("Attack", false); 
 
            } 
            if (ia.melee == true) 
            { 
                animator.SetBool("isWalkingEnemy", false); 
                animator.SetBool("Attack", true); 
                agent.isStopped = true; 
 
            } 
            if (ia.stopped == true) 
            { 
                animator.SetBool("isWalkingEnemy", false); 
                animator.SetBool("Attack", false); 
 
            } 
        } 
        else 
        { 
            ready = false; 
 
            if(ia.melee == true || ia.stopped == true) 
            { 
                agent.isStopped = true; 
 
            } 
 
        } */

        //se muore muore anche prima dell animazione pronta  

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            ready = true;
        }
        else
        {
            ready = false;
        }
        if (ready)
        {
            if (stateManager.getCurrentState() == "EnemyPatrollingState" || stateManager.getCurrentState() == "EnemyChasePlayerState")
            {
                animator.SetBool("isWalkingEnemy", true);
                animator.SetBool("Attack", false);

            }
            else if  (stateManager.getCurrentState() == "EnemyMeleeAttackState")
            {
                animator.SetBool("isWalkingEnemy", false);
                animator.SetBool("Attack", true);
                agent.isStopped = true;

            }
            else if (stateManager.getCurrentState() == "EnemyStopAndFireState")
            {
                animator.SetBool("isWalkingEnemy", false);
                animator.SetBool("Attack", false);
                agent.isStopped = true;


            }
            else
            {
                Debug.Log("animation error ");

            }

        }




        /* 
                if(distanceToTarget>3.0f && distanceToTarget <= 8.0f) 
                { 
                    agent.isStopped = true; 

                } 

                else 
                { 
                    if (distanceToTarget <= 3f) 
                    { 

                        agent.isStopped = true; 


                        animator.SetBool("isWalkingEnemy", false); 

                        if (!attack) 
                        { 
                            animator.SetBool("Attack", true); 

                        } 

                    } 
                    else 
                    { 
                        if (animator.GetBool("Attack") == true) 
                        {agent.isStopped = true; 

                        } 


                        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) 
                        { 
                            animator.SetBool("isWalkingEnemy", true); 
                            animator.SetBool("Attack", false); 

                            agent.isStopped = false; 

                        } 




                    } 

                } 
                */

        /*if (!attack && distanceToTarget <= 2f) 
        { 
            animator.SetBool("isWalkingEnemy", false); 
            animator.SetBool("Attack", true); 

        }else if() 


            if (isMoving && !isWalkingEnemy) 
            { 
                animator.SetBool("isWalkingEnemy", true); 
            } 
            else if (!isMoving && isWalkingEnemy) 
            { 
                animator.SetBool("Attack", false); 

                animator.SetBool("isWalkingEnemy", false); 
            } 

        */
    }


    // Update is called once per frame
    void Update()
    {


        /*if(transform.position != currentPosition)
        {
            isMoving = true;
            currentPosition = transform.position;
        }
        else
        {
            isMoving = false;
        }
        */
        handleAnimation();




    }
}