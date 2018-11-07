using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    Animator animator;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }
        enemyAudio.Play();

        currentHealth -= amount;
        hitParticles.transform.position = hitPoint;

        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        animator.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;

        //
        Destroy(gameObject, 2f);
    }
    
    public class EnemyEntity
    {
        protected int health;
        protected int score;
        protected int damaged;
        protected int speed;
        protected AudioClip deathClip;
    }

    public class ZomBunnyEntity : EnemyEntity
    {

    }

    public class ZomBearEntity : EnemyEntity
    {

    }

    public class HellephantEntity : EnemyEntity
    {

    }
}
