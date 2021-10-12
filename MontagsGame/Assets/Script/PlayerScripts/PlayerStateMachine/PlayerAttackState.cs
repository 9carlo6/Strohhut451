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
    }

    public override void UpdateState(PlayerStateManager player)
    {
      //Gestione passaggio allo stato vivo del giocatore
      if(string.Equals(GetCurrentClipName(), "AttaccoCorpoACorpoDiretto") && playerController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f){
  			//Viene mostrata la pistola
  			playerController.weapon.SetActive(true);
  			animator.SetBool("isAttacking", false);
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
