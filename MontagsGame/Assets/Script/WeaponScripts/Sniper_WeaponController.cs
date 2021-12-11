using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using HumanFeatures;


public class Sniper_WeaponController : WeaponController
{
    public AudioSource audioSource;

    public override void Awake()
    {
        base.Awake();

        //Inizio - Inizializzazione delle feature
        string fileString = new StreamReader("Assets/Push-To-Data/Feature/Weapon/sniper_rifle_features.txt").ReadToEnd();
        mapper = JsonUtility.FromJson<WeaponFeaturesJsonMap>(fileString);

        this.features = mapper.todict();

        audioSource = GetComponent<AudioSource>();
    }

    public override void Update()
    {
        base.Update();
    }

    //Funzione per sparare
    public override void FireBullet()
    {
        //Controllo sul numero delle munizioni disponibili
        if ((int)features[WeaponFeatures.WeaponFeature.FeatureType.FT_AMMO_COUNT].currentValue <= 0)
        {
            return;
        }

        if (!(bool)features[WeaponFeatures.WeaponFeature.FeatureType.FT_IS_AMMO_INFINITE].currentValue)
        {
            //Per diminuire il numero di munizioni quando si spara
            features[WeaponFeatures.WeaponFeature.FeatureType.FT_AMMO_COUNT].currentValue = (int)features[WeaponFeatures.WeaponFeature.FeatureType.FT_AMMO_COUNT].currentValue - 1;
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
                hitPlayerCollider.HurtPlayer((float)features[WeaponFeatures.WeaponFeature.FeatureType.FT_DAMAGE].currentValue);
            }
        }

        //Se non c'� la raffica allora spara solo un colpo e dopo finisce
        if (!(bool)features[WeaponFeatures.WeaponFeature.FeatureType.FT_BURST].currentValue)
        {
            audioSource.Play();
            StopFiring();
        }
    }
}