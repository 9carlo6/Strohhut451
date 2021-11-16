using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponFeature : Feature
{
  public enum FeatureType
  {
    FT_FIRE_RATE,
    FT_MAX_AMMO_COUNT,
    FT_AMMO_COUNT,
    FT_DAMAGE,
    FT_BURST,
    FT_TRACER_EFFECT,
    FT_WEIGHT,
    FT_NOISE_RANGE
    }

  public System.Object baseValue;
  public System.Object currentValue;
  public FeatureType featureName;

  public WeaponFeature(System.Object baseValue, FeatureType featureName){
    this.baseValue = baseValue;
    this.currentValue = baseValue;
    this. featureName = featureName;
  }

}
