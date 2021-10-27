using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //Per capire se si sta sparando o no
    public bool isFiring = false;

    //Per poter gestire il rate dello sparo
    public int fireRate = 25;

    //Per indicare il massimo numero di munizioni
    [HideInInspector] public int maxAmmoCount = 10;
    //Per gestire il numero di munizioni a disposizione
    public int ammoCount = 10;

    //Per gestire il danno dell'arma
    public float damage = 10;

    //Per gestire lo sparo (raffica o no)
    public bool isBurst = false;

    //Questo accumulatedTime sarebbe il tempo che deve passare per poter sparare il prossimo proiettile
    float accumulatedTime;

    //Per gestire il widget relativo alle munizioni
    public AmmoWidget ammoWidget;

    //Per gestire gli effetti particellari
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;

    //Per gestire il rendering di una scia di poligoni dietro un GameObject (scia proiettile)
    public TrailRenderer tracerEffect;

    //Per accedere alla posizione dell'origine del raycast
    public Transform raycastOrigin;
    Ray ray;
    RaycastHit hitInfo;

    void Awake()
    {

         ammoWidget.Refresh(ammoCount);

    }

    //Funzione chiamata quando si riceve l'input per lo sparo
    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0.0f;
        FireBullet();
    }

    //Funzione necessaria per gestire l'update dello sparo
    public void UpdateFiring(float deltaTime)
    {
        //Minore � il fireRate maggiore � il tempo che intercorre tra uno sparo e un'altro (quando si tiene premuto il pulsante per sparare)
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;

        while (accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    //Funzione per sparare
    public void FireBullet()
    {
        //Controllo sul numero delle munizioni disponibili
        if (ammoCount <= 0)
        {
            return;
        }

        //Per diminuire il numero di munizioni quando si spara
        ammoCount--;

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

        //Se non c'� la raffica allora spara solo un colpo e dopo finisce
        if (!isBurst)
        {
            StopFiring();
        }

        //Questo serve per aggiornare le munizioni visibili nel widget
      
            ammoWidget.Refresh(ammoCount);

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
        ammoWidget.Refresh(ammoCount);
    }

    //Funzione chiamata quando termina l'input per lo sparo
    public void StopFiring()
    {
        isFiring = false;
    }
}