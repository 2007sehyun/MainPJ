using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Collider weaponLogic;
    [SerializeField] private Collider weaponLogic2;
    public bool IsAttack;
    public void EnableWeapon()
    {
        weaponLogic.enabled=true;
    }

    public void DisableWeapon()
    {
        weaponLogic.enabled = false;
    }
    public void SkillEnableWeapon()
    {
        weaponLogic2.enabled=true;
    }

    public void SkillDisableWeapon()
    {
        weaponLogic2.enabled = false;
        IsAttack = false;
    }
}
