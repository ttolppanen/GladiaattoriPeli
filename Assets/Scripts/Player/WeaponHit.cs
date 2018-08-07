using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour {

    public float hitForce;
    public float hitForceUp;
    public float limbForceMultiplier;
    public float attackStopTime;
    GameObject player;
    Animator anim;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)//Testi vaiheessa
    {
        if (other.tag == "Enemy")
        {
            EnemyController ec = other.GetComponent<EnemyController>();

            Vector3 forceDirection = (other.transform.position - player.transform.position).normalized;
            forceDirection = forceDirection.normalized * hitForce;
            forceDirection.y = hitForceUp;

            ec.Kill((BodyParts)Random.Range(0, (int)BodyParts.Null), forceDirection * limbForceMultiplier);
            Transform pelvis = other.transform.GetChild(0).GetChild(0);
            
            pelvis.GetComponent<Rigidbody>().AddForce(forceDirection, ForceMode.Impulse);
            StartCoroutine(FreezeAttackFrames());
        }
    }

    IEnumerator FreezeAttackFrames()
    {
        anim.SetFloat("Strike Speed", 0);
        yield return new WaitForSeconds(attackStopTime);
        anim.SetFloat("Strike Speed", 1);
    }
}
