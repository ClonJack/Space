using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;


public class AnimControll : MonoBehaviour
{

   [SerializeField] private SkeletonGraphic[] skeletonGraphic;
   [SerializeField] private AnimationReferenceAsset[] referenceAsset;



    public void Start()
    {


        skeletonGraphic = new SkeletonGraphic[GameObject.FindGameObjectsWithTag("Ship").Length];

        for (int i = 0; i < skeletonGraphic.Length; i++)
            skeletonGraphic[i] = GameObject.FindGameObjectsWithTag("Ship")[i].GetComponent<SkeletonGraphic>();

        ActivaAnim();

    }

    private void ActivaAnim()
    {

        

        referenceAsset = Resources.LoadAll<AnimationReferenceAsset>("ReferenceAssets");


         for (int i = 0; i < skeletonGraphic.Length; i++)
         {
             for (int j = 0; j < referenceAsset.Length; j++)
             {
                 skeletonGraphic[i].AnimationState.SetAnimation(j, referenceAsset[j].name, true);

             }
         }
    }

}
