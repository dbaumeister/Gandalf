using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Flight_Enter_Stage_TakeOff : StateMachineBehaviour
{
    float speed = 50;
    float endTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        endTime = Time.time + 0.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Just move upwards for a few seconds
        Rigidbody2D rb = animator.GetComponent<Rigidbody2D>();

        Vector2 newPosition = Vector2.MoveTowards(rb.position, rb.position + Vector2.up * 100, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if(Time.time > endTime)
        {
            animator.SetTrigger("Boss_Flight_Fly");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
