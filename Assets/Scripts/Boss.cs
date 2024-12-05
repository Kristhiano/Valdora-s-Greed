using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    public Transform[] swordP = new Transform[4];
    public Transform[] celdas = new Transform[4];
    public GameObject sword, celda;
    public Transform player;

    private List<int[]> atacks = new List<int[]>();
    private int enemN = 0;
    [SerializeField] private float timeAtack = 5;
    private float tAtack = 3;

    public float delayAC = 0.1f, reactionAF = 0.75f, reactionAC = 1.5f, reactionE = 2f;
    public float waitAF = 1, waitAC = 1, waitE = 2;

    private void Start()
    {
        atacks.Add(new int[1] { 0 });
        atacks.Add(new int[1] { 0 });
        atacks.Add(new int[1] { 0 });
        atacks.Add(new int[2] { 0, 0 });
        atacks.Add(new int[2] { 0, 0 });
        atacks.Add(new int[2] { 0, 1 });
        atacks.Add(new int[2] { 0, 1 });
        atacks.Add(new int[2] { 0, 1 });
        atacks.Add(new int[2] { 0, 1 });
        atacks.Add(new int[3] { 0, 0, 0 });
        atacks.Add(new int[3] { 0, 0, 0 });
        atacks.Add(new int[3] { 0, 1, 1 });
        atacks.Add(new int[3] { 0, 1, 1 });
        atacks.Add(new int[4] { 0, 1, 1, 4 });
        atacks.Add(new int[2] { 0, 2 });
        atacks.Add(new int[2] { 0, 2 });
        atacks.Add(new int[2] { 1, 2 });
        atacks.Add(new int[3] { 0, 0, 2 });
        atacks.Add(new int[3] { 0, 0, 2 });
        atacks.Add(new int[3] { 0, 0, 2 });
        atacks.Add(new int[4] { 1, 1, 1, 1 });
        atacks.Add(new int[3] { 0, 1, 2 });
        atacks.Add(new int[3] { 0, 1, 2 });
        atacks.Add(new int[3] { 0, 1, 2 });
        atacks.Add(new int[4] { 0, 2, 2, 4});
        atacks.Add(new int[2] { 0, 3 });
        atacks.Add(new int[4] { 0, 0, 0, 3 });
        atacks.Add(new int[3] { 0, 1, 3 });
        atacks.Add(new int[4] { 0, 0, 1, 3 });
        atacks.Add(new int[3] { 1, 1, 3 });
        atacks.Add(new int[3] { 2, 3, 4});
        atacks.Add(new int[4] { 0, 1, 2, 3 });
    }

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
        if(rand == 0)
        {
            StartCoroutine(AtackCuadro());
        } else if(rand == 1)
        {
            StartCoroutine(AtackFollow());
        } else if(rand == 2)
        {
            enemN++;
            StartCoroutine(Enemies());
        }
    }

    IEnumerator AtackCuadro()
    {
        tAtack = waitAC;
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
        yield return new WaitForSeconds(reactionAC);
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
        tAtack = waitAF;
        int px = (int)player.position.x, py = (int)player.position.y;
        yield return new WaitForSeconds(reactionAF);
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

    IEnumerator Enemies()
    {
        tAtack = waitE;
        yield return new WaitForSeconds(reactionE);
        if(enemN >= atacks.Count)
        {
            enemN = atacks.Count-1;
        }
        int[] l = atacks[enemN];
        int aux = Random.Range(0, 3);
        for(int i = 0; i < l.Length; i++)
        {
            GameObject s = Instantiate(celda, celdas[aux].position, Quaternion.identity);
            s.GetComponent<Celda>().i = l[i];
            StartCoroutine(DestroyS(s));
            aux++;
            if(aux == 4)
            {
                aux = 0;
            }
        }
    }

    IEnumerator DestroyS(GameObject s)
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(s);
    }

}
