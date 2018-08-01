using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;
    Vector3 difference;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        difference = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + difference;
	}
}
