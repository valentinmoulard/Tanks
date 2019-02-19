using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {

    public GameObject currentCheckpoint;
    public GameObject nextCheckpoint;

    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tank"))
        {
            currentCheckpoint.SetActive(false);
            nextCheckpoint.SetActive(true);
        }
    }
    
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2.5f);
    }
    

}
