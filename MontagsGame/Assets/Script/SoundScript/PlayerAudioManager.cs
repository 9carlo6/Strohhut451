using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
  /*  PlayerController playerController;
    WeaponController weaponController;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        weaponController = GetComponentInChildren<WeaponController>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        Running();
    }


    void Fire()
    {

        Debug.Log("Vedo se sta sparando " + weaponController.isFiring);
        Debug.Log("Stampo le munizioni " + weaponController.ammoCount);
        if (weaponController.isFiring == true)
        {
            audioManager.Play("NormalFire");
        }



        /*if (weaponController.isFiring && weaponController.ammoCount > 0 && !weaponController.isBurst)
        {
            audioManager.Play("NormalFire");
        }
   }

    void Running()
    {
        if (playerController.isMovementPressed)
        {
            FindObjectOfType<AudioManager>().Play("Running");
        }
        
    }*/
}
