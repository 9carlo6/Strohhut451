using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyWeaponController : WeaponController
{
    public bool isAmmoInfinite = true;

    public override void Awake()
    {
        //Inizio - Inizializzazione delle feature
       	string fileString = new StreamReader("Assets/Push-To-Data/Feature/Weapon/enemy_weapon_features.txt").ReadToEnd();
       	weaponMapper = JsonUtility.FromJson<WeaponFeaturesJsonMap>(fileString);

       	features = new Dictionary<string, WeaponFeature>();
       	features.Add("fireRate", new WeaponFeature(weaponMapper.FT_FIRE_RATE, WeaponFeature.FeatureType.FT_FIRE_RATE));
       	features.Add("maxAmmoCount", new WeaponFeature(weaponMapper.FT_MAX_AMMO_COUNT, WeaponFeature.FeatureType.FT_MAX_AMMO_COUNT));
       	features.Add("ammoCount", new WeaponFeature(weaponMapper.FT_AMMO_COUNT, WeaponFeature.FeatureType.FT_AMMO_COUNT));
       	features.Add("damage", new WeaponFeature(weaponMapper.FT_DAMAGE, WeaponFeature.FeatureType.FT_DAMAGE));
       	features.Add("isBurst", new WeaponFeature(weaponMapper.FT_BURST, WeaponFeature.FeatureType.FT_BURST));
       	features.Add("weight", new WeaponFeature(weaponMapper.FT_WEIGHT, WeaponFeature.FeatureType.FT_WEIGHT));
       	//features.Add("tracerEffect", new WeaponFeature(weaponMapper.FT_MELEE_DAMAGE, WeaponFeature.FeatureType.FT_MELEE_DAMAGE));

       	//Da eliminare???
       	fireRate = (int) features["fireRate"].currentValue;
       	maxAmmoCount  = (int) features["maxAmmoCount"].currentValue;
       	ammoCount  = (int) features["ammoCount"].currentValue;
       	damage  = (float) features["damage"].currentValue;
       	isBurst  = (bool) features["isBurst"].currentValue;
        //Fine - Inizializzazione delle feature
    }

    //Funzione per sparare
    public override void FireBullet()
    {
        //Controllo sul numero delle munizioni disponibili
        if (ammoCount <= 0)
        {
            return;
        }

        if (!isAmmoInfinite)
        {
            //Per diminuire il numero di munizioni quando si spara
            ammoCount--;
        }

        //Questo ciclo permette di azionare tutti gli oggetti particellari in muzzleFlash
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        ray.origin = raycastOrigin.position;
        ray.direction = raycastOrigin.forward;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo))
        {
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;

            //Per la gestione del danno al nemico in seguito alla collisione
            var hitPlayerCollider = hitInfo.collider.GetComponent<PlayerHealthManager>();
            if (hitPlayerCollider)
            {
                hitPlayerCollider.HurtPlayer(damage);
            }
        }

        //Se non c'� la raffica allora spara solo un colpo e dopo finisce
        if (!isBurst)
        {
            StopFiring();
        }
    }

    public override float GetWeight()
    {
      return (float) features["weight"].currentValue;
    }
}
