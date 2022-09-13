using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyController : MonoBehaviour
{
    private Rigidbody rigidbody = null;
    public float spd = 5;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rigidbody?.AddForce(movement * spd);

        float jump = Input.GetAxis("Jump");
        Vector3 vecJump = new Vector3(0, jump, 0);
        rigidbody?.AddForce(vecJump * spd, ForceMode.Impulse);
    }
}
