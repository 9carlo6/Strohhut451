using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using HumanFeatures;

public class PlayerHealthManager : MonoBehaviour
{
    public float currentHealth;
    PlayerController playerController;

    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();

        currentHealth = (float)((playerController.features)[HumanFeature.FeatureType.FT_HEALTH]).currentValue;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = (float)((playerController.features)[HumanFeature.FeatureType.FT_HEALTH]).currentValue;
    }

    public void HurtPlayer(float damageAmount)
    {
        ((playerController.features)[HumanFeature.FeatureType.FT_HEALTH]).currentValue= (float)((playerController.features)[HumanFeature.FeatureType.FT_HEALTH]).currentValue - damageAmount;
        currentHealth = (float)((playerController.features)[HumanFeature.FeatureType.FT_HEALTH]).currentValue;
    }
}
