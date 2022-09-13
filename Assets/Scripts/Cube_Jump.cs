using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube_Jump : MonoBehaviour
{

    private Rigidbody rigidbody = null;
    public float spd = 5;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float jump = Input.GetAxis("Jump");
        Vector3 vecJump = new Vector3(0, jump, 0);

        rigidbody?.AddForce(vecJump * spd, ForceMode.Impulse);
    }

}
