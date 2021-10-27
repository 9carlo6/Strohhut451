using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoWidget : MonoBehaviour
{
    public TMPro.TMP_Text ammoText;
    public WeaponController weapon;


    public void Refresh(int ammoCount)
    {
        ammoText.text = ammoCount.ToString() + "/" + weapon.maxAmmoCount.ToString();
    }
}
