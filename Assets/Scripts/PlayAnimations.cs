using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayAnimations : MonoBehaviour
{

    [SerializeField] private Animator[] animators_ShowShip;
   


   

    public void ShowShip()
    {
        foreach (Animator animator in animators_ShowShip)
            animator.SetBool("Show", true);
    }


    public void BigShip()
    {
        foreach (Animator animator in animators_ShowShip)
        {
            animator.enabled = true;
            animator.SetBool("Ship_small", false);
            animator.SetBool("BigShip", true);
        }


    }


    public void SmallShip()
    {

        foreach (Animator animator in animators_ShowShip)
        {
        
            animator.SetBool("BigShip", false);
            animator.SetBool("Ship_small", true);
            
           
        }
    }


}
