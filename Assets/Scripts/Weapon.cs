using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class Weapon : MonoBehaviour, IPointerDownHandler
{

    public int Damage;
    public int HpWp;
    public int IdWp;
    public bool IsСhanges;
    public Image Weapon_img;

    public UnityEvent eventReturnState;

    private StateInfoGamePlay stateInfo;


    private void SearchSelectedImage() => Weapon_img.sprite = Resources.Load<Sprite>("SlectedWeapons/" + Weapon_img.sprite.name + " selected");

   


    public void OnPointerDown(PointerEventData eventData)
    {

        eventReturnState.Invoke();
        stateInfo.GetInfoCurrentWepon(this);
        SearchSelectedImage();

        IsСhanges = true;


    }


    private void Start() => stateInfo = GameObject.FindGameObjectWithTag("Script").GetComponent<StateInfoGamePlay>();



    private void Awake() => HpWp = 100;













}






