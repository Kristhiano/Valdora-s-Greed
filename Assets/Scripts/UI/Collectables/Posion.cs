using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posion : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("B");
        if(collision.CompareTag("Player"))
        {
            Debug.Log("A");
            anim.Play("Posion_Death");
        }
    }
}
