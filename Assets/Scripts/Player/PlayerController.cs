using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
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
        //anim.SetFloat("Running Speed", Mathf.Clamp01((Mathf.Pow(1 + rb.velocity.magnitude / maxSpeed, 2) - 1)));
    }

    void FixedUpdate ()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(Vector3.forward * speed);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(Vector3.back * speed);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector3.right * speed);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(Vector3.left * speed);
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
