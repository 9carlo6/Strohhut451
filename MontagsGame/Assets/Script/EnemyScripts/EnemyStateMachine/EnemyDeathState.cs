using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    EnemyHuman enemyHumanController;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemyHumanController = enemy.GetComponent<EnemyHuman>();

        enemyHumanController.PlaySoundDeath();

        Debug.Log("Stato = Nemico morto");
        //setting a 0 dell'intensità del material per la dissolvenza
        enemy.GetComponent<EnemyHuman>().material[0].SetFloat("Vector_Intensity_Dissolve2", 0.4f);

        if (enemy.GetComponent<EnemyHuman>().enemyWeapon != null)
        {
            enemy.GetComponent<EnemyHuman>().enemyWeapon.SetActive(false);
        }

        enemyHumanController.GetComponent<CapsuleCollider>().enabled = false;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}