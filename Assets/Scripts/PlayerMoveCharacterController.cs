using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoveCharacterController : MonoBehaviour
{
    CharacterController characterController = null;
    [SerializeField] float speed = 5f;
    [SerializeField] private float jumpValue = 2f;
    [SerializeField] private float dashValue = 5f;

    Vector3 directionValue = Vector3.zero;

    private float gravity = -9.81f;
    public Vector3 drags;
    private Vector3 calcVelocity = Vector3.zero;

    public LayerMask layerGround;
    private bool flagOnGrouned = true;
    private float defaultGroundDistance = 0.2f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //CheckGroundStatus();
        flagOnGrouned = characterController.isGrounded;

        if (flagOnGrouned && calcVelocity.y < 0)
        {
            calcVelocity.y = 0.0f;
        }

        /*
        directionValue = Vector3.zero;
        directionValue.x = Input.GetAxis("Horizontal");
        directionValue.z = Input.GetAxis("Vertical");
        */

        //directionValue = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                directionValue = new Vector3(hit.point.x, 0, hit.point.z);
            }
        }

        if (directionValue != Vector3.zero)
        {
            transform.forward = directionValue;
        }

        var _direction = directionValue - transform.position;

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

        calcVelocity.y += gravity * Time.deltaTime;

        calcVelocity.x /= 1 + drags.x * Time.deltaTime;
        calcVelocity.y /= 1 + drags.y * Time.deltaTime;
        calcVelocity.z /= 1 + drags.z * Time.deltaTime;

        //characterController.Move((directionValue + calcVelocity) * Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, directionValue) > 0.5f)
            characterController.Move(_direction.normalized * Time.deltaTime * speed);
    }

    private void CheckGroundStatus()
    {
        RaycastHit hitInfo;

        Vector3 posOrigin = transform.position + (Vector3.up * 0.1f);

        Debug.DrawLine(posOrigin, transform.position + (Vector3.up * 0.1f) + (Vector3.down * defaultGroundDistance));

        if (Physics.Raycast(posOrigin, Vector3.down, out hitInfo, defaultGroundDistance, layerGround))
        {
            flagOnGrouned = true;
        }
        else
        {
            flagOnGrouned = false;
        }
    }

}
