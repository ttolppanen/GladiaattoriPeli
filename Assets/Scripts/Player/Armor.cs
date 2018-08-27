using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour {

    public Transform HelmetPlace;
    public Transform ChestPlace;
    int armorLvl;

    private void Start()
    {
        armorLvl = 0;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Head" && HelmetPlace.childCount == 0)
        {
            armorLvl++;
            coll.transform.parent = HelmetPlace;
            coll.transform.position = HelmetPlace.position;
        }
        else if (coll.tag == "Chest" && ChestPlace.childCount == 0)
        {
            armorLvl++;
            coll.transform.parent = ChestPlace;
            coll.transform.position = ChestPlace.position;
        }
    }

    public void TakeDamage()
    {
        if (armorLvl == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            armorLvl--;
            RemoveArmorPiece();
        }
    }

    void RemoveArmorPiece()
    {
        if (HelmetPlace.childCount != 0)
        {
            Destroy(HelmetPlace.GetChild(0));
        }
        else
        {
            Destroy(ChestPlace.GetChild(0));
        }
    }
}