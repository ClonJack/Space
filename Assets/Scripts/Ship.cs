﻿using UnityEngine;
using UnityEngine.UI;


public abstract class Ship
{

    public Transform Ship_;
    public Transform MechanismsShip;
    
    public int CountMechanisms;
    public int HpShip;
  
    public Slider sliderHpShip;



   
    


    public virtual void DateShip()
    {

        CountMechanisms = MechanismsShip.childCount;

        HpShip = CountMechanisms * 10;//кол-во хп зависит от кол-во механизмов 


     


        sliderHpShip.maxValue = HpShip;
        sliderHpShip.value = HpShip;



    }


   



  

}
