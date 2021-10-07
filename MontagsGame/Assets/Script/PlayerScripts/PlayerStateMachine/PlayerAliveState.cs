using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAliveState : PlayerBaseState
{
    PlayerHealthManager playerHealthManager;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Stato = Vivo");
        playerHealthManager = player.GetComponent<PlayerHealthManager>();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (playerHealthManager.currentHealth <= 0)
        {
            Debug.Log("Passaggio dallo stato vivo allo stato morto del giocatore");
            player.SwitchState(player.DeathState);
        }
    }
    
    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {

    }
}
