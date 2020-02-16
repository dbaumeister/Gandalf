using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : StateMachineBehaviour
{
    [SerializeField]
    Vector2 targetLocation = Vector2.zero;

    float newAttackTime = 0;
    float speed = 0f;

    void GenerateRandomTargetLocation(Vector2 pos)
    {
        Vector2 leftUpper = new Vector2(-10, 3);
        Vector2 rightLower = new Vector2(10, -3);
        Vector2 distance = rightLower - leftUpper;
        Vector2 random = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        targetLocation = leftUpper + random * distance;

        if(Vector2.Distance(targetLocation, pos) < 3)
        {
            GenerateRandomTargetLocation(pos);
        }
    }

    bool IsCloseToTarget(Vector2 pos)
    {
        float radius = 1;
        return radius > Vector2.Distance(targetLocation, pos);
    }

    void WalkToTargetLocation(Rigidbody2D rb)
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetLocation, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (IsCloseToTarget(newPosition))
        {
            GenerateRandomTargetLocation(rb.position);
        }
    }

    void RollNewAttackTime()
    {
        newAttackTime = Time.time + Random.Range(3f, 7f);
    }

    bool CanAttack()
    {
        return newAttackTime < Time.time;
    }

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speed = 10.0f;
        GenerateRandomTargetLocation(animator.GetComponent<Rigidbody2D>().position);
        RollNewAttackTime();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        WalkToTargetLocation(animator.GetComponent<Rigidbody2D>());
        animator.GetComponent<BossLook>().LookInMoveDirection();

        if(CanAttack())
        {
            // Force to look to the left
            targetLocation = animator.GetComponent<Rigidbody2D>().position + 20 * Vector2.left;
            animator.GetComponent<BossLook>().LookInMoveDirection();
            speed = 0.1f;

            animator.SetTrigger("Stage_1_Attack");
            RollNewAttackTime();
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Stage_1_Attack");
    }
}
