using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateRomming : State<MonsterFSM>
{
    private Animator animator;
    private CharacterController controller;
    private NavMeshAgent agent;

    private MonsterFSM monsterFSM;

    private int hashMove = Animator.StringToHash("Move");
    private int hashMoveSpd = Animator.StringToHash("MoveSpd");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();
        controller = stateMachineClass.GetComponent<CharacterController>();
        agent = stateMachineClass.GetComponent<NavMeshAgent>();

        monsterFSM = stateMachineClass as MonsterFSM;
    }
    public override void OnStart()
    {
        Transform posTarget = null;

        if (stateMachineClass.posTarget == null)
        {
            posTarget = stateMachineClass.SearchNextTargetPosition();
        }

        if (posTarget)
        {
            agent?.SetDestination(posTarget.position);
            animator?.SetBool(hashMove, true);
        }

        agent.stoppingDistance = 0.0f;

        if (monsterFSM?.posTarget == null)
        {
            monsterFSM?.SearchNextTargetPosition();
        }

        if (monsterFSM?.posTarget != null)
        {
            Vector3 destination = monsterFSM.posTarget.position;
            agent?.SetDestination(destination);
            animator?.SetBool(hashMove, true);
        }
    }
    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();
        if (target)
        {
            if (stateMachineClass.getFlagAtk)
            {
                stateMachine.ChangeState<stateAtk>();
            }
            else
            {
                stateMachine.ChangeState<stateMove>();
            }
        }
        else
        {
            if (!agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance))
            {
                SearchNextTargetPosition();
                stateMachine.ChangeState<stateIdle>();
            }
            else
            {
                controller.Move(agent.velocity * Time.deltaTime);

                animator.SetFloat(hashMoveSpd, agent.velocity.magnitude / agent.speed, 1f, Time.deltaTime);
            }
        }
    }
    public override void OnEnd()
    {
        agent.stoppingDistance = stateMachineClass.atkRange;
        animator?.SetBool(hashMove, false);
        agent.ResetPath();
    }
    private void SearchNextTargetPosition()
    {
        Transform posTarget = monsterFSM.SearchNextTargetPosition();
        if (posTarget != null)
        {
            agent?.SetDestination(posTarget.position);
        }
    }

}
