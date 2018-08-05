using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {

    public Transform weaponPlace;

    public void ToggleWeaponTrail()
    {
        GameObject trail = weaponPlace.GetChild(0).Find("Trail").gameObject;
        if (trail.activeSelf)
        {
            trail.SetActive(false);
        }
        else
        {
            trail.SetActive(true);
        }
    }
}
