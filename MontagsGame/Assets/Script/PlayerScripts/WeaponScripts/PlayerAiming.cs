using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Questa classe serve per gesitire il comportamento legato allo sparo del personaggio
public class PlayerAiming : MonoBehaviour 
{ 
    //serve per prendere l'input dello sparo del personaggio con il new input System
    PlayerInput playerInput;
    CharacterController characterController;

    //Per collegarsi alla classe che gestisce l'animazione dello sparo
    RaycastWeapon weapon;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        weapon = GetComponentInChildren<RaycastWeapon>();
    }
    
    void Start()
    {
        //Callbacks
        playerInput.CharacterControls.Fire.performed += _ => weapon.StartFiring();
        playerInput.CharacterControls.Fire.canceled += _ => weapon.StopFiring();
    }

    private void LateUpdate()
    {
        if (weapon.isFiring)
        {
            weapon.UpdateFiring(Time.deltaTime);
        }

        //weapon.UpdateBullets(Time.deltaTime);
    }

    void OnEnable()
    {
        //serve per abilitare la character controls action map
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        //serve per disabilitare la character controls action map
        playerInput.CharacterControls.Disable();
    }
}
