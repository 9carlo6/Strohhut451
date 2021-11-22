using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using HumanFeatures;


public class EnemyWeaponController : WeaponController
{
    public bool isAmmoInfinite = true;

    public AudioSource audioSource;
    
    public override void Awake()
    {
        base.Awake();
        //Inizio - Inizializzazione delle feature
       	string fileString = new StreamReader("Assets/Push-To-Data/Feature/Weapon/enemy_weapon_features.txt").ReadToEnd();
       	weaponMapper = JsonUtility.FromJson<WeaponFeaturesJsonMap>(fileString);

        this.features = new Dictionary<HumanFeature.FeatureType, HumanFeature>();
        this.features = weaponMapper.todict();


        audioSource = GetComponent<AudioSource>();
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
            audioSource.Play();
            StopFiring();
        }
    }

    
}
