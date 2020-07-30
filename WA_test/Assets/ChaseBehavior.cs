using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChaseBehavior : StateMachineBehaviour
{
    public float speed;
    public Transform pos;

    private Transform playerPos;
    private float dis;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 nextPlayerPos = new Vector3(playerPos.position.x, -0.93f, playerPos.position.z);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, nextPlayerPos, speed * Time.deltaTime);

        dis = Vector3.Distance(nextPlayerPos, animator.transform.position);
        if (dis >= 10.0f)
        {
            animator.SetBool("isChasing", false);
        }
        else if (dis <= 2.0f)
        {
            animator.SetBool("isAttack", true);
        }

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
