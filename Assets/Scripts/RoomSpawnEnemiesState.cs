using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnEnemiesState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // TODO close all doors to this room

        // TODO check if this is the first time in the room, and depending on room type if we need to spawn enemies.
        uint remainingSpawnCycles = 0;
        uint roomType = 0;

        // Some rooms never spawn enemies (Shop & Special)
        if(roomType == 0)
        {
            animator.SetTrigger("FinishedRoom");
        }
        else
        {
            if(remainingSpawnCycles > 0)
            {
                // TODO spawn enemies
                animator.SetTrigger("SpawnedEnemies");
            }
            else
            {
                animator.SetTrigger("FinishedRoom");
            }
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
