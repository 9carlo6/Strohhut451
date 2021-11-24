using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using HumanFeatures;

public class WeaponPlayerController : WeaponController
{
    //Per gestire il widget relativo alle munizioni
    //[HideInInspector] public AmmoWidget ammoWidget;

    //Per gestire il puntatore dell'arma
    public GameObject weaponSight;
    Ray rayWeaponSight;
    RaycastHit hitInfoWeaponSight;

    

    public float noiseRange;
    public override void Awake()
    {
        base.Awake();
        weaponSight = GameObject.FindWithTag("WeaponSight");

        //Inizio - Inizializzazione delle feature
       	string fileString = new StreamReader("Assets/Push-To-Data/Feature/Weapon/player_weapon_features.txt").ReadToEnd();
        mapper = JsonUtility.FromJson<WeaponFeaturesJsonMap>(fileString);

        //this.features = new Dictionary<HumanFeature.FeatureType, HumanFeature>();
        this.features = mapper.todict();

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

    public override void Update() {

        base.Update();

       

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

   

}
