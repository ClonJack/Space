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
            alpha.a -= 0.01f;

            stateInfo.SpawnCountElements[id].color = alpha;
        }


        if (stateInfo.SpawnCountElements[id] != null)
            Destroy(stateInfo.SpawnCountElements[id].gameObject);





    }





    private void CreateTarget()
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


                img.transform.position = Input.mousePosition;


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
        ReturnStateWeaponGameObj();

    }


    private IEnumerator GenerateDamage(float time)
    {
        WaitForSeconds forSeconds = new WaitForSeconds(time);
        WaitForSeconds waitFor = new WaitForSeconds(0f);

        stateInfo.ShipInit.enemyShip.HpShip -= 25;
        stateInfo.ShipInit.enemyShip.sliderHpShip.value = stateInfo.ShipInit.enemyShip.HpShip;

        Color color = stateInfo.Damage.color;

        yield return forSeconds;
        while (stateInfo.Damage.color.a < 1)
        {
            yield return waitFor;
          
            color.a += 0.1f;

            stateInfo.Damage.color = color;
           



        }

          

    }


    private IEnumerator ShotWeaponAnimation()
    {
        WaitForSeconds forSeconds = new WaitForSeconds(1F);

        if (stateInfo.weapons.Count != 0)
        {
            for (int i = 0; i < stateInfo.shotWeaponGoodShip.Count; i++)
            {

                for (int j = 0; j < stateInfo.weapons.Count; j++)
                {

                    if (stateInfo.shotWeaponGoodShip[i].Wp_Property == stateInfo.weapons[j].Wp_Property)
                    {
                        stateInfo.wp_audioSource?.Play();
                        stateInfo.shotWeaponGoodShip[i].PlayAnim("fire animation demo", false);
                        yield return forSeconds;
                        stateInfo.shotWeaponGoodShip[i].PlayAnim("idle animation", true);
                        StartCoroutine(GenerateDamage(stateInfo.weapons[j].Time));


                    }


                }

            }

            stateInfo.weapons.Clear();
        }


        for (int i = 0; i < weaponsGoodShip.Length; i++)
        {
            Image img = weaponsGoodShip[i].GetComponent<Image>();
            img.raycastTarget = true;
        }


        stateInfo.IsAttackWeaopons = false;
    }




    private void DoDamge()
    {
        if (stateInfo.IsAttackWeaopons)
        {



            for (int i = 0; i < weaponsGoodShip.Length; i++)
            {


                if (weaponsGoodShip[i].IsСhanges)
                    stateInfo.CurrentImageElments[i] = weaponsGoodShip[i].Weapon_img;




                Image img = weaponsGoodShip[i].GetComponent<Image>();
                img.raycastTarget = false;


            }

            CreateTarget();



            for (int i = 0; i < stateInfo.CurrentImageElments.Length; i++)
            {
                if (stateInfo.SpawnCountElements[i] != null)
                    StartCoroutine(I_ActivateLaser(i));

            }


            StartCoroutine(ShotWeaponAnimation());


            Invoke("CallAnimations", 2f);






        }
    }

}
