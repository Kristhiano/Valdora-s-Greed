using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<GameObject> doors = new List<GameObject>();
    public List<ParticleSystem> sfx = new List<ParticleSystem>();
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> detonadores = new List<GameObject>();
    public CinemachineVirtualCamera virtualCam;

    private bool wined = true;

    public void Update()
    {
        if(Win() && wined)
        {
            virtualCam.Priority = 9;
            for (int i = 0; i < detonadores.Count; i++)
            {
                detonadores[i].GetComponent<Detonador>().free = true;
            }
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].GetComponent<Animator>().Play("Door Open");
            }
            wined = false;
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
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<SlumoMovement>())
            {
                enemies[i].GetComponent<SlumoMovement>().enabled = true;
            }
            if (enemies[i].GetComponent<SlumoShoot>())
            {
                enemies[i].GetComponent<SlumoShoot>().enabled = true;
            }
        }
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].GetComponent<Animator>().Play("Door Close");
            sfx[i].Play();
        }
        gameObject.GetComponent<Collider2D>().enabled = false;
    }


}
