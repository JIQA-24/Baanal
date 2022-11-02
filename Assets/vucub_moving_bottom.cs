using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vucub_moving_bottom : StateMachineBehaviour
{
    Transform Pointer;
    public float speed = 0f;
    Rigidbody2D enemyBody;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Pointer = GameObject.FindGameObjectWithTag("Pointer").transform;
        enemyBody = animator.GetComponent<Rigidbody2D>();  
        animator.SetBool("Reposition_bottom",true);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(Pointer.position.y,enemyBody.position.x);
        Vector2 newPosition =  Vector2.MoveTowards(enemyBody.position,target,speed*Time.fixedDeltaTime);
        enemyBody.MovePosition(newPosition);    

        if(Vector2.Distance(Pointer.position,enemyBody.position) <= 1)
          animator.SetBool("Reposition_bottom",false);   
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
