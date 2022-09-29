using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    protected StateMachine<MonsterFSM> fsmManager;
    public StateMachine<MonsterFSM> FsmManager => fsmManager;

    protected UnityEngine.AI.NavMeshAgent agent;
    protected Animator animator;

    private FieldOfView fov;

    public Transform target => fov?.FirstTarget;
    public Transform[] posTargets;
    public Transform posTarget = null;
    private int posTargetsIdx = 0;


    public float atkRange;
    public virtual bool getFlagAtk
    {
        get
        {
            if (!target)
            {
                return false;
            }

            float distance = Vector3.Distance(transform.position, target.position);

            return (distance <= atkRange);
        }
    }

    protected virtual void Start()
    {
        fov = GetComponent<FieldOfView>();

        fsmManager = new StateMachine<MonsterFSM>(this, new stateIdle());
        fsmManager.AddStateList(new stateMove());
        fsmManager.AddStateList(new stateAtk());

        //fsmManager = new StateMachine<MonsterFSM>(this, new StateRomming());
        //stateIdle stateIdle = new stateIdle();
        //stateIdle.isRomming = true;
        //fsmManager.AddStateList(stateIdle);
        //fsmManager.AddStateList(new stateMove());
        //fsmManager.AddStateList(new stateAtk());
    }

    protected virtual void Update()
    {
        fsmManager.Update(Time.deltaTime);
        Debug.Log(fsmManager.getNowState);
    }

    public virtual Transform SearchEnemy()
    {
        return target;
    }

    public Transform SearchNextTargetPosition()
    {
        return null;
    }

    //public Transform SearchNextTargetPosition()
    //{
    //    posTarget = null;
    //    if (posTargets.Length > 0)
    //    {
    //        posTarget = posTargets[posTargetsIdx];
    //    }

    //    posTargetsIdx = (posTargetsIdx + 1) % posTargets.Length;

    //    return posTarget;
    //}
}
