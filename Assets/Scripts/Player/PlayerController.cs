using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float acceleration;
    public float maxSpeed;
    Rigidbody rb;
    Animator anim;

	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!anim.GetBool("Strike"))
            {
                anim.SetTrigger("Strike");
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (!anim.GetBool("Heavy"))
            {
                anim.SetTrigger("Heavy");
            }
        }

        //anim.SetFloat("Running Speed", Mathf.Clamp01((Mathf.Pow(1 + rb.velocity.magnitude / maxSpeed, 2) - 1)));
    }

    void FixedUpdate ()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(Vector3.forward * acceleration, ForceMode.Acceleration);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(Vector3.back * acceleration, ForceMode.Acceleration);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector3.right * acceleration, ForceMode.Acceleration);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(Vector3.left * acceleration, ForceMode.Acceleration);
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
