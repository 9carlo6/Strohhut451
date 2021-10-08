using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMeleeAttackState : PlayerBaseState
{
    PlayerController playerController;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Stato = Attacco corpo a corpo Melee");
        playerController = player.GetComponent<PlayerController>();
        //Viene nascosta la pistola
        playerController.weapon.SetActive(false);
  			playerController.animator.SetBool("isMeleeAttack", true);
  			//Per disabilitare il RigBuilder
  			playerController.rigBuilder.enabled = false;



    }

    public override void UpdateState(PlayerStateManager player)
    {

      if(playerController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f){
        //Viene mostrata la pistola
        playerController.weapon.SetActive(true);
        playerController.animator.SetBool("isMeleeAttack", false);
        //Per riabilitare il RigBuilder
        playerController.rigBuilder.enabled = true;

        Debug.Log("Passaggio dallo stato attacco melee allo stato vivo del giocatore");
        player.SwitchState(player.AliveState);
      }

      /*
      if(!playerController.isAttackButtonPressed){
        Debug.Log("Passaggio dallo stato attacco melee allo stato vivo del giocatore");
        player.SwitchState(player.AliveState);
      }
      */

    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {

    }
}
