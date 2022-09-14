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

    private void Start()
    {
        _chararcterController = GetComponent<CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _animator = GetComponent<Animator>();

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

        if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
        {
            _chararcterController.Move(_navMeshAgent.velocity * Time.deltaTime);
            // ¿Ãµø
        }
        else
        {
            _chararcterController.Move(Vector3.zero);
            // ∏ÿ√„
        }
    }

    private void LateUpdate()
    {
        transform.position = _navMeshAgent.nextPosition;
    }
}
