﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordTracker : MonoBehaviour {
    
    void Awake () {
        DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
