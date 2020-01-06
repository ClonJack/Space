using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mechanism : MonoBehaviour,IPointerDownHandler,IPointerExitHandler
{
    public int Hp;
    public int idMechanism;
    public bool IsMechanism;

    public Image Mechanism_img;
   
    public ShipInit Ship;

    public UnityEvent eventGetDamage;
    

    private void Awake() => Hp = 100;

  

    public void OnPointerDown(PointerEventData eventData)
    {
        IsMechanism = true;

        eventGetDamage.Invoke();
      
    }

    private void OnDestroy()
    {
        Ship.enemyShip.CountMechanisms--;
        Ship.enemyShip.HpShip = Ship.enemyShip.CountMechanisms * 10;
        Ship.enemyShip.sliderHpShip.value = Ship.enemyShip.HpShip;

      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }
}
