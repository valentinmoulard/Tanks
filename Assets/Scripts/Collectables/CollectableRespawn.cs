using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableRespawn : MonoBehaviour {

    public GameObject collectible;

    public float m_respawnCollectablesTimer = 3f;
    private float m_timeLeftBeforeRespawn = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (collectible.activeSelf == false && m_timeLeftBeforeRespawn <= 0)
        {
            m_timeLeftBeforeRespawn = m_respawnCollectablesTimer;
        }
        if (collectible.activeSelf == false)
        {
            m_timeLeftBeforeRespawn -= Time.deltaTime;
            if(m_timeLeftBeforeRespawn <= 0)
            {
                collectible.SetActive(true);
                m_timeLeftBeforeRespawn = 0;
            }
        }
	}
}
