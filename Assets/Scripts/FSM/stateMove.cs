using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateMove : State<MonsterFSM>
{
    private Animator animator;
    private CharacterController characterController;
    private NavMeshAgent navMeshAgent;

    protected int hashMove = Animator.StringToHash("Move");
    protected int hashMoveSpd = Animator.StringToHash("MoveSpd");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();
        characterController = stateMachineClass.GetComponent<CharacterController>();

        navMeshAgent = stateMachineClass.GetComponent<NavMeshAgent>();
    }

    public override void OnStart()
    {
        navMeshAgent?.SetDestination(stateMachineClass.target.position);
        animator?.SetBool(hashMove, true);
    }

    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();

        if (target)
        {
            navMeshAgent.SetDestination(stateMachineClass.target.position);

            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                characterController.Move(navMeshAgent.velocity * Time.deltaTime);
                animator.SetFloat(hashMoveSpd, navMeshAgent.velocity.magnitude / navMeshAgent.speed, 0.1f, Time.deltaTime);
                return;
            }
        }

        stateMachine.ChangeState<stateIdle>();
    }

    public override void OnEnd()
    {
        animator?.SetBool(hashMove, false);
        animator?.SetFloat(hashMoveSpd, 0);

        navMeshAgent.ResetPath();
    }
}
