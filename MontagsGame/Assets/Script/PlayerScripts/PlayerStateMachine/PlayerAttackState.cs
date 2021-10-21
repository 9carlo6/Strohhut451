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


    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Stato = Attacco corpo a corpo");
        playerController = player.GetComponent<PlayerController>();
        animator = playerController.animator;
        playerHealthManager = player.GetComponent<PlayerHealthManager>();

        //Viene nascosta la pistola
        playerController.weapon.SetActive(false);
  			animator.SetBool("isAttacking", true);
  			//Per disabilitare il RigBuilder
  			playerController.rigBuilder.enabled = false;

        //Gestione dell'attacco
        //Questo serve per rivelare i nemici nel range di attacco
  			Collider[] hitEnemies = Physics.OverlapSphere(playerController.attackPoint.position, playerController.attackRange, playerController.enemyLayers);

        //Questo HashSet serve per non permettere di colpire nuovamente lo stesso nemico nel singolo attacco
        HashSet<string> enemiesAlreadyHitted = new HashSet<string>();

  			//Per infliggere danno ai nemici
  			foreach(Collider enemy in hitEnemies){

          if(enemiesAlreadyHitted.Contains(enemy.name)) break;

  				Debug.Log("sta colpendo: " + enemy.name);
  				enemy.GetComponent<EnemyHealthManager>().TakeDamage(playerController.meleeDamage);

          //Aggiunge il nemico al set
          enemiesAlreadyHitted.Add(enemy.name);
  			}
    }

    public override void UpdateState(PlayerStateManager player)
    {
      //Gestione passaggio allo stato vivo del giocatore
      if(string.Equals(GetCurrentClipName(), "AttaccoCorpoACorpoDiretto") && playerController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && playerController.isAttackButtonPressed == false){
  			//Viene mostrata la pistola
  			playerController.weapon.SetActive(true);
  			animator.SetBool("isAttacking", false);
        playerController.isAttackButtonPressed = false;
  			//Per riabilitare il RigBuilder
  			playerController.rigBuilder.enabled = true;


        Debug.Log("Passaggio dallo stato attacco allo stato vivo del giocatore");
        player.SwitchState(player.AliveState);
  		}

      //Gestione passaggio allo stato morto del giocatore
      if (playerHealthManager.currentHealth <= 0)
      {
          Debug.Log("Passaggio dallo stato attacco allo stato morto del giocatore");
          player.SwitchState(player.DeathState);
      }
    }

    //Funzione necessaria per risalire al nome dell'animazione corrente
    public string GetCurrentClipName(){
      clipInfo = animator.GetCurrentAnimatorClipInfo(0);
      return clipInfo[0].clip.name;
    }


    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {

    }
}
