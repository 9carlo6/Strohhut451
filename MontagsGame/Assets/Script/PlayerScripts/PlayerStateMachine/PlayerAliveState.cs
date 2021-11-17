using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAliveState : PlayerBaseState
{
    PlayerHealthManager playerHealthManager;
    PlayerController playerController;
    

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Stato Player = Vivo");
        playerHealthManager = player.GetComponent<PlayerHealthManager>();
        playerController = player.GetComponent<PlayerController>();
        
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (playerHealthManager.currentHealth <= 0)
        {
            Debug.Log("Passaggio dallo stato vivo allo stato morto del giocatore");
            player.SwitchState(player.DeathState);
        }

        if(playerController.isAttackButtonPressed){
          Debug.Log("Passaggio dallo stato vivo allo stato attacco melee del giocatore");
          player.SwitchState(player.AttackState);
        }
        
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {

    }
}
