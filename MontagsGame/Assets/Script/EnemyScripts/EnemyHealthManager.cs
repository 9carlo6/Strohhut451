using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Questa classe serve per gestire la vita del nemico
public class EnemyHealthManager : MonoBehaviour
{

    public float maxhealth;
    //[HideInInspector]
    public float currentHealth;
    Ragdoll ragdoll;

   // EnemyStateManager enemy;


    //Forza da applicare quando il nemico muore per fargli fare un salto
    public float dieForce;

    //Queste servono per gestire il cambio di colore del nemico quando viene colpito
    SkinnedMeshRenderer skinnedMeshRenderer;
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;
    public GameObject enemy;


    //Per gestire la barra della vita
    UIHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {

        ragdoll = GetComponent<Ragdoll>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        currentHealth = maxhealth;
        healthBar = GetComponentInChildren<UIHealthBar>();

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
        healthBar.SetHealthBarPercentage(currentHealth / maxhealth);
        if (currentHealth <= 0.0f)
        {

            Die(direction);
        }

        blinkTimer = blinkDuration;
    }


    private void Die(Vector3 direction)
    {
        //ragdoll.ActivateRagdoll();
        //direction.y = 1;
        //ragdoll.ApplyForce(direction * dieForce);

        healthBar.gameObject.SetActive(false);

    }


    private void Update()
    {
        /*
        if(currentHealth <= 0)
        {
            enemy.SwitchState
        }
        */

        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        //Si aggiunge l'uno perch� altrimenti il nemico appare totalmente nero
        float intensity = (lerp * blinkIntensity) + 0.5f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
}
