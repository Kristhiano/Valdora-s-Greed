using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    public Transform[] swordP = new Transform[4];
    public GameObject sword;
    public Transform player;
    [SerializeField] private float timeAtack = 5;
    private float tAtack = 5;

    public float delayAC = 0.1f;

    void Update()
    {
        if(tAtack <= 0)
        {
            Atack();
            tAtack = timeAtack;
        } else
        {
            tAtack -= Time.deltaTime;
        }
    }

    void Atack()
    {
        int rand = Random.Range(0, 3);
        rand = 0;
        if(rand == 0)
        {
            StartCoroutine(AtackCuadro());
        } else if(rand == 1)
        {
            AtackFollow();
        } else if(rand == 2)
        {
            AtackCruz();
        }
    }

    IEnumerator AtackCuadro()
    {
        int rand;
        if(player.position.x >= transform.position.x)
        {
            if(player.position.y >= transform.position.y)
            {
                rand = 2;
            } else
            {
                rand = 1;
            }
        } else
        {
            if (player.position.y >= transform.position.y)
            {
                rand = 3;
            }
            else
            {
                rand = 0;
            }
        }
        Vector3 start = swordP[rand].position;
        Debug.Log("A");
        for(int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(delayAC);
            for (int j = 0; j <= i; j++)
            {
                Debug.Log(i + " " + j);
                GameObject s = Instantiate(sword, start + new Vector3(i-j, j, 0), Quaternion.identity);
                StartCoroutine(Destroy(s));
            }
        }
        for(int i = 12; i < 24; i++)
        {
            yield return new WaitForSeconds(delayAC);
            for (int j = i-12; j < 12; j++)
            {
                GameObject s = Instantiate(sword, start + new Vector3(i-j, j, 0), Quaternion.identity);
                StartCoroutine(Destroy(s));
            }
        }
        yield return null;
    }

    void AtackFollow()
    {

    }

    void AtackCruz()
    {

    }

    IEnumerator Destroy(GameObject s)
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(s);
    }

}
