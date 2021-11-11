using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WeaponPlayerController : WeaponController
{
    //Per gestire il widget relativo alle munizioni
    //[HideInInspector] public AmmoWidget ammoWidget;

    //Per gestire il puntatore dell'arma
    public GameObject weaponSight;
    Ray rayWeaponSight;
    RaycastHit hitInfoWeaponSight;

    public override void Awake()
    {
        weaponSight = GameObject.FindWithTag("WeaponSight");

        //Inizio - Inizializzazione delle feature
       	string fileString = new StreamReader("Assets/Push-To-Data/Feature/Weapon/player_weapon_features.txt").ReadToEnd();
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


        //ammoWidget.Refresh(ammoCount);
    }

    //Funzione per sparare
    public override void FireBullet()
    {
        //Controllo sul numero delle munizioni disponibili
        if (ammoCount <= 0)
        {
            return;
        }

        //Per diminuire il numero di munizioni quando si spara
        ammoCount--;
        if (!isBurst)
            FindObjectOfType<AudioManager>().Play("NormalFire");

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
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);

            //Per la gestione del dallo al nemico in seguito alla collisione
            var hitEnemyCollider = hitInfo.collider.GetComponent<EnemyHealthManager>();
            if (hitEnemyCollider)
            {
                hitEnemyCollider.TakeDamage(damage);
            }
        }

        //Se non c'è la raffica allora spara solo un colpo e dopo finisce
        if (!isBurst)
        {
            StopFiring();
        }
        //Questo serve per aggiornare le munizioni visibili nel widget
        //ammoWidget.Refresh(ammoCount);
    }

    //Per gestire il puntatore
    public void handleWeaponSight()
    {
        rayWeaponSight.origin = raycastOrigin.position;
        rayWeaponSight.direction = raycastOrigin.forward;

        if (Physics.Raycast(rayWeaponSight, out hitInfoWeaponSight))
        {
            //Debug.DrawLine(ray2.origin, hitInfo2.point, Color.red, 1.0f);
            weaponSight.transform.position = new Vector3(hitInfoWeaponSight.point.x, 1, hitInfoWeaponSight.point.z);
        }
    }

    public override void Update()
    {
        //Per gestire i modificatori
        //handleWeaponModifier();
        if (isFiring)
        {
            UpdateFiring(Time.deltaTime);
        }

        //Per gestire il puntatore
        handleWeaponSight();
    }

    //Funzione per gestire il drop delle munizioni
    public void DropAmmo(int ammoDropCount)
    {
        if ((ammoCount + ammoDropCount) > maxAmmoCount)
        {
            ammoCount = maxAmmoCount;
        }
        else
        {
            ammoCount += ammoDropCount;
        }

        //Questo serve per aggiornare le munizioni visibili nel widget
        //ammoWidget.Refresh(ammoCount);
    }

    public override float GetWeight()
    {
      return (float) features["weight"].currentValue;
    }
}
