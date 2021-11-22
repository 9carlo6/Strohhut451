using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanFeatures;


public class Telescope : MonoBehaviour
{
    public PlayerController playerController;

    void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void EnableEffect()
    {
        //qua ci va un modificatore non una modifica alla feature
        playerController.increasedVisualField = true;

        ((playerController.features)[HumanFeature.FeatureType.FT_INCREASED_FOV]).currentValue = true;
    }
}
