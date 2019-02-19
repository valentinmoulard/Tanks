using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRespawn : MonoBehaviour {


    private float timer = 0;
    private int respawnTime = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.GetChild(0).gameObject.activeSelf == false)
        {
            timer += Time.deltaTime;
            if (timer > respawnTime)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.transform.position = gameObject.GetComponentInChildren<TankBag>().m_lastCheckpointPos;
                transform.GetChild(0).gameObject.transform.rotation = gameObject.GetComponentInChildren<TankBag>().m_lastCheckpointRotation;
                timer = 0;
            }
        }
	}




}
