using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using WeaponFeatures;
using System;


public  abstract class WeaponController :  Component
{
    public GameObject[] enemies;
    //Per capire se si sta sparando o no
    public bool isFiring = false;

    //Questo accumulatedTime sarebbe il tempo che deve passare per poter sparare il prossimo proiettile
    public float accumulatedTime;

    //Per gestire gli effetti particellari
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;

    //Per gestire il rendering di una scia di poligoni dietro un GameObject (scia proiettile)
    public TrailRenderer tracerEffect;

    //Per accedere alla posizione dell'origine del raycast
    public Transform raycastOrigin;
    public Ray ray;
    public RaycastHit hitInfo;
    
    public AudioManager audioManager;

    public override void Awake()
    {
        audioManager = GetComponent<AudioManager>();
        this.category = "Weapon";
        base.Awake();
    }
    
    //Funzione necessaria per gestire l'update dello sparo
    public void UpdateFiring(float deltaTime)
    {
        isFiring = true;

        //Minore � il fireRate maggiore � il tempo che intercorre tra uno sparo e un'altro (quando si tiene premuto il pulsante per sparare)
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / ((int) features[WeaponFeatures.WeaponFeature.FeatureType.FT_FIRE_RATE].currentValue);

        while (accumulatedTime >= 0.0f)
        {
          FireBullet();
          accumulatedTime -= fireInterval;
        }

        
    }

    //Funzione per sparare
    public virtual void FireBullet()
    {

    }

    public override void setFeatures()
    {
        //l'idea è settare i valori delle feature "composte" tipo la velocità è funzione del peso:

        //this.features[HumanFeature.FT_SPEED].currentValue = 0.0417 * this.features[HumanFeature.FT_WEIGHT].currentValue;

        //NON CI SONO FEATURE COMPOSTE PER ORA, NON FA NULLA
    }
    public override void initializeFeatures()
    {
       // features[WeaponFeatures.WeaponFeature.FeatureType.FT_AMMO_COUNT].currentValue = features[WeaponFeatures.WeaponFeature.FeatureType.FT_MAX_AMMO_COUNT].currentValue;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        //Per gestire i modificatori
        //handleWeaponModifier();

        //applyModifiers();
        base.Update();

        if (isFiring && (int)features[WeaponFeatures.WeaponFeature.FeatureType.FT_AMMO_COUNT].currentValue > 0)
        {
            makeNoise();
            UpdateFiring(Time.deltaTime);
        }
    }

    //Funzione chiamata quando termina l'input per lo sparo
    public void StopFiring()
    {
        isFiring = false;
    }

    public void makeNoise()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)


        {
            if(Vector3.Distance(transform.position, enemy.transform.position) <= (float)((features)[WeaponFeature.FeatureType.FT_NOISE_RANGE]).currentValue && !enemy.GetComponent<EnemyController>().animator.GetBool("isStunned") && !enemy.GetComponent<EnemyStateManager>().getCurrentState().Equals("EnemyChasePlayerState") && !enemy.GetComponent<EnemyStateManager>().getCurrentState().Equals("EnemyDeathState"))
            {

                Debug.Log("Sto entrando nell'if dell'alert");

                enemy.GetComponent<EnemyStateManager>().SwitchState(enemy.GetComponent<EnemyStateManager>().ChasePlayerState);
                enemy.GetComponent<EnemyStateManager>().ChasePlayerState.fireInHearRange = true;
            }
        }
    }
}