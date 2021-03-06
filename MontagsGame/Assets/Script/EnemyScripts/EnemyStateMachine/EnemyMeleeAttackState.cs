using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyFeatures;

public class EnemyMeleeAttackState : EnemyBaseState
{
    float distanceToTarget;

    GameObject enemyGameObject;
    GameObject playerGameObject;

    EnemyHealthManager enemyHealthManager;
    EnemyHuman enemyHumanController;

    //per l'animazione
    public Animator enemyAnimator;
    private AnimatorClipInfo[] clipInfo;

    //Questo ? il timer per gestire quando infliggere il danno al player
    public float timeRemainingToAttack;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato Nemico = Attacca");

        enemyGameObject = enemy.GetComponent<EnemyHuman>().gameObject;
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        enemyHealthManager = enemy.GetComponent<EnemyHealthManager>();
        enemyHumanController = enemy.GetComponent<EnemyHuman>();
        enemyAnimator = enemy.GetComponent<Animator>();

        enemyHumanController.PlaySoundPunch();

        //Il timer viene settato a un valore iniziale (che dovrebbe coincidire con la lunghezza dell'animazione "AttaccoDirettoNemico")
        //Bisogna trovare un modo per ricavare la lunghezza di una specifica animazione (ora sappiamo ricavare solo quella dell'animazione corrente)
        timeRemainingToAttack = 0;

        if (enemyHumanController.enemyWeapon != null)
        {
            enemyHumanController.enemyWeapon.SetActive(false);
        }

        EnemyAttack();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        if(playerGameObject.transform.GetComponent<PlayerHealthManager>().currentHealth <= 0)
        {
            if (enemyHumanController.enemyWeapon != null)
            {
                enemyHumanController.enemyWeapon.SetActive(true);
            }
            enemy.SwitchState(enemy.AliveState);

        }
        else
        {

            distanceToTarget = Vector3.Distance(enemyGameObject.transform.position, playerGameObject.transform.position);

            if (distanceToTarget <= 1.5f)
            {
                enemyGameObject.transform.LookAt(playerGameObject.transform);
                EnemyAttack();
            }
            else
            {
                if (enemyHumanController.enemyWeapon != null)
                {
                    enemyHumanController.enemyWeapon.SetActive(true);
                }
                enemy.SwitchState(enemy.ChasePlayerState);
            }

            //Gestione passaggio allo stato Stunned del nemico
            if (enemyAnimator.GetBool("isStunned"))
            {
                if (enemyHumanController.enemyWeapon != null)
                {
                    enemyHumanController.enemyWeapon.SetActive(true);
                }
                enemy.SwitchState(enemy.StunnedState);
            }

            if (enemyHealthManager.currentHealth <= 0)
            {
                if (enemyHumanController.enemyWeapon != null)
                {
                    enemyHumanController.enemyWeapon.SetActive(true);
                }
                enemy.SwitchState(enemy.DeathState);
            }
        }

       
    }

    //Funzione per gestire il danno inflitto al personaggio dall'attacco del nemico
    public void EnemyAttack()
    {

        //Se il tempo per il prossimo attacco ? ancora maggiore di zero allo si diminuisce timer
        if (timeRemainingToAttack > 0)
        {
            timeRemainingToAttack -= Time.deltaTime;
        }
        //Altrimenti si procede infliggendo il danno al nemico
        else
        {
            if (string.Equals(GetCurrentClipName(), "AttaccoDirettoNemico") && enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.10f)
            {
                Collider[] hitPlayer = Physics.OverlapSphere(enemyHumanController.attackPoint.position, (float) ((enemyHumanController.features)[EnemyFeature.FeatureType.FT_MELEE_RANGE]).currentValue, enemyHumanController.targetMask);

                if (hitPlayer != null)
                {
                    Debug.Log("Sto colpendo il player con melee");
                    playerGameObject.transform.GetComponent<PlayerHealthManager>().HurtPlayer((float) ((enemyHumanController.features)[EnemyFeature.FeatureType.FT_MELEE_DAMAGE]).currentValue);

                    //Una volta inflitto il danno il timer aggiornato alla met? lunghezza dell'animazione corrente, ovvero "AttaccoDirettoNemico"
                    timeRemainingToAttack = enemyAnimator.GetCurrentAnimatorStateInfo(0).length/2;
                }
            }
        }
        
    }

    //Funzione per il debug dell'attacco corpo a corpo
    void OnDrawGizmosSelected()
    {
        if (!enemyAnimator.GetBool("Attack")) return;

        Gizmos.DrawWireSphere(enemyHumanController.attackPoint.position, (float)((enemyHumanController.features)[EnemyFeature.FeatureType.FT_MELEE_RANGE]).currentValue);
    }

    //Funzione necessaria per risalire al nome dell'animazione corrente
    public string GetCurrentClipName(){
      clipInfo = enemyAnimator.GetCurrentAnimatorClipInfo(0);
      return clipInfo[0].clip.name;
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}