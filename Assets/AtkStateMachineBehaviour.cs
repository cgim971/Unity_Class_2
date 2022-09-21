using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkStateMachineBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<MonsterFSM>()?.FsmManager.ChangeState<stateIdle>();
    }
}
