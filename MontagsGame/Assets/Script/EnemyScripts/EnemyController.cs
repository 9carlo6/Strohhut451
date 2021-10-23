using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Questa classe serve per la gestione del'animazione del nemico
public class EnemyController : MonoBehaviour
{ 
    public Transform[] wayPoints;   //Array di points verso cui il nemico dovrà effettuare il patroling
    public LayerMask targetMask;    //Bersaglio, cioè il player
    public LayerMask obstructionMask; //Ostacoli, ad esempio le pareti

    //Per l'attacco Melee
    public Transform attackPoint;
    public float attackRange;
    public float meleeDamage = 1f;
    public float meleeDistance = 1.2f;
    public float fireDistance = 6f;

    //Per l'animazione
    public Animator animator;
    EnemyStateManager stateManager;

    NavMeshAgent enemyNavMeshAgent;  //NavMesh del nemico

    [HideInInspector] public bool ready = false;

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
        this.material[0].SetFloat("Vector_Intensity_Dissolve2", 0.4f);

    }

    // Start is called before the first frame update
    void Start()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        handleAnimation();
    }

    //Per gestire le animazioni
    void handleAnimation()
    {
        //Prende i parametri dall'animator
        bool attack = animator.GetBool("Attack");

        if (string.Equals(GetCurrentClipName(), "AttaccoDirettoNemico") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            //Se sta attaccando deve necessariamente finire l' animazione
            ready = false;
        }
        else
        {
            ready = true;
        }


        if (ready)
        {
            switch (stateManager.getCurrentState())
            {

                case "EnemyPatrollingState":
                    animator.SetBool("isWalkingEnemy", true);
                    animator.SetBool("Attack", false);
                    enemyNavMeshAgent.isStopped = false; ;
                    break;

                case "EnemyChasePlayerState":
                    animator.SetBool("isWalkingEnemy", true);
                    animator.SetBool("Attack", false);
                    enemyNavMeshAgent.isStopped = false; ;
                    break;

                case "EnemyMeleeAttackState":
                    animator.SetBool("isWalkingEnemy", false);
                    animator.SetBool("Attack", true);
                    //enemyNavMeshAgent.isStopped = true;
                    break;

                case "EnemyDeathState":
                    EnemyDeath();
                    break;

                default:

                    Debug.Log("animation error ");
                    break;
            }
        }
    }

    void EnemyDeath()
    {
        animator.SetBool("isDeathEnemy", true);
        enemyNavMeshAgent.isStopped = true;

        //I materiali del personaggio vengono settati al materiale con lo shader per la dissolvenza
        renderEnemyBody.sharedMaterials = material;

        if (string.Equals(GetCurrentClipName(), "MorteNemico") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            GameObject.Destroy(this.gameObject);
        }

        //Per gestire la dissolvenza durante la morte del MorteNemico
        if (string.Equals(GetCurrentClipName(), "MorteNemico"))
        {
            //Man mano che l'animazione va avanti l'intensita dello shader della dissolvenza aumenta di valore
            this.material[0].SetFloat("Vector_Intensity_Dissolve2", this.material[0].GetFloat("Vector_Intensity_Dissolve2") + 0.01f);
        }
    }

    //Funzione necessaria per risalire al nome dell'animazione corrente
    public string GetCurrentClipName()
    {
        clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        return clipInfo[0].clip.name;
    }
}
