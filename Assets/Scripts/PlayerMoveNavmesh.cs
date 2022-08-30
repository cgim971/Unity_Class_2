using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoveNavmesh : MonoBehaviour
{
    CharacterController characterController = null;
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
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (Input.GetButtonDown("Jump") && flagOnGrouned)
        {
            calcVelocity.y += Mathf.Sqrt(jumpValue * -2f * Physics.gravity.y);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            float posDashEndX = Mathf.Log(1f / (Time.deltaTime * drags.x + 1)) / -Time.deltaTime;
            float posDashEndZ = Mathf.Log(1f / (Time.deltaTime * drags.z + 1)) / -Time.deltaTime;

            Vector3 dashVelocity = Vector3.Scale(
                transform.forward,
                dashValue * new Vector3(posDashEndX, 0, posDashEndZ)
                );

            calcVelocity += dashVelocity;
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            characterController.Move(agent.velocity * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        transform.position = agent.nextPosition;
    }
}
