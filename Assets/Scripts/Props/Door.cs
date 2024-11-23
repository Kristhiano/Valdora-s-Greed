using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<GameObject> doors = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();

    public void Update()
    {
        if(Win())
        {
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].GetComponent<Animator>().Play("DoorOpen");
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
        for(int i = 0; i < doors.Count; i++)
        {
            doors[i].GetComponent<Animator>().Play("DoorClose");
        }
    }


}
