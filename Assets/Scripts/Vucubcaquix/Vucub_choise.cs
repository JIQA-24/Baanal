using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vucub_choise : StateMachineBehaviour
{
    int num;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       /* float num = Random.Range(0, 6);
        
        if (num == 3)
        {
            animator.SetBool("Reposition_left",true);
        }
        else if (num == 4)
        {
            animator.SetBool("Reposition_right",true);
        }
        else if (num == 5)
        {
            animator.SetBool("Reposition_bottom",true);
        }
         */   
        animator.SetBool("Reposition_left",true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
