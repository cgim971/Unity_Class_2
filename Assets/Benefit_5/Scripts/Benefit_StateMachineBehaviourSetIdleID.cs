using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Benefit_StateMachineBehaviourSetIdleID : StateMachineBehaviour
{
    private int _idle_IDCount = 2;
    public float _minIdleIDTime = 0f;
    public float _maxIdleIDTime = 2f;
    [SerializeField] protected float _rndBaseIdleTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rndBaseIdleTime = Random.Range(_minIdleIDTime, _maxIdleIDTime);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash)
        {
            animator.SetInteger("Idle_ID", -1);
        }

        if (stateInfo.normalizedTime > _rndBaseIdleTime && !animator.IsInTransition(0))
        {
            animator.SetInteger("Idle_ID", Random.Range(0, _idle_IDCount + 1));
        }
    }
}
