using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HumanFeature : Feature
{
  public enum FeatureType
  {
    FT_SPEED,
    FT_HEALTH,
    FT_ATTACK_RANGE,
    FT_MELEE_DAMAGE
  }

  public System.Object baseValue;
  public System.Object currentValue;
  public FeatureType featureName;

  public HumanFeature(System.Object baseValue, FeatureType featureName){
    this.baseValue = baseValue;
    this.currentValue = baseValue;
    this. featureName = featureName;
  }

}
