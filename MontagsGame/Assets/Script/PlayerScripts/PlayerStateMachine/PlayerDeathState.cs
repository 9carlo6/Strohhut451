using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public Shader deathShader;
    PlayerController playerController;
    private Animator animator;
    private AnimatorClipInfo[] clipInfo;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Stato = Morto");
        playerController = player.GetComponent<PlayerController>();
        animator = playerController.animator;

        //Viene nascosta la pistola
        playerController.weapon.SetActive(false);
  			animator.SetBool("isDeath", true);
  			//Per disabilitare il RigBuilder
  			playerController.rigBuilder.enabled = false;

    }

    public override void UpdateState(PlayerStateManager player)
    {
      if(string.Equals(GetCurrentClipName(), "MortePersonaggio") && playerController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f){
        //quando finisce l'animazione scompare il personaggio (da cambiare)
        Object.Destroy(player.gameObject);
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
