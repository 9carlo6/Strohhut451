using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedState : LevelBaseState
{
    public override void EnterState(LevelStateManager level)
    {
        Debug.Log("Stato Livello = Livello completato");
    }

    public override void UpdateState(LevelStateManager level)
    {

    }

    public override void OnCollisionEnter(LevelStateManager level, Collision collision)
    {

    }
}