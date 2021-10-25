using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //abbassare barra vita grafica se ci sarà
    }

    public void HurtPlayer(float damageAmount)
    {
        currentHealth -= damageAmount;
    }
}
