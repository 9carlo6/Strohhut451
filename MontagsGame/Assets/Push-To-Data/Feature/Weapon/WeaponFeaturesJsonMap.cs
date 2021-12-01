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
        if (FT_FIRE_RATE < 0) throw new DataException("IL FIRE RATE NON PUO ASSUMERE VALORI <0");
        if (FT_MAX_AMMO_COUNT < 0 || FT_MAX_AMMO_COUNT < FT_AMMO_COUNT) throw new DataException("IL MAX AMMO COUNT NON PUO ASSUMERE VALORI <0 o MINORI DEL CURRENT AMMO COUNT");
        if (FT_AMMO_COUNT < 0 || FT_AMMO_COUNT > FT_MAX_AMMO_COUNT) throw new DataException(" AMMO COUNT NON PUO ASSUMERE VALORI <0 o MAGGIORI DEL CURRENT AMMO COUNT");
        if (FT_DAMAGE < 0) throw new DataException(" IL DAMAGE NON PUO ASSUMERE VALORI NEGATIVI");
        if (FT_WEIGHT < 0) throw new DataException(" IL PESO NON PUO ASSUMERE VALORI NEGATIVI");
        if (FT_NOISE_RANGE < 0) throw new DataException(" IL FT_NOISE_RANGE NON PUO ASSUMERE VALORI NEGATIVI");
        if (FT_CHANCE_OF_SHOOTING < 0 || FT_CHANCE_OF_SHOOTING > 100) throw new DataException(" Il FT_CHANCE_OF_SHOOTING NON PUO ASSOMERE VALORI NON COMPRESI IN [0,100]");

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
