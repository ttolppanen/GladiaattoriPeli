using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {

    public Transform weaponPlace;
    public float hitLayerWeight = 0;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetLayerWeight(1, hitLayerWeight);
    }

    public void ToggleWeaponTrail()
    {
        GameObject trail = weaponPlace.GetChild(0).Find("Trail").gameObject;
        trail.SetActive(!trail.activeSelf);
        if (!trail.activeSelf)
        {
            trail.GetComponent<TrailRenderer>().Clear();
        }
    }
    public void ToggleWeaponHitbox(int state) //0 on false, 1 true
    {
        Collider hitbox = weaponPlace.GetChild(0).GetComponent<Collider>();
        hitbox.enabled = System.Convert.ToBoolean(state);
    }
}
