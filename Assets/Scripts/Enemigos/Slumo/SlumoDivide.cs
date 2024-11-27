using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlumoDivide : MonoBehaviour
{
    public List<Vector2> forces = new List<Vector2>();
    public List<Transform> slumos = new List<Transform>();
    public GameObject slumo;

    public void Divide()
    {
        for(int i = 0; i < slumos.Count; i++)
        {
            GameObject s = Instantiate(slumo, slumos[i].position, new Quaternion(0, 0, 0, 0));
            s.GetComponent<Rigidbody2D>().AddForce(forces[i], ForceMode2D.Force);
        }
    }
}
