using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public Shader deathShader;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Stato = Morto");

        //Appena entra si distrugge l'oggetto
        Object.Destroy(player.gameObject);
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {

    }
}
