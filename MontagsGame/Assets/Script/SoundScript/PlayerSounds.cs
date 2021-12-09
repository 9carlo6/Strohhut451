using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    AudioManager audioManager;
    PlayerController playerController;
    WeaponController weaponController;
    Coin coin;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        weaponController = GetComponentInChildren<WeaponController>();
        audioManager = GetComponent<AudioManager>();
        coin = GetComponent<Coin>();
    }

    // Update is called once per frame
    void Update()
    {
        FireSound();
        RunningSound();
        CoinSound();
    }

    void FireSound()
    {
        if (weaponController.isFiring && (float) weaponController.features[WeaponFeatures.WeaponFeature.FeatureType.FT_AMMO_COUNT].currentValue > 0 && !(bool) weaponController.features[WeaponFeatures.WeaponFeature.FeatureType.FT_BURST].currentValue)
        {
                FindObjectOfType<AudioManager>().Play("NormalFire");
        }
   }

    //da gestire un po meglio
    void RunningSound()
    {
        if (!playerController.isMovementPressed)
        {
            FindObjectOfType<AudioManager>().Play("running");
        }
    }

    void CoinSound()
    {
        /*if (coin.taking)
        {
            FindObjectOfType<AudioManager>().Play("Coin");
        }*/
    }
}