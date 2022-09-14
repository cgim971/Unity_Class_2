using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class Benefit_PlayerController : MonoBehaviour
{
    private CharacterController _chararcterController;
    private NavMeshAgent        _navMeshAgent;

    private Animator            _animator;

    private float               _defaultSpeed = 3.5f;
    private float               _runSpeed = 5f;

    private void Start()
    {
        _chararcterController = GetComponent<CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _animator = GetComponentInChildren<Animator>();

        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = true;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                _navMeshAgent.SetDestination(hit.point);
            }
        }

        _navMeshAgent.speed = _animator.GetBool("isRun") ? _runSpeed : _defaultSpeed;

        if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
        {
            _chararcterController.Move(_navMeshAgent.velocity * Time.deltaTime);

            // ¿Ãµø
            _animator.SetBool("isMove", true);
        }
        else
        {
            _chararcterController.Move(Vector3.zero);

            // ∏ÿ√„
            _animator.SetBool("isMove", false);
        }
    }

    private void LateUpdate()
    {
        transform.position = _navMeshAgent.nextPosition;
    }
}
