using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObusExplosion : MonoBehaviour {

    public Rigidbody m_Shell;
    public float m_timeBeforeSeparation = 1f;
    private Transform m_separationPosition;
    public AudioSource m_SeparationAudio;
    public AudioClip m_SeparationClip;
    public int m_FragmentsNumber;
    public int m_RaduisDeploymentFragment;
    private float theta;
    //public AudioClip m_SeparationSound;
    // Use this for initialization
    void Start () {
        m_SeparationAudio.volume = PlayerPrefs.GetFloat("volume");
        theta = 2* Mathf.PI / m_FragmentsNumber;

	}
	
	// Update is called once per frame
	void Update () {
        m_timeBeforeSeparation -= Time.deltaTime;
        if (m_timeBeforeSeparation < 0)
        {
            
            m_SeparationAudio.clip = m_SeparationClip;
            m_SeparationAudio.Play();
            
            m_separationPosition = gameObject.GetComponent<Transform>();
            for (int j = 1; j <= m_RaduisDeploymentFragment; j+=2)
            {
                for (int i = 1; i <= m_FragmentsNumber; i++)
                {
                    Rigidbody shellInstance = Instantiate(m_Shell, m_separationPosition.position + j * new Vector3(0, Mathf.Sin(theta * i), Mathf.Cos(theta * i)), m_separationPosition.rotation) as Rigidbody;
                    shellInstance.velocity = gameObject.GetComponent<Rigidbody>().velocity;
                }
            }

            
            
            Destroy(gameObject);
        }
	}
}
