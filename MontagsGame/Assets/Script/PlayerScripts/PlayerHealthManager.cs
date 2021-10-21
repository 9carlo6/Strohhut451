using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public float startingHealth;
    public float currentHealth;
    public GameObject pnlDeath;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        pnlDeath.SetActive(false);
        playerController = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {

        
        
        /*
        if(currentHealth <= 0)
        {
          gameObject.SetActive(false);
        }
        */
    }

    public void HurtPlayer(float damageAmount)
    {
        
        currentHealth -= damageAmount;
    }

    public void Respawn()
    {
        //Cursor.visible = false;
        pnlDeath.SetActive(false);
        GetComponent<PlayerController>().enabled = true;
        currentHealth = startingHealth;
        playerController.weapon.SetActive(true);
        playerController.rigBuilder.enabled = true;
       


    }
}
