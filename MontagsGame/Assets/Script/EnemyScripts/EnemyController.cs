﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using System.IO;

//Questa classe serve per la gestione del'animazione del nemico
public class EnemyController : MonoBehaviour
{
    public Transform[] wayPoints;   //Array di points verso cui il nemico dovrà effettuare il patroling
    public LayerMask targetMask;    //Bersaglio, cioè il player
    public LayerMask obstructionMask; //Ostacoli, ad esempio le pareti

    public AudioSource deathClip;
    public AudioSource punchClip;



    //Per l'attacco
    public GameObject enemyWeapon;
    public Transform attackPoint;
    public float attackRange;
    public float meleeDamage = 1f;
    public float meleeDistance = 1.2f;
    public float fireDistance = 6f;

    //Per l'animazione
    public Animator animator;
    [HideInInspector] public EnemyStateManager stateManager;

    NavMeshAgent enemyNavMeshAgent;  //NavMesh del nemico

    [HideInInspector] public bool ready = false;

    private AnimatorClipInfo[] clipInfo;

    //Per modificare i materiali dei figli runtime
  	public Material[] material;
  	private GameObject enemyBody;
  	public Renderer renderEnemyBody;

    //Per l'animazione dell'arma
    public Rig aimLayer;

    float aimDuration = 0.3f;

    public float acceleration = 0.3f;
    public float deceleration = 0.3f;
    float velocity = 0.4f;
    int velocityHash;

    //Per Gestire lo shader corrente
    public Shader baseEnemyShader;
    public Shader stunnedEnemyShader;

    //Per gestire le feature
  	public Dictionary<string, EnemyFeature> features;
  	private EnemyFeaturesJsonMap enemyMapper;

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

        //Inizio - Inizializzazione delle feature
    		string fileString = new StreamReader("Assets/Push-To-Data/Feature/Enemy/enemy_features.json").ReadToEnd();
    		enemyMapper = JsonUtility.FromJson<EnemyFeaturesJsonMap>(fileString);

    		features = new Dictionary<string, EnemyFeature>();
            
            features.Add("viewRadius", new EnemyFeature(enemyMapper.FT_VIEW_RADIUS, EnemyFeature.FeatureType.FT_VIEW_RADIUS));
            features.Add("viewAnglePatrolling", new EnemyFeature(enemyMapper.FT_VIEW_ANGLE_PATROLLING, EnemyFeature.FeatureType.FT_VIEW_ANGLE_PATROLLING));
            features.Add("viewAngleChasing", new EnemyFeature(enemyMapper.FT_VIEW_ANGLE_CHASING, EnemyFeature.FeatureType.FT_VIEW_ANGLE_CHASING));

            features.Add("velocity", new EnemyFeature(enemyMapper.FT_VELOCITY , EnemyFeature.FeatureType.FT_VELOCITY));
    		features.Add("acceleration", new EnemyFeature(enemyMapper.FT_ACCELERATION , EnemyFeature.FeatureType.FT_ACCELERATION));
    		features.Add("deceleration", new EnemyFeature(enemyMapper.FT_DECELERATION , EnemyFeature.FeatureType.FT_DECELERATION));
    		features.Add("health", new EnemyFeature(enemyMapper.FT_HEALTH , EnemyFeature.FeatureType.FT_HEALTH));
    		features.Add("meleeRange", new EnemyFeature(enemyMapper.FT_MELEE_RANGE , EnemyFeature.FeatureType.FT_MELEE_RANGE));
    		features.Add("meleeDamage", new EnemyFeature(enemyMapper.FT_MELEE_DAMAGE , EnemyFeature.FeatureType.FT_MELEE_DAMAGE));
    		features.Add("isWeaponed", new EnemyFeature(enemyMapper.FT_IS_WEAPONED , EnemyFeature.FeatureType.FT_IS_WEAPONED));
    		features.Add("fireDistance", new EnemyFeature(enemyMapper.FT_FIRE_DISTANCE , EnemyFeature.FeatureType.FT_FIRE_DISTANCE));
    		//Fine - Inizializzazione delle feature
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        if (enemyWeapon != null)
        {
            animator.SetFloat("isWeapon", 1);
            animator.SetFloat("isRunning", 1f);
            velocityHash = Animator.StringToHash("isRunningWithWeapon");
        }
        else
        {
            animator.SetFloat("isWeapon", 0f);
            animator.SetFloat("isRunning", 0f);
            velocityHash = Animator.StringToHash("isRunningWithoutWeapon");

        }
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
        if (true)
        {
            switch (stateManager.getCurrentState())
            {

                case "EnemyPatrollingState":

                    if (aimLayer != null)
                    {
                        aimLayer.weight -= Time.deltaTime / aimDuration;
                    }
                    if (velocity >= 0.2f)
                    {

                        velocity -= Time.deltaTime * deceleration;

                    }
                    animator.SetFloat(velocityHash, velocity);
                    animator.SetBool("isWalkingEnemy", true);
                    animator.SetBool("Attack", false);
                    animator.SetBool("isStunned", false);
                    animator.SetBool("waypointReached", false);
                    enemyNavMeshAgent.isStopped = false;

                    //Per gestire lo shader
                    renderEnemyBody.material.shader = baseEnemyShader;
                    break;

                case "EnemyCheckState":
                    animator.SetBool("isWalkingEnemy", false);
                    animator.SetBool("Attack", false);
                    animator.SetBool("isStunned", false);
                    animator.SetBool("waypointReached", true);
                    enemyNavMeshAgent.isStopped = true;

                    //Per gestire lo shader
                    renderEnemyBody.material.shader = baseEnemyShader;
                    break;

                case "EnemyChasePlayerState":

                    if (aimLayer != null)
                    {
                        aimLayer.weight += Time.deltaTime / aimDuration;
                    }
                    if (velocity <= 0.5f)
                    {
                        velocity += Time.deltaTime * acceleration;
                    }

                    animator.SetFloat(velocityHash, velocity);

                    animator.SetBool("isWalkingEnemy", true);
                    animator.SetBool("isStunned", false);
                    animator.SetBool("Attack", false);
                    animator.SetBool("waypointReached", false);
                    enemyNavMeshAgent.isStopped = false;

                    //Per gestire lo shader
                    renderEnemyBody.material.shader = baseEnemyShader;
                    break;

                case "EnemyMeleeAttackState":
                    animator.SetBool("isWalkingEnemy", true);
                    animator.SetBool("Attack", true);
                    animator.SetBool("isStunned", false);
                    animator.SetBool("waypointReached", false);
                    enemyNavMeshAgent.isStopped = false;

                    //Per gestire lo shader
                    renderEnemyBody.material.shader = baseEnemyShader;
                    break;

                case "EnemyStunnedState":
                    animator.SetBool("isWalkingEnemy", false);
                    animator.SetBool("Attack", false);
                    animator.SetBool("isStunned", true);
                    animator.SetBool("waypointReached", false);
                    enemyNavMeshAgent.isStopped = true;

                    //Per gestire lo shader
                    renderEnemyBody.material.shader = stunnedEnemyShader;
                    break;

                case "EnemyAliveState":
                    animator.SetBool("isWalkingEnemy", false);
                    animator.SetBool("Attack", false);
                    animator.SetBool("isStunned", false);
                    animator.SetBool("waypointReached", false);
                    enemyNavMeshAgent.isStopped = true;

                    //Per gestire lo shader
                    renderEnemyBody.material.shader = baseEnemyShader;
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

    public void PlaySoundDeath()
    {
        deathClip.Play();
    }

    public void PlaySoundPunch()
    {
        punchClip.Play();
    }
}
