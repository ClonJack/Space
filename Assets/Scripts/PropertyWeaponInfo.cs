using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class PropertyWeaponInfo : MonoBehaviour
{
    [SerializeField] private Weapon[] weaponsGoodShip;
    [SerializeField] private Mechanism[] mechanismsBadShip;
    [SerializeField] private StateInfoGamePlay stateInfo;




    private void Awake() => stateInfo = GetComponent<StateInfoGamePlay>();

    private void Start() => SetPropertyWeapons();



    private ShipInit ship = null;

    private void SetPropertyWeapons()//метод частичной инициализации для свойст(пушек,механизмов)
    {
        ship = GetComponent<ShipInit>();

        weaponsGoodShip = new Weapon[ship.goodShip.CountMechanisms];

        for (int i = 0; i < weaponsGoodShip.Length; i++)
        {

            weaponsGoodShip[i] = ship.goodShip.MechanismsShip.GetChild(i).GetComponent<Weapon>();

            weaponsGoodShip[i].eventReturnState = new UnityEvent();
            weaponsGoodShip[i].eventReturnState.AddListener(ReturnStateWeaponGameObj);

            weaponsGoodShip[i].IdWp = i;
            weaponsGoodShip[i].Weapon_img = ship.goodShip.MechanismsShip.GetChild(i).GetComponent<Image>();


        }

        /*--------------------------------------------------------------------------------*/

        mechanismsBadShip = new Mechanism[ship.enemyShip.CountMechanisms];

        for (int i = 0; i < mechanismsBadShip.Length; i++)
        {
            mechanismsBadShip[i] = ship.enemyShip.MechanismsShip.GetChild(i).GetComponent<Mechanism>();

            mechanismsBadShip[i].eventGetDamage = new UnityEvent();
            mechanismsBadShip[i].eventGetDamage.AddListener(DoDamge);
            mechanismsBadShip[i].idMechanism = i;
            mechanismsBadShip[i].Hp = 100;
            mechanismsBadShip[i].Ship = ship;
            mechanismsBadShip[i].Mechanism_img = ship.enemyShip.MechanismsShip.GetChild(i).GetComponent<Image>();


        }







    }






    private void ReturnStateWeaponGameObj()
    {

     /*   for (int i = 0; i < weaponsGoodShip.Length; i++)
        {
            int Length = weaponsGoodShip[i].Weapon_img.sprite.name.Length;

            if (weaponsGoodShip[i].IsСhanges)
            {


                string newstr = weaponsGoodShip[i].Weapon_img.sprite.name.Replace(" selected", "");

                weaponsGoodShip[i].Weapon_img.sprite = Resources.Load<Sprite>("SimpleWeapons/" + newstr);
            }
            

        }
        */




    }



    private void DoDamge()
    {


        for (int i = 0; i < mechanismsBadShip.Length; i++)
        {


            if (mechanismsBadShip[i].IsMechanism && stateInfo.Currentweapon != null && !stateInfo.WeaponDamage.activeSelf)
            {
                if (mechanismsBadShip[i].Hp > 0)
                {

                    mechanismsBadShip[i].Hp -= stateInfo.Currentweapon.Damage;

                    mechanismsBadShip[i].IsMechanism = false;
                    stateInfo.WeaponDamage.SetActive(true);



                    if (mechanismsBadShip[i].Hp <= 0)
                        mechanismsBadShip[i].Mechanism_img.sprite = Resources.Load<Sprite>("DamagedWeapons/" + mechanismsBadShip[i].Mechanism_img.sprite.name + " damaged");

                }
            }

            else if (mechanismsBadShip[i].IsMechanism && stateInfo.Currentweapon == null)
                mechanismsBadShip[i].IsMechanism = false;




        }







    }

}
