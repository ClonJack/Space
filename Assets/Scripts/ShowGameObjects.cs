using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGameObjects : MonoBehaviour
{

    [SerializeField] private List<GameObject> showGm;
    [SerializeField] private float second;


    private IEnumerator FirstShow(int id)
    {
        WaitForSeconds seconds = new WaitForSeconds(second);

        GameObject setGameobj = showGm[id];

        if (setGameobj.GetComponent<Image>() != null)
        {

            Image currentImg = setGameobj.GetComponent<Image>();
            Color color = currentImg.color;


            while (currentImg.color.a < 1)
            {
                yield return seconds;
                color.a += 0.01f;
                currentImg.color = color;
            }
        }
    }


    private IEnumerator ShowChild(Image child)
    {
        WaitForSeconds seconds = new WaitForSeconds(second);


        Color color = child.color;


        while (color.a < 1)
        {
            yield return seconds;
            color.a += 0.01f;
            child.color = color;
        }



    }


    private IEnumerator Show(int idParent, int IdChild)
    {
        WaitForSeconds waitFor = new WaitForSeconds(second);

        GameObject setGameobj = showGm[idParent].transform.GetChild(IdChild).gameObject;

    


        if (setGameobj.GetComponent<Image>() != null)
        {
            

            Image currentImg = setGameobj.GetComponent<Image>();
            Color color = currentImg.color;



            while (currentImg != null && currentImg.color.a < 1)
            {
                yield return waitFor;
                color.a += 0.01f;
                currentImg.color = color;
            }

        }
        else
        {
            if (setGameobj.transform.childCount > 0)
            {
                for(int i = 0; i < setGameobj.transform.childCount; i++)
                {

                    if (setGameobj.transform.GetChild(i).GetComponent<Image>() != null)
                    {
                        Image currentImgChild = setGameobj.transform.GetChild(i).GetComponent<Image>();

                        StartCoroutine(ShowChild(currentImgChild));
                    }
                }
            }
        }




    }


    public void ShowShowAlpha()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(second);

        for (int i = 0; i < showGm.Count; i++)
        {
            StartCoroutine(FirstShow(i));
            for (int j = 0; j < showGm[i].transform.childCount; j++)
                StartCoroutine(Show(i, j));
        }
    }




}
