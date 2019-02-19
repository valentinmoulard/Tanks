using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBag : MonoBehaviour
{

    public int m_nombreBoost;
    public int m_nombreBerserk;
    public int m_nombreMunition;
    public Vector3 m_lastCheckpointPos = new Vector3(7,0,-1.7f);
    public Quaternion m_lastCheckpointRotation = Quaternion.Euler(0, -90f, 0);

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boost"))
        {
            other.gameObject.SetActive(false);
            m_nombreBoost += 1;
        }

        if (other.gameObject.CompareTag("Ammo") && m_nombreMunition < 15)
        {
            other.gameObject.SetActive(false);
            m_nombreMunition += 3;
            if (m_nombreMunition > 15)
            {
                m_nombreMunition = 15;
            }
        }
        //je garde la position du checkpoint précédent pour le respawn
        if (other.gameObject.CompareTag("Checkpoints"))
        {
            m_lastCheckpointPos = other.transform.position;
            m_lastCheckpointRotation = other.transform.rotation;
        }

    }
}
