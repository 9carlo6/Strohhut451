using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WeaponController : MonoBehaviour, Component
{
    //Per capire se si sta sparando o no
    public bool isFiring = false;

    //Per poter gestire il rate dello sparo
    public int fireRate = 1;

    //Per indicare il massimo numero di munizioni
    [HideInInspector] public int maxAmmoCount = 10;
    //Per gestire il numero di munizioni a disposizione
    public int ammoCount = 10;

    //Per gestire il danno dell'arma
    public float damage = 10;

    //Per gestire lo sparo (raffica o no)
    public bool isBurst = false;

    //Questo accumulatedTime sarebbe il tempo che deve passare per poter sparare il prossimo proiettile
    public float accumulatedTime;

    //Per gestire gli effetti particellari
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;

    //Per gestire il rendering di una scia di poligoni dietro un GameObject (scia proiettile)
    public TrailRenderer tracerEffect;

    //Per accedere alla posizione dell'origine del raycast
    public Transform raycastOrigin;
    public Ray ray;
    public RaycastHit hitInfo;

    //Per gestire le feature;
  	public Dictionary<string, WeaponFeature> features;
    public WeaponFeaturesJsonMap weaponMapper;

    //Per la gestione dei moficatori
    //public WeaponModifier weaponModifier;
    //public Text jsonWeaponModifier;

    
    public AudioManager audioManager;


    public virtual void Awake()
    {
      audioManager = GetComponent<AudioManager>();
    }

    //Funzione necessaria per gestire l'update dello sparo
    public void UpdateFiring(float deltaTime)
    {
        isFiring = true;

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
    public virtual void FireBullet()
    {

    }

    public virtual void Update()
    {
        //Per gestire i modificatori
        //handleWeaponModifier();
        if (isFiring)
        {
           
            UpdateFiring(Time.deltaTime);
        }
       
    }

    //Funzione chiamata quando termina l'input per lo sparo
    public void StopFiring()
    {
        isFiring = false;
    }

    public virtual float GetWeight()
    {
      return -1;
    }

}
