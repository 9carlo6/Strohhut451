using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EnemyFeatures;

[Serializable]
public class EnemyFeaturesJsonMap: Dictionable
{
  public float FT_VELOCITY;
  public float FT_ACCELERATION;
  public float FT_DECELERATION;
  public float FT_HEALTH;
  public float FT_MAX_HEALTH;
  public float FT_MELEE_RANGE;
  public float FT_MELEE_DAMAGE;
  public bool  FT_IS_WEAPONED;
  public float FT_FIRE_DISTANCE;
  public float FT_VIEW_RADIUS;
  public float FT_VIEW_ANGLE_PATROLLING;
  public float FT_VIEW_ANGLE_CHASING;
  public float FT_WEIGHT;


    public Dictionary<System.Object, Feature> todict()
    {

        if (FT_VELOCITY < 0) throw new DataException("FT_VELOCITY NON PUO ESSERE NEGATIVO");
        if (FT_ACCELERATION < 0) throw new DataException("FT_ACCELERATION NON PUO ESSERE NEGATIVO");
        if (FT_DECELERATION < 0) throw new DataException("FT_DECELERATION NON PUO ESSERE NEGATIVO");

        if (FT_HEALTH < 0 || FT_HEALTH > FT_MAX_HEALTH) throw new DataException("LA SALUTE NON PUO' ESSERE NEGATIVA O MAGGIORE DELLA SALUTE MASSIMA");
        if (FT_MAX_HEALTH < 0 || FT_MAX_HEALTH < FT_HEALTH) throw new DataException("LA SALUTE MASSIMA NON PUO' ESSERE NEGATIVA O MINORE DELLA SALUTE MASSIMA");

        if (FT_MELEE_RANGE < 0) throw new DataException("FT_MELEE_RANGE NON PUO ESSERE NEGATIVO");
        if (FT_MELEE_DAMAGE < 0) throw new DataException("FT_MELEE_DAMAGE NON PUO ESSERE NEGATIVO");
        if (FT_FIRE_DISTANCE < 0) throw new DataException("FT_FIRE_DISTANCE NON PUO ESSERE NEGATIVO");
        if (FT_VIEW_RADIUS < 0) throw new DataException("FT_VIEW_RADIUS NON PUO ESSERE NEGATIVO");
        if (FT_VIEW_ANGLE_PATROLLING < 0 || FT_VIEW_ANGLE_PATROLLING>360) throw new DataException("FT_VIEW_ANGLE_PATROLLING NON PUO ESSERE NEGATIVO");
        if (FT_VIEW_ANGLE_CHASING < 0 || FT_VIEW_ANGLE_CHASING > 360) throw new DataException("FT_VIEW_ANGLE_CHASING NON PUO ESSERE NEGATIVO");
        if (FT_WEIGHT < 0) throw new DataException("FT_WEIGHT NON PUO ESSERE NEGATIVO");


        Dictionary<System.Object, Feature> newDict = new Dictionary<System.Object, Feature>();

        newDict.Add(EnemyFeature.FeatureType.FT_VELOCITY, new SpeedEnemyFeature(FT_VELOCITY, EnemyFeature.FeatureType.FT_VELOCITY));
        newDict.Add(EnemyFeature.FeatureType.FT_ACCELERATION, new AccelerationEnemyFeature(FT_ACCELERATION, EnemyFeature.FeatureType.FT_ACCELERATION));
        newDict.Add(EnemyFeature.FeatureType.FT_DECELERATION, new DecelerationEnemyFeature(FT_DECELERATION, EnemyFeature.FeatureType.FT_DECELERATION));
        newDict.Add(EnemyFeature.FeatureType.FT_HEALTH, new HealthEnemyFeature(FT_HEALTH, EnemyFeature.FeatureType.FT_HEALTH));
        newDict.Add(EnemyFeature.FeatureType.FT_WEIGHT, new WeightEnemyFeature(FT_WEIGHT, EnemyFeature.FeatureType.FT_WEIGHT));
        newDict.Add(EnemyFeature.FeatureType.FT_MAX_HEALTH, new MaxHealthEnemyFeature(FT_MAX_HEALTH, EnemyFeature.FeatureType.FT_MAX_HEALTH));
        newDict.Add(EnemyFeature.FeatureType.FT_MELEE_RANGE, new MeleeRangeEnemyFeature(FT_MELEE_RANGE, EnemyFeature.FeatureType.FT_MELEE_RANGE));
        newDict.Add(EnemyFeature.FeatureType.FT_MELEE_DAMAGE, new MeleeDamageEnemyFeature(FT_MELEE_DAMAGE, EnemyFeature.FeatureType.FT_MELEE_DAMAGE));
        newDict.Add(EnemyFeature.FeatureType.FT_IS_WEAPONED, new IsWeaponedEnemyFeature(false, EnemyFeature.FeatureType.FT_IS_WEAPONED));
        newDict.Add(EnemyFeature.FeatureType.FT_FIRE_DISTANCE, new FireDistanceEnemyFeature(FT_FIRE_DISTANCE, EnemyFeature.FeatureType.FT_FIRE_DISTANCE));
        newDict.Add(EnemyFeature.FeatureType.FT_VIEW_RADIUS, new ViewRadiusEnemyFeature(FT_VIEW_RADIUS, EnemyFeature.FeatureType.FT_VIEW_RADIUS));
        newDict.Add(EnemyFeature.FeatureType.FT_VIEW_ANGLE_PATROLLING, new ViewAnglePatrollingEnemyFeature(FT_VIEW_ANGLE_PATROLLING, EnemyFeature.FeatureType.FT_VIEW_ANGLE_PATROLLING));
        newDict.Add(EnemyFeature.FeatureType.FT_VIEW_ANGLE_CHASING, new ViewAngleChasingEnemyFeature(FT_VIEW_ANGLE_CHASING, EnemyFeature.FeatureType.FT_VIEW_ANGLE_CHASING));

        return newDict;
    }
}
