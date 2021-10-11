using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMeleeAttackState : PlayerBaseState
{
    PlayerController playerController;
    private Animator animator;
    private AnimatorClipInfo[] clipInfo;


    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Stato = Attacco corpo a corpo Melee");
        playerController = player.GetComponent<PlayerController>();
        animator = playerController.animator;

        //Viene nascosta la pistola
        playerController.weapon.SetActive(false);
  			animator.SetBool("isMeleeAttack", true);
  			//Per disabilitare il RigBuilder
  			playerController.rigBuilder.enabled = false;
    }

    public override void UpdateState(PlayerStateManager player)
    {

      if(string.Equals(GetCurrentClipName(), "AttaccoCorpoACorpo") && playerController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f){
  			//Viene mostrata la pistola
  			playerController.weapon.SetActive(true);
  			animator.SetBool("isMeleeAttack", false);
  			//Per riabilitare il RigBuilder
  			playerController.rigBuilder.enabled = true;


        Debug.Log("Passaggio dallo stato attacco melee allo stato vivo del giocatore");
        player.SwitchState(player.AliveState);
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
