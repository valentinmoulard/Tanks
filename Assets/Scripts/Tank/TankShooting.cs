using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    //PROJECTILE
    //pour savoir qui shoot le projectil
    public int m_PlayerNumber = 1;      
    //reference au rigidbody du prefab projectil
    public Rigidbody m_Shell;            
    //reference de la position de tir
    public Transform m_FireTransform;
    //reference au slider
    public Slider m_AimSlider;
    //puissance de tir
    public float m_MinLaunchForce = 15f;
    public float m_MaxLaunchForce = 30f;
    //temps pour arriver au chargement max
    public float m_MaxChargeTime = 0.75f;
    //input button are strings
    //key event to shoot
    //private string m_FireButton;
    //how hard the fire is already charged
    private float m_CurrentLaunchForce;
    //speed of building the charge
    private float m_ChargeSpeed;
    //si on a deja tiré ou non
    private bool m_Fired;


    //AUDIO
    //reference a l'audio du tir
    public AudioSource m_ShootingAudio;
    //un audio pour charger le tir et un audio pour tirer
    public AudioClip m_ChargingClip;
    public AudioClip m_FireClip;
    

    //instance de TankBag pour récupérer les items dans l'inventaire
    TankBag TBAmmo;

    //délai avant de pouvoir tirer à nouveau
    private float m_compteurShoot = 0f;
    public float m_ShootTimer = 1.5f;


    private void OnEnable()
    {
        m_ShootingAudio.volume = PlayerPrefs.GetFloat("volume");
        //si on meurt, lors du respawn on remet les valeurs minimum
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        TBAmmo = gameObject.GetComponent<TankBag>();
        //pour savoir qui a tiré
        //ici m_Playernumber = 1 (désigne donc le joueur 1)
        //m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }

    //take care of the input
    private void Update()
    {
        if (TBAmmo.m_nombreMunition > 0 && m_compteurShoot <= 0)
        {
            m_AimSlider.value = m_MinLaunchForce;

            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
                //chargé au max et pas encore tiré
                m_CurrentLaunchForce = m_MaxLaunchForce;
                Fire();
            }
            //else if(Input.GetButtonDown(m_FireButton))
            else if (Input.GetMouseButtonDown(0))
            {
                //quand on presse le boutton pour commencer à charger
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;
                //audio de chargement
                
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();
            }
            //else if (Input.GetButton(m_FireButton) && !m_Fired)
            else if (Input.GetMouseButton(0) && !m_Fired)
            {
                //le boutton est maintenu enfoncé et pas encore tiré
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
                m_AimSlider.value = m_CurrentLaunchForce;
            }
            //else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
            else if (Input.GetMouseButtonUp(0) && !m_Fired)
            {
                //quand on relache le boutton
                Fire();
            }
        }
        m_compteurShoot -= Time.deltaTime;
        // Track the current state of the fire button and make decisions based on the current launch force.

    }


    private void Fire()
    {
        // Instantiate and launch the shell.
        m_Fired = true;
        //on instancie le projectil avec le model m_Shell a la position m_FireTransform.position avec une rotation m_FireTransform.rotation
        //attention Instanciate renvoie un gameObject à la base mais on a besoin d'un Rigidbody, donc on rajoute as Rigidbody
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        //tire dans la direction qui correspond (m_FireTransform.forward) avec la puissance actuelle du tir m_CurrentLaunchForce
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
        //juste une précaution
        m_CurrentLaunchForce = m_MinLaunchForce;
        TBAmmo.m_nombreMunition -= 1;
        m_compteurShoot = m_ShootTimer;

    }
}