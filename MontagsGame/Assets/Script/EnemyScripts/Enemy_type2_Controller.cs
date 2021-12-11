using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using System.IO;
using EnemyFeatures;

//Questa classe serve per la gestione del'animazione del nemico
public class Enemy_type2_Controller : Character
{
    public Transform[] wayPoints;   //Array di points verso cui il nemico dovrà effettuare il patroling
    public LayerMask targetMask;    //Bersaglio, cioè il player
    public LayerMask obstructionMask; //Ostacoli, ad esempio le pareti

    public AudioSource deathClip;
    public AudioSource punchClip;
    public AudioSource alertClip;

    //Per l'attacco
    public GameObject enemyWeapon;
    public Transform attackPoint;

    //Per l'animazione
    public Animator animator;
    [HideInInspector] int velocityHash;
    private AnimatorClipInfo[] clipInfo;

    [HideInInspector] public EnemyStateManager stateManager;

    NavMeshAgent enemyNavMeshAgent;  //NavMesh del nemico

    [HideInInspector] public bool ready = false;

    //Per modificare i materiali dei figli runtime
    public Material[] material;
    private GameObject enemyBody;
    public Renderer renderEnemyBody;

    //Per l'animazione dell'arma
    public Rig enemy_weapon_rig;
    public Rig aimLayer;
    float aimDuration = 0.3f;

    //Per Gestire lo shader corrente
    public Shader baseEnemyShader;
    public Shader stunnedEnemyShader;
    public Shader enemyTrapShader;

    public float animation_velocity;

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
        string fileString = new StreamReader("Assets/Push-To-Data/Feature/Enemy/enemy_type2_features.json").ReadToEnd();
        mapper = JsonUtility.FromJson<EnemyFeaturesJsonMap>(fileString);

        base.Awake();

        this.features = mapper.todict();
    }

    public override void setFeatures()
    {
        //l'idea è settare i valori delle feature "composte" tipo la velocità è funzione del peso:
        this.features[EnemyFeature.FeatureType.FT_VELOCITY].currentValue = (((float)this.features[EnemyFeature.FeatureType.FT_VELOCITY].baseValue) * ((float)this.features[EnemyFeature.FeatureType.FT_WEIGHT].baseValue)) / (float)(this.features[EnemyFeature.FeatureType.FT_WEIGHT].currentValue);
    }

    public override void initializeFeatures()
    {
        features[EnemyFeature.FeatureType.FT_HEALTH].currentValue = features[EnemyFeature.FeatureType.FT_MAX_HEALTH].currentValue;

        if (weaponController != null)
        {
            features[EnemyFeature.FeatureType.FT_IS_WEAPONED].currentValue = true;
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        animation_velocity = (float)this.features[EnemyFeature.FeatureType.FT_VELOCITY].currentValue;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();

        if (weaponController != null)
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
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
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

                    //Per gestire il movimento delle mani rispetto all'arma
                    if (enemy_weapon_rig != null)
                    {
                        enemy_weapon_rig.weight = 1;
                    }

                    if (animation_velocity >= 0.2f)
                    {
                        animation_velocity -= Time.deltaTime * ((float)this.features[EnemyFeature.FeatureType.FT_DECELERATION].currentValue);
                    }

                    animator.SetFloat(velocityHash, animation_velocity);
                    animator.SetBool("isWalkingEnemy", true);
                    animator.SetBool("Attack", false);
                    animator.SetBool("isStunned", false);
                    animator.SetBool("waypointReached", false);
                    enemyNavMeshAgent.isStopped = false;

                    //Per gestire lo shader
                    renderEnemyBody.material.shader = baseEnemyShader;
                    break;

                case "EnemyCheckState":

                    //Per gestire il movimento delle mani rispetto all'arma
                    if (enemy_weapon_rig != null)
                    {
                        enemy_weapon_rig.weight = 1;
                    }

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

                    //Per gestire il movimento delle mani rispetto all'arma
                    if (enemy_weapon_rig != null)
                    {
                        enemy_weapon_rig.weight = 1;
                    }

                    if (animation_velocity <= 0.5f)
                    {
                        animation_velocity += Time.deltaTime * ((float)this.features[EnemyFeature.FeatureType.FT_ACCELERATION].currentValue);
                    }

                    animator.SetFloat(velocityHash, animation_velocity);

                    animator.SetBool("isWalkingEnemy", true);
                    animator.SetBool("isStunned", false);
                    animator.SetBool("Attack", false);
                    animator.SetBool("waypointReached", false);
                    enemyNavMeshAgent.isStopped = false;

                    //Per gestire lo shader
                    //renderEnemyBody.material.shader = baseEnemyShader;
                    break;

                case "EnemyMeleeAttackState":

                    //Per gestire il movimento delle mani rispetto all'arma
                    if (enemy_weapon_rig != null)
                    {
                        enemy_weapon_rig.weight = 0;
                    }

                    animator.SetBool("isWalkingEnemy", true);
                    animator.SetBool("Attack", true);
                    animator.SetBool("isStunned", false);
                    animator.SetBool("waypointReached", false);
                    enemyNavMeshAgent.isStopped = false;

                    //Per gestire lo shader
                    renderEnemyBody.material.shader = baseEnemyShader;
                    break;

                case "EnemyStunnedState":

                    //Per gestire il movimento delle mani rispetto all'arma
                    if (enemy_weapon_rig != null)
                    {
                        enemy_weapon_rig.weight = 0;
                    }

                    animator.SetBool("isWalkingEnemy", false);
                    animator.SetBool("Attack", false);
                    animator.SetBool("isStunned", true);
                    animator.SetBool("waypointReached", false);
                    enemyNavMeshAgent.isStopped = true;

                    //Per gestire lo shader
                    renderEnemyBody.material.shader = stunnedEnemyShader;
                    break;

                case "EnemyAliveState":

                    //Per gestire il movimento delle mani rispetto all'arma
                    if (enemy_weapon_rig != null)
                    {
                        enemy_weapon_rig.weight = 1;
                    }

                    animator.SetBool("isWalkingEnemy", false);
                    animator.SetBool("Attack", false);
                    animator.SetBool("isStunned", false);
                    animator.SetBool("waypointReached", false);
                    enemyNavMeshAgent.isStopped = true;

                    //Per gestire lo shader
                    renderEnemyBody.material.shader = baseEnemyShader;
                    break;


                case "EnemyDeathState":
                    //Per gestire il movimento delle mani rispetto all'arma
                    if (enemy_weapon_rig != null)
                    {
                        enemy_weapon_rig.weight = 0;
                    }

                    EnemyDeath();
                    break;

                default:

                    Debug.Log("animation error ");
                    break;
            }
        }
    }

    public void EnemyDeath()
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

    public void PlaySoundAlert()
    {
        alertClip.Play();
    }
}
