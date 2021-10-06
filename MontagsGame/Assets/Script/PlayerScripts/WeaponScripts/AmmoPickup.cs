using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    //Non è proprio corretto richiamare questa classe
    public RaycastWeapon raycastWeapon;

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        raycastWeapon.DropAmmo(5);
        Destroy(gameObject);
    }
}
