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

    //Per modificare i materiali dei figli runtime
  	public Material[] material;
  	private GameObject enemyBody;
  	public Renderer renderEnemyBody;


    void Awake()
    {
        animator = GetComponent<Animator>();
        stateManager = GetComponent<EnemyStateManager>();

        //Inizio - Componenti dei Figli
    		enemyBody = transform.Find("EnemyPirateSkin").gameObject;
    		renderEnemyBody = enemyBody.GetComponent<Renderer>();
    		//Fine - Componenti dei Figli

        //Per gestire il passaggio allo shader per la dissolvenza
        //L'intensita dello shader per la dissolvenza viene settato inizialmente a 0.3
        this.material[0].SetFloat("Vector_Intensity_Dissolve2", 0.3f);

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


        //MORTE
       if (stateManager.getCurrentState() == "EnemyDeathState")
        {
          animator.SetBool("isDeathEnemy", true);
          agent.isStopped = true;

          //I materiali del personaggio vengono settati al materiale con lo shader per la dissolvenza
          renderEnemyBody.sharedMaterials = material;


           if (string.Equals(GetCurrentClipName(), "MorteNemico") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f){
             GameObject.Destroy(this.gameObject);
           }

           //Per gestire la dissolvenza durante la morte del MorteNemico
           if(string.Equals(GetCurrentClipName(), "MorteNemico")){
             //Man mano che l'animazione va avanti l'intensita dello shader della dissolvenza aumenta di valore
             this.material[0].SetFloat("Vector_Intensity_Dissolve2", this.material[0].GetFloat("Vector_Intensity_Dissolve2") + 0.005f);
           }

        }



        if (!animator.GetBool("Attack") || animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            // se sta attaccando deve necessariamente finire l' animazione
            ready = true;
        }
        else
        {
            ready = false;
        }
        if (ready)
        {
            

            // piu elegante? 

            switch (stateManager.getCurrentState())
            {

                case "EnemyPatrollingState":
                    animator.SetBool("isWalkingEnemy", true);
                    animator.SetBool("Attack", false);
                    agent.isStopped = false; ;
                    break;

                case "EnemyChasePlayerState":
                    animator.SetBool("isWalkingEnemy", true);
                    animator.SetBool("Attack", false);
                    agent.isStopped = false; ;
                    break;

                case "EnemyMeleeAttackState":
                    animator.SetBool("isWalkingEnemy", false);
                    animator.SetBool("Attack", true);
                    agent.isStopped = true;
                    break;

                default:

                    Debug.Log("animation error ");
                    break;



            }





            /*
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
            */
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
