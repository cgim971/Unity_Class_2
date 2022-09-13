using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class CubeMove_CharacterController_NavMesh : MonoBehaviour
{
    CharacterController characterController = null;
    [SerializeField] private float jumpValue = 2f;
    [SerializeField] private float dashValue = 5f;

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

        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }

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
