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
    private float timeS = 1f;

    private void Update()
    {
        if(timeS == 1f && Time.timeScale == 0f)
        {
            part.SetActive(false);
        } else if(timeS == 0f && Time.timeScale == 1f)
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
            Debug.Log("Open");
            E.Play("E_Open");
            over = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Close");
            E.Play("E_Close");
            over = false;
        }
    }
}
