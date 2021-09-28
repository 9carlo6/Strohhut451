using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Questa classe serve per gestire la vita del nemico
public class EnemyHealthManager : MonoBehaviour
{

    public float maxhealth;
    public float currentHealth;
    Ragdoll ragdoll;

    //Queste servono per gestire il cambio di colore del nemico quando viene colpito
    SkinnedMeshRenderer skinnedMeshRenderer;
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        currentHealth = maxhealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigidbody in rigidBodies)
        {
            HitBox hitBox = rigidbody.gameObject.AddComponent<HitBox>();
            hitBox.enemyHealth = this;
        }
    }

    //Questa funzione serve per diminuire la vita del nemico ogni volta che esso viene colpito.
    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        if(currentHealth <= 0.0f)
        {
            Die();
        }

        blinkTimer = blinkDuration;
    }


    private void Die()
    {
        ragdoll.ActivateRagdoll();
    }


    private void Update() 
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        //Si aggiunge l'uno perchè altrimenti il nemico appare totalmente nero
        float intensity = (lerp * blinkIntensity) + 0.5f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
}
