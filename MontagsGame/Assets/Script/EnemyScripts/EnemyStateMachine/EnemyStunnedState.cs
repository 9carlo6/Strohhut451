using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnedState : EnemyBaseState
{
    EnemyHealthManager enemyHealthManager;
    Animator enemyAnimator;

    //Questo è il timer per gestire quando il nemico deve uscire dallo stato stordito
    public float stunnedTime;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato = Nemico stordito");
        enemyHealthManager = enemy.GetComponent<EnemyHealthManager>();
        enemyAnimator = enemy.GetComponent<Animator>();

        //Viene settato a zero quando entra nello stato stordito
        stunnedTime = 0;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //Se il tempo in cui è nello stato stordito è superiore a 3 secondi allora esce dallo stato stordito
        if (stunnedTime > 3 && !enemyAnimator.GetBool("isAttackedStealth"))
        {
            enemy.SwitchState(enemy.ChasePlayerState);
        }
        else
        {
            stunnedTime += Time.deltaTime;
        }

        if (enemyHealthManager.currentHealth <= 0)
        {
          enemy.SwitchState(enemy.DeathState);
        }
    }
    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
