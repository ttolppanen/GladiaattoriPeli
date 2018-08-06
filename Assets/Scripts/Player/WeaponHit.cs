using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour {

    public float hitForce;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)//Testi vaiheessa
    {
        if (other.tag == "Enemy")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce((other.transform.position - player.transform.position).normalized * hitForce, ForceMode.Impulse);
            rb.constraints = RigidbodyConstraints.None;
            other.transform.parent = null;
            other.GetComponent<GoTowardPlayer>().enabled = false;
        }
    }
}
