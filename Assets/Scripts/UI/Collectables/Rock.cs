using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Animator anim;

    private int state = -1;
    private string[] s = new string[2];

    private void Start()
    {
        s[0] = "Rock_2";
        s[1] = "Rock_3";
    }

    public void Explosion()
    {
        state++;
        anim.Play(s[state]);
    }
}
