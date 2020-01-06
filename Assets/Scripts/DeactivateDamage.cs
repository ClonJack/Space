using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateDamage : MonoBehaviour
{


    private void DeactDmg() => gameObject.SetActive(false);

    private void OnEnable() => Invoke("DeactDmg", 0.9f);


}
