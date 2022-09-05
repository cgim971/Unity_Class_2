using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class EnemyMoveNavMesh : MonoBehaviour
{
    CharacterController characterController = null;
    [SerializeField] private Transform player;
    // [SerializeField] float speed = 5f;
    [SerializeField] private float jumpValue = 2f;
    [SerializeField] private float dashValue = 5f;

    // Vector3 directionValue = Vector3.zero;

    // private float gravity = -9.81f;
    public Vector3 drags;
    private Vector3 calcVelocity = Vector3.zero;

    NavMeshAgent agent;

    public LayerMask layerGround;
    private bool flagOnGrouned = true;
    private float defaultGroundDistance = 0.2f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = true;
    }

    private void Update()
    {
        flagOnGrouned = characterController.isGrounded;

        if (flagOnGrouned && calcVelocity.y < 0)
        {
            calcVelocity.y = 0.0f;
        }

        agent.SetDestination(player.position);

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            characterController.Move(agent.velocity * Time.deltaTime);
        }
        else
        {
            characterController.Move(Vector3.zero);
        }
    }

    private void LateUpdate()
    {
        transform.position = agent.nextPosition;
    }
}
