﻿using UnityEngine;
using System.Collections;

public class Combatant : MonoBehaviour
{
    public float health = 100;
    public float invincibilityTime = 0.5f;

    public float attackTime = 0.8f;
    public float delayBetweenAttacks = 2.0f;
    public WeaponScript weapon;
    public Transform weaponBind;

    public bool invincible = false;
    public bool readyToAttack = true;

    void Start()
    {
        AddWeapon(weapon);
    }

    public void TakeDamage(float amount)
    {
        if (!invincible)
        {
            health -= amount;
            invincible = true;

            if (health <= 0)
            {
                //gameObject.GetComponent<PlayerScript>().Kill();
            }
            FinishAttack();  //deactivate weapon and stun
            Invoke("FinishInvincibility", invincibilityTime);
        }
    }

    public void AddWeapon(WeaponScript newWeapon)
    {
        if (newWeapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = GameObject.Instantiate(newWeapon) as WeaponScript;
            weapon.transform.parent = weaponBind;
            weapon.Deactivate();
        }
    }

    public bool Attack()
    {
        if (readyToAttack)
        {
            weapon.Activate();
            Invoke("FinishAttack", attackTime);
            readyToAttack = false;

            return true;
        }
        return false;
    }

    public void FinishAttack()
    {
        weapon.Deactivate();
        readyToAttack = false;
        Invoke("ReadyAttack", delayBetweenAttacks);
    }

    public void ReadyAttack()
    {
        readyToAttack = true;
    }

    public void FinishInvincibility()
    {
        invincible = false;
    }
}