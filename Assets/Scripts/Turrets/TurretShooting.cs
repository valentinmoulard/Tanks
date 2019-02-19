using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour {

    //reference au rigidbody du prefab projectil
    public Rigidbody m_Shell;
    //reference de la position de tir
    public Transform m_FireTransform;

    //puissance du tir qui sera déterminé aléatoirement
    private float m_firePower;
    

    //reference a l'audio du tir
    public AudioSource m_ShootingAudio;
    //un audio pour tirer
    public AudioClip m_FireClip;


    void OnAwake()
    {
        m_ShootingAudio.volume = PlayerPrefs.GetFloat("volume");
    }

    // Use this for initialization
    void Start () {
        InvokeRepeating("Fire", 3, 3);
	}
	
	// Update is called once per frame
	void Update () {

    }


    private void Fire()
    {
        m_firePower = Random.Range(10, 75);
        //on instancie le projectil avec le model m_Shell a la position m_FireTransform.position avec une rotation m_FireTransform.rotation
        //attention Instanciate renvoie un gameObject à la base mais on a besoin d'un Rigidbody, donc on rajoute as Rigidbody
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        //tire dans la direction qui correspond (m_FireTransform.forward) avec la puissance actuelle du tir m_CurrentLaunchForce
        shellInstance.velocity = m_firePower * m_FireTransform.forward;

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }

}
