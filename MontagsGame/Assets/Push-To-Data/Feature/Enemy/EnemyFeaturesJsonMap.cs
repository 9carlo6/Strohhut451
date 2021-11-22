using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EnemyFeatures;

[Serializable]
public class EnemyFeaturesJsonMap
{
  public float FT_VELOCITY;
  public float FT_ACCELERATION;
  public float FT_DECELERATION;
  public float FT_HEALTH;
  public float FT_MELEE_RANGE;
  public float FT_MELEE_DAMAGE;
  public bool  FT_IS_WEAPONED;
  public float FT_FIRE_DISTANCE;
  public float FT_VIEW_RADIUS;
  public float FT_VIEW_ANGLE_PATROLLING;
  public float FT_VIEW_ANGLE_CHASING;


    public Dictionary<EnemyFeature.FeatureType, EnemyFeature> todict()
    {
        Dictionary<EnemyFeature.FeatureType, EnemyFeature> newDict = new Dictionary<EnemyFeature.FeatureType, EnemyFeature>();

        newDict.Add(EnemyFeature.FeatureType.FT_VELOCITY, new SpeedEnemyFeature(FT_VELOCITY, EnemyFeature.FeatureType.FT_VELOCITY));
        newDict.Add(EnemyFeature.FeatureType.FT_ACCELERATION, new AccelerationEnemyFeature(FT_ACCELERATION, EnemyFeature.FeatureType.FT_ACCELERATION));
        newDict.Add(EnemyFeature.FeatureType.FT_DECELERATION, new DecelerationEnemyFeature(FT_DECELERATION, EnemyFeature.FeatureType.FT_DECELERATION));
        newDict.Add(EnemyFeature.FeatureType.FT_HEALTH, new HealthEnemyFeature(FT_HEALTH, EnemyFeature.FeatureType.FT_HEALTH));
        newDict.Add(EnemyFeature.FeatureType.FT_MELEE_RANGE, new MeleeRangeEnemyFeature(FT_MELEE_RANGE, EnemyFeature.FeatureType.FT_MELEE_RANGE));
        newDict.Add(EnemyFeature.FeatureType.FT_MELEE_DAMAGE, new MeleeDamageEnemyFeature(FT_MELEE_DAMAGE, EnemyFeature.FeatureType.FT_MELEE_DAMAGE));
        newDict.Add(EnemyFeature.FeatureType.FT_IS_WEAPONED, new IsWeaponedEnemyFeature(FT_IS_WEAPONED, EnemyFeature.FeatureType.FT_IS_WEAPONED));
        newDict.Add(EnemyFeature.FeatureType.FT_FIRE_DISTANCE, new FireDistanceEnemyFeature(FT_FIRE_DISTANCE, EnemyFeature.FeatureType.FT_FIRE_DISTANCE));
        newDict.Add(EnemyFeature.FeatureType.FT_VIEW_RADIUS, new ViewRadiusEnemyFeature(FT_VIEW_RADIUS, EnemyFeature.FeatureType.FT_VIEW_RADIUS));
        newDict.Add(EnemyFeature.FeatureType.FT_VIEW_ANGLE_PATROLLING, new ViewAnglePatrollingEnemyFeature(FT_VIEW_ANGLE_PATROLLING, EnemyFeature.FeatureType.FT_VIEW_ANGLE_PATROLLING));
        newDict.Add(EnemyFeature.FeatureType.FT_VIEW_ANGLE_CHASING, new ViewAngleChasingEnemyFeature(FT_VIEW_ANGLE_CHASING, EnemyFeature.FeatureType.FT_VIEW_ANGLE_CHASING));

        return newDict;
    }
}
