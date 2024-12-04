using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Idle : StateMachineBehaviour
{
    int rand;
    public float timer = 2;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rand  = Random.Range(0, 3);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer <= 0)
        {
            Atack(animator);
        } else
        {
            timer -= Time.deltaTime;
        }
    }

    private void Atack(Animator anim)
    {
        Debug.Log(rand);
        if(rand == 0)
        {
            anim.SetTrigger("AttackFA");
        } else if(rand == 1)
        {
            anim.SetTrigger("AttackAA");
        } else if(rand == 2)
        {
            anim.SetTrigger("AttackFM");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
