using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celda : MonoBehaviour
{
    public GameObject[] pre = new GameObject[4];
    public int i;

    void Spawn()
    {
        Instantiate(pre[i], transform.position, Quaternion.identity);
    }
}
