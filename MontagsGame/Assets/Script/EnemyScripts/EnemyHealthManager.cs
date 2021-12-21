using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyFeatures;

//Questa classe serve per gestire la vita del nemico
public class EnemyHealthManager : MonoBehaviour
{
    //[HideInInspector]
    public float maxhealth; 
    public float currentHealth;

    //Forza da applicare quando il nemico muore per fargli fare un salto
    public float dieForce;

    //Queste servono per gestire il cambio di colore del nemico quando viene colpito
    SkinnedMeshRenderer skinnedMeshRenderer;
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    EnemyHuman enemyHumanController;


    //Per gestire la barra della vita
    [HideInInspector] public EnemyStateManager stateManager;

    // Start is called before the first frame update
    void Start()
    {

        stateManager = GetComponent<EnemyStateManager>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        enemyHumanController = GetComponent<EnemyHuman>();

        maxhealth = (float)((enemyHumanController.features)[EnemyFeature.FeatureType.FT_HEALTH]).currentValue;
        currentHealth = maxhealth;

    }

    //Questa funzione serve per diminuire la vita del nemico ogni volta che esso viene colpito.
    public void TakeDamage(float amount)
    {
        currentHealth = (float)((enemyHumanController.features)[EnemyFeature.FeatureType.FT_HEALTH]).currentValue;
        currentHealth -= amount;

        ((enemyHumanController.features)[EnemyFeature.FeatureType.FT_HEALTH]).currentValue = currentHealth;

            if (((float)((enemyHumanController.features)[EnemyFeature.FeatureType.FT_HEALTH]).currentValue)<=0 || stateManager.getCurrentState() == "EnemyStunnedState")
            {
                Die();
            }

            blinkTimer = blinkDuration;

    }

    //Questa funzione serve per gestire l'illuminazione del nemico quando viene stordito
    public void enemyHit()
    {
        blinkTimer = blinkDuration;
    }

    private void Die()
    {
        //stateManager.SwitchState(stateManager.DeathState);
        ((enemyHumanController.features)[EnemyFeature.FeatureType.FT_HEALTH]).currentValue = 0.0f;
    }

    private void Update()
    {

        //Questa parte serve per far illuminare il nemico quando viene colpito

        //Debug.log("CAPIAMO IL PROBLEMA " + typeof((((Dictionary<EnemyFeature.FeatureType, EnemyFeature>) enemyController.features)[EnemyFeature.FeatureType.FT_HEALTH]).currentValue).ToString()) ;
        //Debug.Log((((Dictionary<EnemyFeature.FeatureType, EnemyFeature>)enemyController.features)[EnemyFeature.FeatureType.FT_HEALTH]).currentValue);


        currentHealth = (float)((enemyHumanController.features)[EnemyFeature.FeatureType.FT_HEALTH]).currentValue;

        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);

        //Si aggiunge l'uno perche altrimenti il nemico appare totalmente nero
        float intensity = (lerp * blinkIntensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
}
