using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAttackState : PlayerBaseState
{
    PlayerController playerController;
    PlayerHealthManager playerHealthManager;
    private Animator animator;
    private AnimatorClipInfo[] clipInfo;
    private Collider enemyCollider;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Stato Player = Attacco corpo a corpo");
        playerController = player.GetComponent<PlayerController>();
        animator = playerController.animator;
        playerHealthManager = player.GetComponent<PlayerHealthManager>();

        //Per nascondere la pistola
        playerController.weapon.SetActive(false);
        //Per disabilitare il RigBuilder
        playerController.rigBuilder.enabled = false;

        handleAttackDamage();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //Gestione passaggio allo stato vivo del giocatore
        if ((string.Equals(GetCurrentClipName(), "AttaccoCorpoACorpoStealth") || string.Equals(GetCurrentClipName(), "AttaccoCorpoACorpoDiretto")) && playerController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && playerController.isAttackButtonPressed == false)
        {
            //Viene mostrata la pistola
            playerController.weapon.SetActive(true);
            animator.SetBool("isAttacking", false);
            animator.SetFloat("isStealthAttack", 0);
            playerController.isAttackButtonPressed = false;
            //Per riabilitare il RigBuilder
            playerController.rigBuilder.enabled = true;

            //Per far illuminare il nemico quando viene colpito
            //enemyCollider.GetComponent<EnemyHealthManager>().enemyHit();

            Debug.Log("Passaggio dallo stato attacco allo stato vivo del giocatore");
            player.SwitchState(player.AliveState);
        }

        //Gestione del tempo in cui deve essere assegnato il danno al nemico quando il player attacca Stealth
        if (string.Equals(GetCurrentClipName(), "AttaccoCorpoACorpoStealth") && playerController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.50f && playerController.isAttackButtonPressed == false)
        {
            enemyCollider.GetComponent<EnemyHealthManager>().TakeDamage(playerController.meleeDamage);
        }


        //Gestione passaggio allo stato morto del giocatore
        if (playerHealthManager.currentHealth <= 0)
        {
            Debug.Log("Passaggio dallo stato attacco allo stato morto del giocatore");
            player.SwitchState(player.DeathState);
        }
    }

    //Funzione necessaria per la gestione dei danni inflitti dall'attacco
    public void handleAttackDamage()
    {
        animator.SetBool("isAttacking", true);

        //Questo serve per rivelare i nemici nel range di attacco
        Collider[] hitEnemies = Physics.OverlapSphere(playerController.attackPoint.position, playerController.attackRange, playerController.enemyLayers);

        //Questo HashSet serve per non permettere di colpire nuovamente lo stesso nemico nel medesimo attacco
        HashSet<string> enemiesAlreadyHitted = new HashSet<string>();

        //Per infliggere danno ai nemici
        foreach (Collider enemy in hitEnemies)
        {
            if (enemiesAlreadyHitted.Contains(enemy.name)) break;

            string enemyState = enemy.gameObject.GetComponent<EnemyController>().stateManager.getCurrentState();
            Animator enemyAnimator = enemy.gameObject.GetComponent<Animator>();

            if (string.Equals(enemyState, "EnemyStunnedState"))
            {
                animator.SetFloat("isStealthAttack", 1);
                Debug.Log("Il player sta colpendo Stealth: " + enemy.name);
                enemyCollider = enemy;
                enemyAnimator.SetBool("isAttackedStealth", true);

                //Gestione rotazione e posizionamento del giocatore nel momento in cui attacca stealth
                playerController.gameObject.transform.LookAt(enemyCollider.gameObject.transform);
                enemyCollider.gameObject.transform.LookAt(playerController.gameObject.transform);
                enemyCollider.gameObject.transform.position = playerController.gameObject.transform.position + playerController.gameObject.transform.forward * 1.5f;
                //enemyCollider.gameObject.transform.position = playerController.gameObject.transform.TransformPoint(5);
                //Vector3 targetDir = enemy.gameObject.transform.position - playerController.gameObject.transform.position;
            }
            else if (string.Equals(enemyState, "EnemyChasePlayerState") || string.Equals(enemyState, "EnemyMeleeAttackState") || string.Equals(enemyState, "EnemyPatrollingState"))
            {
                animator.SetFloat("isStealthAttack", 0);
                Debug.Log("Il player sta colpendo Melee: " + enemy.name);
                enemyAnimator.SetBool("isStunned", true);

                //Gestione rotazione del giocatore nel momento in cui attacca melee
                enemyCollider = enemy;
                playerController.gameObject.transform.LookAt(enemyCollider.gameObject.transform);
            }

            //Aggiunge il nemico al set
            enemiesAlreadyHitted.Add(enemy.name);
        }
    }

    //Funzione necessaria per risalire al nome dell'animazione corrente
    public string GetCurrentClipName()
    {
        clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        return clipInfo[0].clip.name;
    }


    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {

    }
}