using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class FlyingEye : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] private GameObject teleportTrigger;
    private Transform player;
    private Animator animator;
    public float AttackDistance = 1.5f;
    private bool facingRight = true;
    private bool isAttacking = false;
    [SerializeField] private AudioSource lagu;
    private bool hidup = true;

    [SerializeField] private GameObject deathVFX;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private AudioSource sauraMati;
    private int currentHealth;

    private Flash flashEffect;

    [SerializeField] private AudioSource hit;

    // Tambahkan referensi ke HealthBar
    public BossHealthBar healthBar;

    public GameObject BossHealthBar1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        lagu.Play();
        healthBar.SetMaxHealth(maxHealth);


        animator.SetTrigger("Fly");

        flashEffect = GetComponent<Flash>();

        BossHealthBar1.SetActive(true);
    }

    void Update()
    {
        if(hidup){
            MoveTowardsPlayer();
        
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < AttackDistance && !isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
            else if (distanceToPlayer >= AttackDistance && isAttacking)
            {
                animator.SetTrigger("Fly");
                isAttacking = false;
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("Fly");
        isAttacking = false;
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        if (transform.position.x < player.position.x && !facingRight)
        {
            Flip();
        }
        else if (transform.position.x > player.position.x && facingRight)
        {
            Flip();
        }
    }

    // void Attack()
    // {
    //     animator.SetTrigger("Attack");
    // }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    void Die()
    {   
        hidup = false;
        // Menonaktifkan komponen Rigidbody2D agar bos tidak bergerak saat die
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
        if(!sauraMati.isPlaying){
            sauraMati.Play();
        }

        animator.SetTrigger("Die");

        // Destroy(gameObject, 2f);
        teleportTrigger.SetActive(true);
        StartCoroutine(ShowDeathVFX());
    }

    IEnumerator ShowDeathVFX()
    {
        // Tunggu beberapa detik untuk memastikan animasi selesai
        yield return new WaitForSeconds(2f); // Ubah angka sesuai dengan durasi animasi "Die"

        // Instansiasi efek kematian di posisi bos
        if (deathVFX != null)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
        }

        BossHealthBar1.SetActive(false);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            hit.Play();
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
            TakeDamage(arrow.damageArrow);

            StartCoroutine(flashEffect.FlashRoutine());
            
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("SwordCollider"))
        {
            hit.Play();
            DamageSource sword = collision.gameObject.GetComponent<DamageSource>();
            TakeDamage(sword.damageAmount);
            
            StartCoroutine(flashEffect.FlashRoutine());
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
