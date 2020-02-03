using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Spine.Unity;

public interface IDamage

{

    void DamageForShip();
    void WP_imgAcivateRayCast(bool isActivateRaycastTarget);
    void CreateTarget(Vector3 PlaceSpawnElement);
    void ReturnStateAllWeaponGameObj();


}


public class PropertyWeaponInfo : MonoBehaviour, IDamage
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


            weaponsGoodShip[i].IdWp = i;
            weaponsGoodShip[i].Weapon_img = ship.goodShip.MechanismsShip.GetChild(i).GetComponent<Image>();


        }

        /*--------------------------------------------------------------------------------*/

        mechanismsBadShip = new Mechanism[ship.enemyShip.CountMechanisms];

        for (int i = 0; i < mechanismsBadShip.Length; i++)
        {
            mechanismsBadShip[i] = ship.enemyShip.MechanismsShip.GetChild(i).GetComponent<Mechanism>();

            mechanismsBadShip[i].eventGetDamage = new UnityEvent();
            //  mechanismsBadShip[i].eventGetDamage.AddListener(DamageForEnemyShip);
            mechanismsBadShip[i].idMechanism = i;
            mechanismsBadShip[i].Hp = 100;
            mechanismsBadShip[i].Ship = ship;
            mechanismsBadShip[i].Mechanism_img = ship.enemyShip.MechanismsShip.GetChild(i).GetComponent<Image>();


        }







    }






    public void ReturnStateAllWeaponGameObj()
    {

        for (int i = 0; i < weaponsGoodShip.Length; i++)
        {
            int Length = weaponsGoodShip[i].Weapon_img.sprite.name.Length;

            if (weaponsGoodShip[i].IsСhanges)
            {


                string newstr = weaponsGoodShip[i].Weapon_img.sprite.name.Replace(" selected", "");

                weaponsGoodShip[i].Weapon_img.sprite = Resources.Load<Sprite>("SimpleWeapons/" + newstr);

                weaponsGoodShip[i].IsСhanges = false;
            }

            stateInfo.CurrentImageElementsPositons[i] = Vector2.zero;
            stateInfo.CurrentImageElments[i] = null;


        }

    }




    private IEnumerator I_ActivateLaser(int id)
    {
        WaitForSeconds wait = new WaitForSeconds(0.03f);

        Color alpha = stateInfo.SpawnCountElements[id].color;

        while (alpha.a > 0)
        {
            yield return wait;
            alpha.a -= 0.1f;

            stateInfo.SpawnCountElements[id].color = alpha;
        }


        if (stateInfo.SpawnCountElements[id] != null)
            Destroy(stateInfo.SpawnCountElements[id].gameObject);





    }





    public void CreateTarget(Vector3 PlaceSpawnElement)
    {

        Vector3 savePosRand = Vector3.zero;
        Vector3 generateRandomPostion = Vector3.zero;
        int idCreate = 0;



        for (int i = 0; i < stateInfo.CurrentImageElments.Length; i++)
        {


            if (stateInfo.CurrentImageElments[i] != null)
            {
                GameObject gm = new GameObject("Clone");

                Image img = gm.AddComponent<Image>();


                string path = stateInfo.CurrentImageElments[i].sprite.name.Replace(" selected", " Target");



                img.sprite = Resources.Load<Sprite>("TargetWeapons/" + path);

                stateInfo.SpawnCountElements[i] = img;

                img.transform.SetParent(stateInfo.canvas, false);


                img.transform.position = PlaceSpawnElement;


                if (idCreate > 0)
                {

                    generateRandomPostion = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100));



                    while (generateRandomPostion.x == savePosRand.x && generateRandomPostion.y == savePosRand.y)
                    {
                        generateRandomPostion = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100));
                    }



                    savePosRand = generateRandomPostion;

                    stateInfo.CurrentImageElementsPositons[i] = generateRandomPostion;


                    img.transform.localPosition += generateRandomPostion;
                }


                idCreate++;


            }

        }





    }

    private void CallAnimations()
    {
        stateInfo.playAnimations.SmallShip();

        ReturnStateAllWeaponGameObj();

    }


    private IEnumerator GenerateDamage(float time,Ship ship,Image dmg)
    {
        WaitForSeconds forSeconds = new WaitForSeconds(time);
        WaitForSeconds waitFor = new WaitForSeconds(0f);

      
   
        yield return forSeconds;

        ship.HpShip -= 25;
        ship.sliderHpShip.value = ship.HpShip;

        Color color = dmg.color;




        while (color.a < 1f)
        {
            yield return waitFor;

            color.a += 0.1f;

            if (dmg.color.a <= 1f)
                dmg.color = color;





        }




    }

    private IEnumerator Shot(float time, float timeAfterShot, List<ShotWeapon> shotWeapons, int id)
    {
        WaitForSeconds wait = new WaitForSeconds(time);
        WaitForSeconds seconds = new WaitForSeconds(timeAfterShot);

        yield return wait;
        shotWeapons[id].PlayAnim("fire animation demo", false);
        stateInfo.wp_audioSource?.Play();
        yield return seconds;
        shotWeapons[id].PlayAnim("idle animation", true);

    }


    private IEnumerator ShotWeaponAnimation()
    {
        WaitForSeconds forSeconds = new WaitForSeconds(1.2F);

        if (stateInfo.weapons.Count != 0)
        {
            for (int i = 0; i < stateInfo.shotWeaponGoodShip.Count; i++)
            {

                for (int j = 0; j < stateInfo.weapons.Count; j++)
                {

                    if (stateInfo.shotWeaponGoodShip[i].Wp_Property == stateInfo.weapons[j].Wp_Property)
                    {

                        StartCoroutine(Shot(0.1f, 1f, stateInfo.shotWeaponGoodShip, i));
                        StartCoroutine(GenerateDamage(stateInfo.weapons[j].Time, stateInfo.ShipInit.enemyShip,stateInfo.DamageForEnemy));

                        yield return forSeconds;

                        int id = stateInfo.weapons[j].IdWp;

                        if (stateInfo.SpawnCountElements[id] != null)
                            StartCoroutine(I_ActivateLaser(id));



                      



                    }
                }


            }

            stateInfo.weapons.Clear();
        }


    

        stateInfo.IsAttackWeaopons = false;


        StartCoroutine(DamageForGoodShip());




    }






    public void WP_imgAcivateRayCast(bool isActivateRaycastTarget)
    {

        for (int i = 0; i < weaponsGoodShip.Length; i++)
        {


            if (weaponsGoodShip[i].IsСhanges)
                stateInfo.CurrentImageElments[i] = weaponsGoodShip[i].Weapon_img;




            Image img = weaponsGoodShip[i].GetComponent<Image>();
            img.raycastTarget = isActivateRaycastTarget;


        }
    }


    public IEnumerator DamageForGoodShip()
    {
        WaitForSeconds seconds = new WaitForSeconds(1f);
        WaitForSeconds secondsEnd = new WaitForSeconds(2f);
        int timeDamage = Random.Range(3,5);

        for (int i = 0; i < stateInfo.shotWeaponsBadShip.Count; i++)
        {
            int rand = Random.Range(0, stateInfo.shotWeaponsBadShip.Count);

            StartCoroutine(Shot(1.5f, 2, stateInfo.shotWeaponsBadShip, rand));

       
            StartCoroutine(GenerateDamage(timeDamage, stateInfo.ShipInit.goodShip,stateInfo.DamageForGoodShip));

            yield return seconds;
        }

        yield return secondsEnd;
        WP_imgAcivateRayCast(true);
    }



    public void DamageForShip()
    {
        if (stateInfo.IsAttackWeaopons)
        {
            WP_imgAcivateRayCast(false);

            CreateTarget(Input.mousePosition);

            StartCoroutine(ShotWeaponAnimation());




            Invoke("CallAnimations", 2f);

        }
    }
}

