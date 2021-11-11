using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : WeaponController
{
    public bool isAmmoInfinite = true;

    AudioSource audioSource;

    private void Start()
    {
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

        //Se non c'è la raffica allora spara solo un colpo e dopo finisce
        if (!isBurst)
        {
            audioSource.Play();
            StopFiring();
        }
    }
}