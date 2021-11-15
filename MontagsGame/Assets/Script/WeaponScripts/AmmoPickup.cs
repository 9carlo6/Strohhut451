using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    //Non è proprio corretto richiamare questa classe
    [HideInInspector] public WeaponPlayerController weapon;
    public int ammoDropped = 5;

    void Awake()
    {
        weapon = GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponPlayerController>();
    }

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Pickup"); ;

        weapon.DropAmmo(ammoDropped);
        Destroy(gameObject);
    }
}
