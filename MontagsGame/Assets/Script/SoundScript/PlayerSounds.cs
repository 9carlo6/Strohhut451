using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    AudioManager audioManager;
    PlayerController playerController;
    WeaponController weaponController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        weaponController = GetComponentInChildren<WeaponController>();
        audioManager = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        Running();
        

    }

    void Fire()
    {
        if (weaponController.isFiring && weaponController.ammoCount > 0 && !weaponController.isBurst)
        {
  
                FindObjectOfType<AudioManager>().Play("NormalFire");

        }

   }

    //da gestire un po meglio
    void Running()
    {
        if (!playerController.isMovementPressed)
        {
            FindObjectOfType<AudioManager>().Play("running");
        }
        
    }
}
