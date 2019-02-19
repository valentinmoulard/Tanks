using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    private float timer = 4.0f;
    private float rotationTimer = 0.0f;
    private string rotation = "left";
	// Update is called once per frame
	void Update () {

        if (gameObject.tag == "Ammo" || gameObject.tag == "Boost")
        {
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }
        if (gameObject.tag == "OuterTurret" && rotationTimer < timer && rotation == "left")
        {
            rotationTimer += Time.deltaTime;
            transform.Rotate(new Vector3(0, 15, 0) * Time.deltaTime);
            if (rotationTimer > timer)
            {
                rotationTimer = 0.0f;
                rotation = "right";
            }
        }
        else if (gameObject.tag == "OuterTurret" && rotationTimer < timer && rotation == "right")
        {
            rotationTimer += Time.deltaTime;
            transform.Rotate(new Vector3(0,-15, 0) * Time.deltaTime);
            if (rotationTimer > timer)
            {
                rotationTimer = 0.0f;
                rotation = "left";
            }
        }
	}
}
