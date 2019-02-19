using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;    //vie de début      
    public Slider m_Slider;                  //reférence au slider pour pouvoir le modifier
    public Image m_FillImage;                //permet d'acceder a l'image pour en changer la couleur
    public Color m_FullHealthColor = Color.green;   //max vie : vert
    public Color m_ZeroHealthColor = Color.red;    //min vie : rouge
    public GameObject m_ExplosionPrefab;        //prefab de l'explosion
    
    private AudioSource m_ExplosionAudio;      
    private ParticleSystem m_ExplosionParticles;   
    private float m_CurrentHealth;      //vie courante du tank
    public bool m_Dead;            //alive or not
    //TankBag tankBag;

    void Start()
    {
        //tankBag = gameObject.GetComponent<TankBag>();
    }

    private void Awake()
    {
        //pour avoir la référence du particle system, on instancie le préfab de l'explosion
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        //pour que le tank n'explose pas en début de game
        m_ExplosionParticles.gameObject.SetActive(false);
    }

    //quand le tank est ON
    private void OnEnable()
    {
        //gameObject.transform.position = tankBag.m_lastCheckpoint;
        //etat comme en début de jeu
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        m_CurrentHealth -= amount;
        SetHealthUI();
        if(m_CurrentHealth <= 0.0f && !m_Dead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        m_Slider.value = m_CurrentHealth;
        //fait varier la couleur entre vert et rouge en fonction de la proportion de la vie du tank
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);

    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;
        //affiche les particules à l'endroit de l'explosion (endroit où le tank n'a plus de vie)
        m_ExplosionParticles.transform.position = transform.position;
        //active les particules
        m_ExplosionParticles.gameObject.SetActive(true);

        //on affiche les particule
        m_ExplosionParticles.Play();
        //on joue l'audio
        m_ExplosionAudio.Play();
        //le tank n'est plus actif
        gameObject.SetActive(false);
    }
    
}