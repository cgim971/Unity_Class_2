using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    protected StateMachine<MonsterFSM> fsmManager;

    public StateMachine<MonsterFSM> FsmManager => fsmManager;

    private FieldOfView fov;

    public Transform target => fov.FirstTarget;

    public float atkRange;
    public bool getFlagAtk
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

    private void Start()
    {
        fov = GetComponent<FieldOfView>();

        fsmManager = new StateMachine<MonsterFSM>(this, new stateIdle());
        fsmManager.AddStateList(new stateMove());
        fsmManager.AddStateList(new stateAtk());
    }

    private void Update()
    {
        fsmManager.Update(Time.deltaTime);
        Debug.Log(fsmManager.getNowState);
    }

    public Transform SearchEnemy()
    {
        return target;
    }
}
