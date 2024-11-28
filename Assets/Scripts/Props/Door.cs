using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<GameObject> doors = new List<GameObject>();
    public List<ParticleSystem> sfx = new List<ParticleSystem>();
    public List<GameObject> enemies = new List<GameObject>();
    public CinemachineVirtualCamera virtualCam;

    public void Update()
    {
        if(Win())
        {
            virtualCam.Priority = 9;
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].GetComponent<Animator>().Play("Door Open");
            }
            return;
        }
    }

    private bool Win()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                if (enemies[i].tag != "Untagged")
                {
                    return false;
                }
            }
            
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        virtualCam.Priority = 11;
        for(int i = 0; i < doors.Count; i++)
        {
            doors[i].GetComponent<Animator>().Play("Door Close");
            sfx[i].Play();
        }
        gameObject.GetComponent<Collider2D>().enabled = false;
    }


}
