using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoWidget : MonoBehaviour
{
    public TMPro.TMP_Text ammoText;
    public RaycastWeapon raycastWeapon;


    public void Refresh(int ammoCount)
    {
        ammoText.text = ammoCount.ToString() + "/" + raycastWeapon.maxAmmoCount.ToString();
    }
}
