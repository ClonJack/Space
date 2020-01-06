using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimations : MonoBehaviour
{

    [SerializeField] private Animator[] animators_ShowShip;



    public void ShowShip()
    {
        foreach (Animator animator in animators_ShowShip)
            animator.SetBool("Show", true);

    

    }



}
