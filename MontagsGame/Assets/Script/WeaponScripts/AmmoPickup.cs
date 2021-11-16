using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    //Non � proprio corretto richiamare questa classe
    [HideInInspector] public WeaponPlayerController weapon;
    public int ammoDropped = 5;

    void Awake()
    {
        weapon = GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponPlayerController>();
    }

    //Funzione che si attiva quando l'oggetto viene toccato
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Recharge");

        weapon.DropAmmo(ammoDropped);
        Destroy(gameObject);
    }
}
