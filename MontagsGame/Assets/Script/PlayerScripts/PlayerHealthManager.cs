using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth;
    public GameObject pnlDeath;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        pnlDeath.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HurtPlayer(float damageAmount)
    {
        currentHealth -= damageAmount;
    }
}
