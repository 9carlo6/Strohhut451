using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyBaseState
{
    float distanceToTarget;
    Transform enemyTransform;
    Transform targetTransform;
    PlayerController playerController;
    GameObject playerRef;
    EnemyHealthManager enemyHealthManager;
    EnemyController enemyController;

    //per l'animazione
  	public Animator animator;
    private AnimatorClipInfo[] clipInfo;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato Nemico = Attacca");
        enemyTransform = enemy.gameObject.transform;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        targetTransform = playerRef.transform;
        enemyHealthManager = enemy.GetComponent<EnemyHealthManager>();
        enemyController = enemy.GetComponent<EnemyController>();
        animator = enemy.GetComponent<Animator>();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        distanceToTarget = Vector3.Distance(enemyTransform.position, targetTransform.position);

        if (distanceToTarget <= 1.5){
          enemyTransform.LookAt(targetTransform);

          if(string.Equals(GetCurrentClipName(), "AttaccoDirettoNemico") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f){
            Collider[] hitPlayer = Physics.OverlapSphere(enemyController.attackPoint.position, enemyController.attackRange, enemyController.targetMask);

            if(hitPlayer.Length != 0)
            {
                Debug.Log("Sto colpendo il player con melee");
                playerRef.GetComponent<PlayerHealthManager>().HurtPlayer(enemyController.meleeDamage);
            }
          }
        }else{
            enemy.SwitchState(enemy.ChasePlayerState);
        }

        if(enemyHealthManager.currentHealth <= 0){
            enemy.SwitchState(enemy.DeathState);
        }


        //ATTACCO


         //FINE ATTACCO
    }



    //Funzione necessaria per risalire al nome dell'animazione corrente
    public string GetCurrentClipName(){
      clipInfo = animator.GetCurrentAnimatorClipInfo(0);
      return clipInfo[0].clip.name;
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
