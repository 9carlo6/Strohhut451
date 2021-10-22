using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    //Non è proprio corretto richiamare questa classe
    public WeaponController weapon;
    public int ammoDropped = 10;

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        weapon.DropAmmo(ammoDropped);
        Destroy(gameObject);
    }
}
