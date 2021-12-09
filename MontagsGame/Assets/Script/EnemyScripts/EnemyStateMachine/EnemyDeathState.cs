using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    EnemyController enemyController;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemyController = enemy.GetComponent<EnemyController>();

        enemyController.PlaySoundDeath();

        Debug.Log("Stato = Nemico morto");
        //setting a 0 dell'intensità del material per la dissolvenza
        enemy.GetComponent<EnemyController>().material[0].SetFloat("Vector_Intensity_Dissolve2", 0.4f);

        if (enemy.GetComponent<EnemyController>().enemyWeapon != null)
        {
            enemy.GetComponent<EnemyController>().enemyWeapon.SetActive(false);
        }
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}