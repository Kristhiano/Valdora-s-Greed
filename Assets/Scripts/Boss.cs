using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    public Transform[] swordP = new Transform[4];
    public GameObject sword;
    public Transform player;
    [SerializeField] private float timeAtack = 5;
    private float tAtack = 4;

    public float delayAC = 0.1f, reaction = 1f;

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
        rand = 1;
        if(rand == 0)
        {
            StartCoroutine(AtackCuadro());
        } else if(rand == 1)
        {
            StartCoroutine(AtackFollow());
        } else if(rand == 2)
        {
            Enemies();
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
        yield return new WaitForSeconds(reaction);
        for (int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(delayAC);
            for (int j = 0; j <= i; j++)
            {
                GameObject s = Instantiate(sword, start + new Vector3(i-j, j, 0), Quaternion.identity);
                StartCoroutine(DestroyS(s));
            }
        }
        for(int i = 12; i < 24; i++)
        {
            yield return new WaitForSeconds(delayAC);
            for (int j = i-12; j < 12; j++)
            {
                GameObject s = Instantiate(sword, start + new Vector3(i-j, j, 0), Quaternion.identity);
                StartCoroutine(DestroyS(s));
            }
        }
        yield return null;
    }

    IEnumerator AtackFollow()
    {
        int px = (int)player.position.x, py = (int)player.position.y;
        yield return new WaitForSeconds(reaction);
        for (int i = -3; i < 4; i++)
        {
            if(Mathf.Abs(i%2) == 1)
            {
                for (int j = (i+3)/2; j > (i-5)/2; j--)
                {
                    GameObject s = Instantiate(sword, new Vector3(px + i-j, py + j, 0), Quaternion.identity);
                    StartCoroutine(DestroyS(s));
                }
            } else
            {
                for (int j = (i+2)/2; j > (i-4)/2; j--)
                {
                    GameObject s = Instantiate(sword, new Vector3(px + i-j, py + j, 0), Quaternion.identity);
                    StartCoroutine(DestroyS(s));
                }
            }
        }
        yield return null;
    }

    void Enemies()
    {

    }

    IEnumerator DestroyS(GameObject s)
    {
        yield return new WaitForSeconds(1.8f);
        Debug.Log("Destroyed");
        Destroy(s);
    }

}
