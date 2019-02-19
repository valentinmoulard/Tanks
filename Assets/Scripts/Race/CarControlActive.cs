using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControlActive : MonoBehaviour {

    public GameObject TankControl;
    public GameObject IAactive1;
    public GameObject Turrets;
	// Use this for initialization
	void Start () {
        TankControl.GetComponent<TankMovement>().enabled = true;
        IAactive1.GetComponent<NPCMove>().enabled = true;

        for (int i = 0; i < Turrets.transform.childCount; ++i)
        {
            Turrets.transform.GetChild(i).gameObject.GetComponent<TurretShooting>().enabled = true;
        }
    }

}
