using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBehaviourSetMove : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash)
        {
            animator.SetBool("Move", false);

            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Run", true);

            Debug.Log("run");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Run", false);

            Debug.Log("walk");
        }
    }
}
