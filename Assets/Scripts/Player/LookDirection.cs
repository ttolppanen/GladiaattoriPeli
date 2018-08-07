using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirection : MonoBehaviour {

    GameObject player;

	void Start ()
    {
        player = GameObject.FindWithTag("Player");
	}
	
	
	void Update ()
    {
        Vector2 mousePosFromPlayer = Input.mousePosition - Camera.main.WorldToScreenPoint(player.transform.position);
        float angleToFaceMouse = Mathf.Atan2(mousePosFromPlayer.y, mousePosFromPlayer.x);
        Vector3 playerRot = player.transform.rotation.eulerAngles;
        playerRot.y = -angleToFaceMouse * Mathf.Rad2Deg + 90; //Meni väärään suuntaan, siitä miinus?
        player.transform.rotation = Quaternion.Euler(playerRot);
	}
}
