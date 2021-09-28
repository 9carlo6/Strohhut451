using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public EnemyHealthManager enemyHealth;

    public void OnRaycastHit(RaycastWeapon weapon, Vector3 direction)
    {
        enemyHealth.TakeDamage(weapon.damage, direction);
    }
}
