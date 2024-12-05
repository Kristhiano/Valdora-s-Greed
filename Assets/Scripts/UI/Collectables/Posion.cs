using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Posion : MonoBehaviour
{
    public Animator anim;
    public Animator E;
    public bool over = false;
    public GameObject player;

    public GameObject part;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(Time.timeScale == 0f)
        {
            part.SetActive(false);
        } else if(Time.timeScale == 1f)
        {
            part.SetActive(true);
        }
        if(over)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                anim.Play("Posion_Death");
                if(player.GetComponent<PlayerController>())
                {
                    player.GetComponent<PlayerController>().GetPosion();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            E.Play("E_Open");
            over = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            E.Play("E_Close");
            over = false;
        }
    }
}
