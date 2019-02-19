using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    //layer mask, on choisi le layer qu'on veut affecter lors de l'explosion du projectil
    //on veut juste affecter les tanks qui recoit le projectil lors de l'explosion
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    //force générée par l'explosion (plus l'impact est proche, plus le tank sera repooussé)
    public float m_ExplosionForce = 1000f;            
    //temps de vie du projectil
    public float m_MaxLifeTime = 3f;
    //area of effect de l'explosion
    public float m_ExplosionRadius = 5f;


    private void Start()
    {
        m_ExplosionAudio.volume = PlayerPrefs.GetFloat("volume");
        //détruit l'objet si on est au dessu de 2 secondes
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
        //si le projectil est trigger avec un autre collider
        //récupere tous les colliders qu'on veut affecter
        //on crée une liste de colliders
        //OverlapSphere : Crée une sphere fictive et tout ce qui se passe dans la sphere est collecté et mis dans la liste de collider
        //on veut que cette sphere soit crée au meme endroit que l'explosion (transform.position) qui aura un rayon de (m_ExplosionRadius) et le mask du tank (m_TankMask)
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            //récupere le rigidbody de l'objet dans la liste de colliders
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
            //si on a pas réussi a récupérer un rigidbody
            if (!targetRigidbody)
                continue;

            //sinon

            //AddExplosionForce crée une force d'explosion et fera bouger les object qui ont un rigidbody
            //puissance (m_ExplosionForce); position (transform.position); area of effect (m_ExplosionRadius)
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            //on instancie une tank health et on récupere son component TankHealth
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
            //s'il n'y a pas de tankHealth quand on essaie de récupérer le component
            if (!targetHealth)
                continue;

            float damage = CalculateDamage(targetRigidbody.position);
            //applique les dégats au tank
            targetHealth.TakeDamage(damage);

        }

        //attention, lorsqu'on le projectil atteint sa cible, on veut le retirer de la scene
        //cependant, l'audio d'explosion et les effets de particul de l'explosion sont mis dans ShellExplosion qui est un enfant de Shell(le projectil qu'on veut retirer)
        //si on retire le projectil quand il touche un autre collider, le son et les effets de particul ne seront pas joués
        //on doit donc détacher ShellExplosion de son parent
        
        m_ExplosionParticles.transform.parent = null;
        m_ExplosionAudio.transform.parent = null;
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        //destroy after m_MaxLifeTime OU m_ExplosionParticles.duration
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        //on détruit le projectil
        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;
        //check how far the projectil is from the tank
        //plus il est proche, plus explosion distance sera faible
        float explotionDistance = explosionToTarget.magnitude;
        //pour avoir une magnétude entre 0 et 1
        float relativeDistance = (m_ExplosionRadius - explotionDistance) / m_ExplosionRadius;

        float damage = relativeDistance * m_MaxDamage;
        //vérifie si les dégats ne sont pas négatif
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}