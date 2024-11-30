using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonador : MonoBehaviour
{
    public Animator anim;
    public Animator E;
    public bool over = false;
    public GameObject rock;
    public bool free = true;

    private void Update()
    {
        if (over)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                rock.GetComponent<Rock>().Explosion();
                anim.Play("Detonador_Pressed");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && free)
        {
            E.Play("E_Open");
            over = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && free)
        {
            E.Play("E_Close");
            over = false;
        }
    }
}
