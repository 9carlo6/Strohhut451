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

    private AnimatorClipInfo[] clipInfo;


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

       if (stateManager.getCurrentState() == "EnemyDeathState")
        {
          animator.SetBool("isDeathEnemy", true);
          agent.isStopped = true;
           if (string.Equals(GetCurrentClipName(), "MorteNemico") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f){
             GameObject.Destroy(this.gameObject, 0.2f);
           }

        }



        if (!animator.GetBool("Attack") || animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
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
                agent.isStopped = false; ;

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


    }

    //Funzione necessaria per risalire al nome dell'animazione corrente
    public string GetCurrentClipName(){
      clipInfo = animator.GetCurrentAnimatorClipInfo(0);
      return clipInfo[0].clip.name;
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
