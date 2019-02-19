using UnityEngine;
using UnityEngine.UI;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;  //numéro du joueur
    public float m_Speed = 12f;     //vitesse du tank
    public float m_TurnSpeed = 180f;       //vitesse de rotation

    /* BOOST */
    public float m_boostCoef = 1.0f;        //coef d'acceleration pour boost
    public bool m_boosted = false;          //etat boost true or false
    public float m_boostTime = 2.0f;        //temps de boost quand devenu boosté
    private float m_compteurBoost = 0;      //temps restant de boost
    TankBag TBBoost;                        //instance de TankBag pour récupérer les items dans l'inventaire

    /* AUDIO */
    public AudioSource m_MovementAudio;    //audio
    public AudioClip m_EngineIdling;       //audio quand position stationnaire
    public AudioClip m_EngineDriving;      //audio quand déplacement
    public float m_PitchRange = 0.2f;       //portée du pitch
    public AudioSource m_klaxon;

    /* MESURE VITESSE */
    Vector3 lastPosition = Vector3.zero;
    public float countSpeed = 0.0f;

    /* DEPLACEMENT */
    private string m_MovementAxisName;     //horizontal ou vertical (1) pour le joueur 1, (2) pour le joueur 2 etc
    private string m_TurnAxisName;         //right ou left
    private Rigidbody m_Rigidbody;
    private MeshRenderer[] m_Color;         //contiendra le tableau des couleurs du tank
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;



    //pour établir des références entre des scripts ou initialisation de variables
    private void Awake()
    {
        //stock la référence du rigidbody
        m_Rigidbody = GetComponent<Rigidbody>();
        //recupération du tableau dans le mesh renderer
        m_Color = transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();

        //le son est trop fort si on le laisse à 1
        m_MovementAudio.volume = PlayerPrefs.GetFloat("volume") * 0.2f;
        m_klaxon.volume = PlayerPrefs.GetFloat("volume") * 0.2f;
    }


    private void OnEnable ()
    {
        //appelé apres awake()
        //kinematic : off. pour déplacer notre objet
        m_Rigidbody.isKinematic = false;
        //on met les valeurs suivantes à 0 car on ne veut pas récupérer les vitesses résiduels provenant des parties précedantes
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
        /*
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
        */
    }


    private void OnDisable ()
    {
        //kinematic : on. aucune force n'est appliqué à l'objet
        m_Rigidbody.isKinematic = true;
    }

    //aavant l'execution de la premiere frame
    private void Start()
    {
        TBBoost = gameObject.GetComponent<TankBag>();
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }

    //fonction qui récupere les inputs
    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        EngineAudio();
    }


    private void EngineAudio()
    {

        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
        //si le tank ne bouge pas (pas de déplacement ou de rotation)
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            //si le son joué est le mauvais, on le change en m_engine idling
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                //donne une valeur pour le pitch random dans l'intevalle correct
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                //joue le son en boucle
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip != m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineDriving;
                //donne une valeur pour le pitch random dans l'intevalle correct
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                //joue le son en boucle
                m_MovementAudio.Play();
            }
        }
    }


    //appelé a chaque frame avec un meme intervalle de temps
    //pour mettre a jour des déplacements/ timers/ interactions avec utilisateurs
    private void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
        Turn();
        ChangeColor();
    }


    private void ChangeColor()
    {
        //permettra de changer la couleur du tank qui varie entre rouge et bleu
        float lerp = Mathf.PingPong(Time.time, 1.0f) / 1.0f;
        m_Color[0].materials[0].color = Color.Lerp(Color.red, Color.blue, lerp);
        m_Color[1].materials[0].color = Color.Lerp(Color.cyan, Color.red, lerp);
        m_Color[2].materials[0].color = Color.Lerp(Color.green, Color.magenta, lerp);
        m_Color[3].materials[0].color = Color.Lerp(Color.yellow, Color.green, lerp);
    }

    private void Move()
    {
        Vector3 movement;
        m_boostCoef = 1.0f;
        if (Input.GetKey(KeyCode.V))
        {
            m_klaxon.Play();
        }

        if (Input.GetKey(KeyCode.Space) && !m_boosted)
        {
            if (TBBoost.m_nombreBoost > 0)
            {
                m_boosted = true;
                m_compteurBoost = m_boostTime;
                TBBoost.m_nombreBoost -= 1;
            }
        }

        // Modifie le coef de vitesse si boosté et que le temps de boost est supérieur a 0
        if (m_boosted)
        {
            if (m_compteurBoost > 0)
            {
                m_boostCoef = 2.0f;
                m_compteurBoost -= Time.fixedDeltaTime;
            }
            else
            {
                m_boosted = false;
            }
        }
        //calcul de la vitesse avec l'input, la vitesse et time.deltatime (smoothing deplacement per frame)
        movement = transform.forward * m_MovementInputValue * m_Speed * m_boostCoef * Time.fixedDeltaTime;
        //déplace le rigidbody
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

        //mesurer la vitesse
        countSpeed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        //quaternion store a rotation
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}