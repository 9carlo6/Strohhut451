using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HumanFeatures;

[Serializable]
public class HumanFeaturesJsonMap : Dictionable
{
    public float FT_SPEED;
    public float FT_HEALTH;
    public float FT_ATTACK_RANGE;
    public float FT_MELEE_DAMAGE;
    public bool FT_INCREASED_FOV;
    public float FT_MAX_HEALTH;
    public float FT_WEIGHT;


    public Dictionary<System.Object, Feature> todict()
    {
        Dictionary<System.Object, Feature> newDict =  new Dictionary<System.Object, Feature>();

        newDict.Add(HumanFeature.FeatureType.FT_SPEED ,new SpeedHumanFeature(FT_SPEED, HumanFeature.FeatureType.FT_SPEED));
        newDict.Add(HumanFeature.FeatureType.FT_HEALTH, new HealthHumanFeature(FT_HEALTH, HumanFeature.FeatureType.FT_HEALTH));
        newDict.Add(HumanFeature.FeatureType.FT_ATTACK_RANGE, new AttackRangeHumanFeature(FT_ATTACK_RANGE, HumanFeature.FeatureType.FT_ATTACK_RANGE));
        newDict.Add(HumanFeature.FeatureType.FT_MELEE_DAMAGE, new MeleeDamageHumanFeature(FT_MELEE_DAMAGE, HumanFeature.FeatureType.FT_MELEE_DAMAGE));
        newDict.Add(HumanFeature.FeatureType.FT_INCREASED_FOV, new IncreasedFovHumanFeature(FT_INCREASED_FOV, HumanFeature.FeatureType.FT_INCREASED_FOV));
        newDict.Add(HumanFeature.FeatureType.FT_MAX_HEALTH, new MaxHealthHumanFeature(FT_MAX_HEALTH, HumanFeature.FeatureType.FT_MAX_HEALTH));
        newDict.Add(HumanFeature.FeatureType.FT_WEIGHT, new WeightHumanFeature(FT_WEIGHT, HumanFeature.FeatureType.FT_WEIGHT));

        Debug.Log("CREO IL DIZIONARIO");

        return newDict;
    }
}

