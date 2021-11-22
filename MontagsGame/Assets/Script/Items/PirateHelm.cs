using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateHelm : MonoBehaviour
{
    public GameObject[] enemies;
    private EnemyStateManager enemymanager;

    public void EnableEffect()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemymanager = enemy.GetComponent<EnemyStateManager>();

            if (enemymanager.getCurrentState() == "EnemyStunnedState"){
                enemymanager.SwitchState(enemymanager.StunnedState);
            }
        }
    }

}
