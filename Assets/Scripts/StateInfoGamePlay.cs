using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;


public class StateInfoGamePlay : MonoBehaviour
{
    public Weapon Currentweapon;
    public ShipInit ShipInit;
    public PlayAnimations playAnimations;

    public GameObject WeaponDamage;
    public bool IsAttackWeaopons;

    public Transform canvas;


    public List<ShotWeapon> shotWeaponGoodShip;
    public List<ShotWeapon> shotWeaponsBadShip;

    public Image[] CurrentImageElments;
    public Vector2[] CurrentImageElementsPositons;
    public Image[] SpawnCountElements;

    public Image DamageForEnemy,DamageForGoodShip;

    public AudioSource wp_audioSource;


    public int idCurrentShips;

    public List<Weapon> weapons;

  

    private void Awake()
    {
        weapons = new List<Weapon>();
        ShipInit = GetComponent<ShipInit>();
        playAnimations = GetComponent<PlayAnimations>();
    }

    private void Start()
    {

        for (int i = 0; i < shotWeaponGoodShip.Count; i++)
            shotWeaponGoodShip[i].PlayAnim("idle animation", true);

        CurrentImageElments = new Image[ShipInit.goodShip.CountMechanisms];
        CurrentImageElementsPositons = new Vector2[ShipInit.goodShip.CountMechanisms];
        SpawnCountElements = new Image[ShipInit.goodShip.CountMechanisms];

        WeaponDamage = GameObject.FindGameObjectWithTag("Damage");

        WeaponDamage?.SetActive(false);

    }



    public void GetInfoCurrentWepon(Weapon weaponsGoodShip) => Currentweapon = weaponsGoodShip;






}

[System.Serializable]
public class ShotWeapon
{


    public SkeletonGraphic skeletonGraphic;

    public SetProperty Wp_Property;

    public void PlayAnim(string nameAnim, bool loopAnimation)
    {

        Spine.Animation[] animation = skeletonGraphic.skeletonDataAsset.GetAnimationStateData().SkeletonData.Animations.ToArray();

        for (int i = 0; i < animation.Length; i++)
        {
            if (animation[i].Name.Equals(nameAnim))
            {
                skeletonGraphic.AnimationState.ClearTracks();
                skeletonGraphic.startingAnimation = animation[i].Name;
                skeletonGraphic.AnimationState.SetAnimation(i, animation[i].Name, loopAnimation);
                break;

            }
        }
    }



}

