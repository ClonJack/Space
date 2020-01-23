using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;


public enum SetProperty
{
    One, Two, Three
};

public class Weapon : MonoBehaviour, IPointerDownHandler
{


    public int Damage;
    public int HpWp;
    public int IdWp;
    public bool IsСhanges;
    public Image Weapon_img;
    public SetProperty Wp_Property;
    public float Time;



    private StateInfoGamePlay stateInfo;


    private void SearchSelectedImage() => Weapon_img.sprite = Resources.Load<Sprite>("SlectedWeapons/" + Weapon_img.sprite.name + " selected");




    public void OnPointerDown(PointerEventData eventData)
    {

    
        stateInfo.GetInfoCurrentWepon(this);
      
        stateInfo.playAnimations.BigShip();
        stateInfo.IsAttackWeaopons = true;

        if (!IsСhanges)
        {
           
           SearchSelectedImage();
            stateInfo.weapons.Add(this);

            IsСhanges = true;
        }

    }


    private void Start() => stateInfo = GameObject.FindGameObjectWithTag("Script").GetComponent<StateInfoGamePlay>();



    private void Awake() => HpWp = 100;













}






