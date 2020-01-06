using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipInit : MonoBehaviour
{



    public EnemyShip enemyShip;
    public GoodShip goodShip;



    public void RetunCurrentData()
    {
        enemyShip.CountMechanisms = enemyShip.MechanismsShip.childCount;

        enemyShip.HpShip = enemyShip.CountMechanisms * 10;
        enemyShip.sliderHpShip.value = enemyShip.HpShip;


    }



    private void IniitializedShips()
    {
   
        
        enemyShip.DateShip();
        /*--------*/
        goodShip.DateShip();





    }


    public void Awake()
    {
        IniitializedShips();



    }


}

[System.Serializable]
public class EnemyShip : Ship
{

}

[System.Serializable]
public class GoodShip : Ship
{


}




