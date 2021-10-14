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

    NavMeshAgent agent;

    //variabili utilizzare per salvare informazioni relative al movimento del nemico
    Vector3 currentPosition;
    bool isMoving;
    float initialSpeed;
    


    void Awake()
    {
        animator = GetComponent<Animator>();

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
       // bool isWalkingEnemy = animator.GetBool("isWalkingEnemy");
        bool attack = animator.GetBool("Attack");


        if(distanceToTarget <= 2f)
        {
            animator.SetBool("isWalkingEnemy", false);

            if (!attack)
            {
                animator.SetBool("Attack", true);

            }
           
        }
        else 
        {
            if(animator.GetBool("Attack") == true)
            {
                Debug.Log("Gregorio è gay");

                agent.isStopped = true;

            }

            animator.SetBool("isWalkingEnemy", true);
            animator.SetBool("Attack", false);

        }

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

        if ( animator.GetBool("Attack") == true && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {

            //ferma il nemico quando incontra il player
            agent.isStopped = true;

        }
        else
        {
            agent.isStopped = false;

        }
        
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
