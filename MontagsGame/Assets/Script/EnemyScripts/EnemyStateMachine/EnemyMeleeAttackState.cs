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

    //Questo è il timer per gestire quando infliggere il danno al player
    public float timeRemainingToAttack;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato Nemico = Attacca");
        enemyTransform = enemy.gameObject.transform;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        targetTransform = playerRef.transform;
        enemyHealthManager = enemy.GetComponent<EnemyHealthManager>();
        enemyController = enemy.GetComponent<EnemyController>();
        animator = enemy.GetComponent<Animator>();

        //Il timer viene settato a un valore iniziale (che dovrebbe coincidire con la lunghezza dell'animazione "AttaccoDirettoNemico")
        //Bisogna trovare un modo per ricavare la lunghezza di una specifica animazione (ora sappiamo ricavare solo quella dell'animazione corrente)
        timeRemainingToAttack = 0.5f;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        distanceToTarget = Vector3.Distance(enemyTransform.position, targetTransform.position);

        if (distanceToTarget <= 1.5){
            enemyTransform.LookAt(targetTransform);
            EnemyAttack();
        }
        else{
            enemy.SwitchState(enemy.ChasePlayerState);
        }

        if(enemyHealthManager.currentHealth <= 0){
            enemy.SwitchState(enemy.DeathState);
        }
    }

    //Funzione per gestire il danno inflitto al personaggio dall'attacco del nemico
    public void EnemyAttack()
    {
        //Se il tempo per il prossimo attacco è ancora maggiore di zero allo si diminuisce timer
        if (timeRemainingToAttack > 0)
        {
            timeRemainingToAttack -= Time.deltaTime;
        }
        //Altrimenti si procede infliggendo il danno al nemico
        else
        {
            if (string.Equals(GetCurrentClipName(), "AttaccoDirettoNemico"))
            {
                Collider[] hitPlayer = Physics.OverlapSphere(enemyController.attackPoint.position, enemyController.attackRange, enemyController.targetMask);

                if (hitPlayer.Length != 0)
                {
                    Debug.Log("Sto colpendo il player con melee");
                    playerRef.GetComponent<PlayerHealthManager>().HurtPlayer(enemyController.meleeDamage);

                    //Una volta inflitto il danno il timer aggiornato alla lunghezza dell'animazione corrente, ovvero "AttaccoDirettoNemico"
                    timeRemainingToAttack = animator.GetCurrentAnimatorStateInfo(0).length;
                }
            }
        }
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
