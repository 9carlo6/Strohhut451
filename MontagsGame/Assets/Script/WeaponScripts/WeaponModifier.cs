using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModifier
{
  public int fireRate;
  public int maxAmmoCount;
  public int ammoCount;
  public float damage;
  public bool isBurst;
  public TrailRenderer tracerEffect;

  //Costruttore
  public WeaponModifier(int fireRate, int maxAmmoCount, int ammoCount, float damage, bool isBurst, TrailRenderer tracerEffect)
  {
    this.fireRate = fireRate;
    this.maxAmmoCount = maxAmmoCount;
    this.ammoCount = ammoCount;
    this.damage = damage;
    this.isBurst = isBurst;
    this.tracerEffect = tracerEffect;
  }

  public int getFireRate(){
    return fireRate;
  }

  public int getMaxAmmoCount(){
    return maxAmmoCount;
  }

  public int getAmmoCount(){
    return ammoCount;
  }

  public float getDamage(){
    return damage;
  }

  public TrailRenderer getTracerEffect(){
    return tracerEffect;
  }
}
