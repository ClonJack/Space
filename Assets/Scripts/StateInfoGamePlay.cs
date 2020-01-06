using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateInfoGamePlay : MonoBehaviour
{
    public Weapon Currentweapon;

    public GameObject WeaponDamage;

    public void Start()
    {
        WeaponDamage = GameObject.FindGameObjectWithTag("Damage");

        WeaponDamage?.SetActive(false);

    }



    public void GetInfoCurrentWepon(Weapon weaponsGoodShip) => Currentweapon = weaponsGoodShip;
    
    
   



}

