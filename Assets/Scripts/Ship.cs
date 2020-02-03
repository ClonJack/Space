using UnityEngine;
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
        if(MechanismsShip!=null)
        CountMechanisms = MechanismsShip.childCount;     


        sliderHpShip.maxValue = HpShip;
        sliderHpShip.value = HpShip;



    }


   



  

}

