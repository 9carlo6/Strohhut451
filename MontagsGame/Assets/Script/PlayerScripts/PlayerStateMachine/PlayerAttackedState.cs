using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackedState : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Stato = Giocatore Attaccato");
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {

    }
}
