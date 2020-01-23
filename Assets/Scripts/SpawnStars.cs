using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnStars : MonoBehaviour
{



    [SerializeField] private GameObject prefabSmallStar, prefabBigStar;
    [SerializeField] private Image canvas;
    [SerializeField] private int countGmSpawn;


    [SerializeField] private List<Image> SpawnStar;

    [SerializeField] private float time;

    [SerializeField] private float speed;
    [SerializeField] private int step;
    [SerializeField] private float percent;


    private IEnumerator I_MovementStartrs(int id)
    {
        WaitForSeconds seconds = new WaitForSeconds(time);

       
        Vector3 target = new Vector3(0, -1000f, 0);


        while (true)
        {

            yield return seconds;


            if (SpawnStar[id].transform.localPosition.y <= -1000f)
            {
                target.x = 0;
                SpawnStar[id].transform.localPosition = new Vector3(Random.Range(-500, 500), 1020f, 0);
            }


            if (target.x == 0)
                target.x = SpawnStar[id].transform.localPosition.x;

         

            SpawnStar[id].transform.localPosition = Vector3.MoveTowards(SpawnStar[id].transform.localPosition, target, Time.deltaTime * speed);

        }

    }

    private void Start()
    {
        SpawnStar = new List<Image>();

        int count = step;
        Vector3 randValue = Vector3.zero;

        percent = ((countGmSpawn / 100f) * 50f);

        for (int i = 0; i < countGmSpawn; i++)
        {
            if (percent < i)
                randValue = new Vector3(Random.Range(-500, 500), Random.Range(-100, -960));

            else
                randValue = new Vector3(Random.Range(-500, 500), Random.Range(0, 960));

            GameObject create = null;

            if (count >= step)
            {
                create = Instantiate(prefabBigStar, canvas.transform, false);
                count = 0;

            }
            else
            {
                create = Instantiate(prefabSmallStar, canvas.transform, false);
                count++;
            }

            SpawnStar.Add(create.GetComponent<Image>());

            create.transform.localPosition += randValue;


            StartCoroutine(I_MovementStartrs(i));


        }





    }




}
