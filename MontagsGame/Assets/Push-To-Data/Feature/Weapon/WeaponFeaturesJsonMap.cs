using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WeaponFeatures;

[Serializable]
public class WeaponFeaturesJsonMap:Dictionable
{
  public int  FT_FIRE_RATE;
  public int FT_MAX_AMMO_COUNT;
  public int  FT_AMMO_COUNT;
  public float FT_DAMAGE;
  public bool FT_BURST;
  public float FT_WEIGHT;
  public float FT_NOISE_RANGE;
  public float FT_CHANCE_OF_SHOOTING;
  public bool FT_IS_AMMO_INFINITE;

    public Dictionary<System.Object, Feature> todict()
    {
        Dictionary<System.Object, Feature> newDict = new Dictionary<System.Object, Feature>();

        newDict.Add(WeaponFeature.FeatureType.FT_FIRE_RATE, new FireRateWeaponFeature(FT_FIRE_RATE, WeaponFeature.FeatureType.FT_FIRE_RATE));
        newDict.Add(WeaponFeature.FeatureType.FT_MAX_AMMO_COUNT, new MaxAmmoCountWeaponFeature(FT_MAX_AMMO_COUNT, WeaponFeature.FeatureType.FT_MAX_AMMO_COUNT));
        newDict.Add(WeaponFeature.FeatureType.FT_AMMO_COUNT, new AmmoCountWeaponFeature(FT_AMMO_COUNT, WeaponFeature.FeatureType.FT_AMMO_COUNT));
        newDict.Add(WeaponFeature.FeatureType.FT_DAMAGE, new DamageWeaponFeature(FT_DAMAGE, WeaponFeature.FeatureType.FT_DAMAGE));
        newDict.Add(WeaponFeature.FeatureType.FT_BURST, new BurstWeaponFeature(FT_BURST, WeaponFeature.FeatureType.FT_BURST));
        newDict.Add(WeaponFeature.FeatureType.FT_WEIGHT, new WeightWeaponFeature(FT_WEIGHT, WeaponFeature.FeatureType.FT_WEIGHT));
        newDict.Add(WeaponFeature.FeatureType.FT_NOISE_RANGE, new NoiseRangeWeaponFeature(FT_NOISE_RANGE, WeaponFeature.FeatureType.FT_NOISE_RANGE));
        newDict.Add(WeaponFeature.FeatureType.FT_CHANCE_OF_SHOOTING, new ChancheOfShootingWeaponFeature(FT_CHANCE_OF_SHOOTING, WeaponFeature.FeatureType.FT_CHANCE_OF_SHOOTING));
        newDict.Add(WeaponFeature.FeatureType.FT_IS_AMMO_INFINITE, new AmmoInfiniteWeaponFeature(FT_IS_AMMO_INFINITE, WeaponFeature.FeatureType.FT_IS_AMMO_INFINITE));

        


        return newDict;
    }

}
