using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTowardPlayer : MonoBehaviour {

    public float acceleration;

    GameObject player;
    Rigidbody boi;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boi = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        if(player != null)
        {
            float angleToPlayer = Mathf.Atan2(directionToPlayer.z, directionToPlayer.x);
            Vector3 enemyRot = transform.rotation.eulerAngles;
            enemyRot.y = -angleToPlayer * Mathf.Rad2Deg+90;
            transform.rotation = Quaternion.Euler(enemyRot);
            boi.AddForce(directionToPlayer.normalized * acceleration);
        }
	}
}
