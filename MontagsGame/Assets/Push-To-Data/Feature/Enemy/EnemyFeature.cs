using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyFeature : Feature
{
  public enum FeatureType
  {
    FT_VELOCITY,
    FT_ACCELERATION,
    FT_DECELERATION,
    FT_HEALTH,
    FT_MELEE_RANGE,
    FT_MELEE_DAMAGE,
    FT_IS_WEAPONED,
    FT_FIRE_DISTANCE,
    FT_VIEW_ANGLE_CHASING,
    FT_VIEW_ANGLE_PATROLLING,
    FT_VIEW_RADIUS
    }

  public System.Object baseValue;
  public System.Object currentValue;
  public FeatureType featureName;

  public EnemyFeature(System.Object baseValue, FeatureType featureName){
    this.baseValue = baseValue;
    this.currentValue = baseValue;
    this. featureName = featureName;
  }

}
