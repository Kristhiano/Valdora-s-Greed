using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlumoDivide : MonoBehaviour
{
    public List<Vector2> forces = new List<Vector2>();
    public List<Transform> slumos = new List<Transform>();
    public List<GameObject> divides = new List<GameObject>();
    public GameObject doorCollider;

    private List<GameObject> s = new List<GameObject>();

    public void Divide()
    {
        for(int i = 0; i < slumos.Count; i++)
        {
            GameObject inst = Instantiate(divides[i], slumos[i].position, new Quaternion(0, 0, 0, 0));
            inst.GetComponent<Rigidbody2D>().AddForce(forces[i], ForceMode2D.Force);
            s.Add(inst);
        }
    }

    public void Door()
    {
        if(doorCollider != null)
        {
            if(doorCollider.GetComponent<Door>())
            {
                for(int i = 0; i < s.Count; i++)
                {
                    doorCollider.GetComponent<Door>().enemies.Add(s[i]);
                    if(s[i].GetComponent<SlumoDivide>())
                    {
                        s[i].GetComponent<SlumoDivide>().doorCollider = doorCollider;
                    }
                }
            }
        }
    }
}
