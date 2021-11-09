using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    //Non è proprio corretto richiamare questa classe
    public WeaponPlayerController weapon;
    public int ammoDropped = 10;

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        // FindObjectOfType<AudioManager>().Play("Suono del tocco di questo oggetto");


        weapon.DropAmmo(ammoDropped);
        Destroy(gameObject);
    }
}
