using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using HumanFeatures;

public class PlayerHealthManager : MonoBehaviour
{
    public float currentHealth;
    PlayerController pc;

    // Start is called before the first frame update
    void Awake()
    {
        pc = GetComponent<PlayerController>();

        currentHealth = (float)((pc.features)[HumanFeature.FeatureType.FT_HEALTH]).currentValue;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = (float)((pc.features)[HumanFeature.FeatureType.FT_HEALTH]).currentValue;
    }

    public void HurtPlayer(float damageAmount)
    {
        ((pc.features)[HumanFeature.FeatureType.FT_HEALTH]).currentValue= (float)((pc.features)[HumanFeature.FeatureType.FT_HEALTH]).currentValue - damageAmount;
        currentHealth = (float)((pc.features)[HumanFeature.FeatureType.FT_HEALTH]).currentValue;
    }
}
