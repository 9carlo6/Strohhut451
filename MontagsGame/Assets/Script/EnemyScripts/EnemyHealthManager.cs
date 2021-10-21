using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Questa classe serve per gestire la vita del nemico
public class EnemyHealthManager : MonoBehaviour
{
    public float maxhealth;
    //[HideInInspector]
    public float currentHealth;

    //Forza da applicare quando il nemico muore per fargli fare un salto
    public float dieForce;

    //Queste servono per gestire il cambio di colore del nemico quando viene colpito
    SkinnedMeshRenderer skinnedMeshRenderer;
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    //Per gestire la barra della vita
    UIHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //ragdoll = GetComponent<Ragdoll>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        currentHealth = maxhealth;
        healthBar = GetComponentInChildren<UIHealthBar>();
    }

    //Questa funzione serve per diminuire la vita del nemico ogni volta che esso viene colpito.
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetHealthBarPercentage(currentHealth / maxhealth);
        if (currentHealth <= 0.0f)
        {
            Die();
        }

        blinkTimer = blinkDuration;
    }

    private void Die()
    {
        healthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        //Questa parte serve per far illuminare il nemico quando viene colpito
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        //Si aggiunge l'uno perche altrimenti il nemico appare totalmente nero
        float intensity = (lerp * blinkIntensity) + 0.5f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
}
