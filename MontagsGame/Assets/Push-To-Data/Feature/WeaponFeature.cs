using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFeature : Feature
{
  public enum FeatureType
  {
    FT_FIRE_RATE,
    FT_MAX_AMMO_COUNT,
    FT_AMMO_COUNT,
    FT_DAMAGE,
    FT_BURST,
    FT_TRACER_EFFECT
  }

  Object baseValue;
  Object currentValue;
  FeatureType featureName;

  public WeaponFeature(Object baseValue, FeatureType featureName){
    this.baseValue = baseValue;
    this.currentValue = baseValue;
    this. featureName = featureName;
  }
}
