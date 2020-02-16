using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Flight_Fly : StateMachineBehaviour
{
    Rigidbody2D rb;
    Vector2 start;
    Vector2 end;

    float speed = 30;
    bool firstEnter = true;

    void ChooseStartAndEnd()
    {
        start = new Vector2(-20f, Random.Range(-3f, 3f));
        end = new Vector2(20f, Random.Range(-3f, 3f));
    }

    void TeleportToStart()
    {
        rb.transform.position = start;
        rb.position = rb.transform.position;
    }

    bool IsCloseToEnd(Vector2 pos)
    {
        float radius = 1;
        return radius > Vector2.Distance(end, pos);
    }

    void Fly()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, end, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (IsCloseToEnd(newPosition))
        {
            ChooseStartAndEnd();
            TeleportToStart();
        }
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(firstEnter)
        {
            ChooseStartAndEnd();
            firstEnter = false;
        }
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Fly();
        animator.GetComponent<BossLook>().LookInMoveDirection();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
