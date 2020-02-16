using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : StateMachineBehaviour
{
    [SerializeField]
    Vector2 targetLocation = Vector2.zero;

    void GenerateRandomTargetLocation(Vector2 pos)
    {
        Vector2 leftUpper = new Vector2(-10, 3);
        Vector2 rightLower = new Vector2(10, -3);
        Vector2 distance = rightLower - leftUpper;
        Vector2 random = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        targetLocation = leftUpper + random * distance;

        Debug.Log("New Target Position: " + targetLocation);

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
        float speed = 10.0f;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetLocation, speed * Time.fixedDeltaTime);

        Debug.Log("Old position: " + rb.position);

        rb.MovePosition(newPosition);

        Debug.Log("Walk to new position: " + newPosition);


        if (IsCloseToTarget(newPosition))
        {
            GenerateRandomTargetLocation(rb.position);
        }
    }

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GenerateRandomTargetLocation(animator.GetComponent<Rigidbody2D>().position);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        WalkToTargetLocation(animator.GetComponent<Rigidbody2D>());
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
