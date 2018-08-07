using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {

    public Transform weaponPlace;

    public void ToggleWeaponTrail()
    {
        GameObject trail = weaponPlace.GetChild(0).Find("Trail").gameObject;
        trail.SetActive(!trail.activeSelf);
        if (!trail.activeSelf)
        {
            trail.GetComponent<TrailRenderer>().Clear();
        }
    }
    public void ToggleWeaponHitbox()
    {
        Collider hitbox = weaponPlace.GetChild(0).GetComponent<Collider>();
        hitbox.enabled = !hitbox.enabled;
    }
}
