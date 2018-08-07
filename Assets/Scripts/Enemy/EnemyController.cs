using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

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
            enemyRot.y = -angleToPlayer * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.Euler(enemyRot);
            boi.AddForce(directionToPlayer.normalized * acceleration);
        }
	}

    public void Kill(BodyParts bodyPart, Vector3 bodyPartThrustForce)
    {
        Destroy(GetComponent<Collider>());
        Destroy(boi);
        Destroy(GetComponent<Animator>());
        //Ragdoll päälle...
        foreach (Collider coll in GetComponentsInChildren<Collider>())
        {
            coll.enabled = true;
        }
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }
        //Irrota ruumiinosa
        if (bodyPart == BodyParts.Head)
        {
            Transform head = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0);
            SevereLimb(head);
            head.GetComponent<Rigidbody>().AddForce(bodyPartThrustForce, ForceMode.Impulse);
        }
        else if (bodyPart == BodyParts.RightArm)
        {
            Transform rightArm = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2);
            SevereLimb(rightArm);
            rightArm.GetComponent<Rigidbody>().AddForce(bodyPartThrustForce, ForceMode.Impulse);
        }
        else if (bodyPart == BodyParts.LeftArm)
        {
            Transform leftArm = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1);
            SevereLimb(leftArm);
            leftArm.GetComponent<Rigidbody>().AddForce(bodyPartThrustForce, ForceMode.Impulse);
        }
        else if (bodyPart == BodyParts.RightLeg)
        {
            Transform rightLeg = transform.GetChild(0).GetChild(0).GetChild(1);
            SevereLimb(rightLeg);
            rightLeg.GetComponent<Rigidbody>().AddForce(bodyPartThrustForce, ForceMode.Impulse);
        }
        else if (bodyPart == BodyParts.LeftLeg)
        {
            Transform leftLeg = transform.GetChild(0).GetChild(0).GetChild(0);
            SevereLimb(leftLeg);
            leftLeg.GetComponent<Rigidbody>().AddForce(bodyPartThrustForce, ForceMode.Impulse);
        }

        gameObject.tag = "Dead";
        transform.parent = null;
        Destroy(this);
    }

    void SevereLimb(Transform limb)
    {
        limb.parent = null;
        Destroy(limb.GetComponent<CharacterJoint>());
    }
}

public enum BodyParts {Head, RightArm, LeftArm, RightLeg, LeftLeg, Null};
