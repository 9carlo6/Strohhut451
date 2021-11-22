using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HumanFeatures;

[Serializable]
public class HumanFeaturesJsonMap
{
    public float FT_SPEED;
    public float FT_HEALTH;
    public float FT_ATTACK_RANGE;
    public float FT_MELEE_DAMAGE;
    public bool FT_INCREASED_FOV;

    public Dictionary<HumanFeatures.HumanFeature.FeatureType, HumanFeature> todict()
    {
        Dictionary<HumanFeatures.HumanFeature.FeatureType, HumanFeature> newDict =  new Dictionary<HumanFeatures.HumanFeature.FeatureType, HumanFeature>();

        newDict.Add(HumanFeature.FeatureType.FT_SPEED ,new SpeedHumanFeature(FT_SPEED, HumanFeature.FeatureType.FT_SPEED));
        newDict.Add(HumanFeature.FeatureType.FT_HEALTH, new HealthHumanFeature(FT_HEALTH, HumanFeature.FeatureType.FT_HEALTH));
        newDict.Add(HumanFeature.FeatureType.FT_ATTACK_RANGE, new AttackRangeHumanFeature(FT_ATTACK_RANGE, HumanFeature.FeatureType.FT_ATTACK_RANGE));
        newDict.Add(HumanFeature.FeatureType.FT_MELEE_DAMAGE, new MeleeDamageHumanFeature(FT_MELEE_DAMAGE, HumanFeature.FeatureType.FT_MELEE_DAMAGE));
        newDict.Add(HumanFeature.FeatureType.FT_INCREASED_FOV, new IncreasedFovHumanFeature(FT_INCREASED_FOV, HumanFeature.FeatureType.FT_INCREASED_FOV));


        return newDict;
    }
}

