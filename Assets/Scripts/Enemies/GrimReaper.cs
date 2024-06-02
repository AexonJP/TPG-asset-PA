using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrimReaper : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] private GameObject summon;
    [SerializeField] private GameObject changeScene;
    [SerializeField] private float cooldown;
    [SerializeField] private AudioSource hit;
    [SerializeField] private AudioSource lagu;
    private Transform player;
    private Transform grim;
    private Animator animator;
    public float AttackDistance = 1.5f;
    private bool facingRight = true;
    public LayerMask obstacleLayerMask;
    private bool isAttacking = false;
    // private float lamaskill = 0;

    [SerializeField] private GameObject deathVFX;
    [SerializeField] public int maxHealth = 10000;
    private int currentHealth;
    private PlayerController mvspeed;
    float lama=5;

    private float[] pindah = {-1.5f,1.5f};

    private Flash flashEffect;

    // Tambahkan referensi ke HealthBar
    public BossHealthBar healthBar;
    private bool hidup = true;

    public GameObject BossHealthBar3;

    // Array untuk menyimpan nama-nama trigger animasi serangan
    private string[] attackAnimations = { "Attack1", "Attack2" };

    void Start()
    {
        lagu.Play();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        grim = GameObject.FindGameObjectWithTag("grim").transform;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        animator.SetTrigger("");

        flashEffect = GetComponent<Flash>();
        obstacleLayerMask = LayerMask.GetMask("Water");
        mvspeed= player.gameObject.GetComponent<PlayerController>();

        BossHealthBar3.SetActive(true);
        
    }

    void Update()
    {
        // if ()
        // animator.GetCurrentAnimatorStateInfo();
    
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < AttackDistance && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
        else if (distanceToPlayer >= AttackDistance && isAttacking)
        {
            animator.SetTrigger("Idle");
            isAttacking = false;
        }
         else if (distanceToPlayer >= AttackDistance && !isAttacking)
        {
            animator.SetTrigger("Idle");
            // isAttacking = false;
        }
        if(distanceToPlayer >= AttackDistance){
            if(lama <=0){
                moveSpeed = 0f;
                
                mvspeed.moveSpeed=0;
                mvspeed.dashSpeed=0;
                // player.
        // Debug.Log(animator.GetCurrentAnimatorStateInfo(0));
                animator.SetTrigger("Hilang");
            }
            else{
                lama = lama-Time.deltaTime;
            }
        }
        else {
            lama =5;
            moveSpeed=2f;
            
        }
        if(cooldown <= 0){
            cooldown = 5f;
            animator.SetTrigger("Summon");
        }
        else{
            cooldown=cooldown-Time.deltaTime;
        }
        MoveTowardsPlayer();
    }

    void teleport(){
        transform.position = player.position-new Vector3(pindah[Random.Range(0,2)],0,0);
        mvspeed.moveSpeed=4f;
        mvspeed.dashSpeed=4f;
    }

    void panggil(){
        
        Instantiate(summon, grim.position+new Vector3(Random.Range(-2,2),Random.Range(-2,2),0), new Quaternion(0,0,0,1));
        Instantiate(summon, grim.position+new Vector3(Random.Range(-2,2),Random.Range(-2,2),0), new Quaternion(0,0,0,1));
        Instantiate(summon, grim.position+new Vector3(Random.Range(-2,2),Random.Range(-2,2),0), new Quaternion(0,0,0,1));
    }

    

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        
        // Pilih animasi serangan secara acak
        string randomAttack = attackAnimations[Random.Range(0, attackAnimations.Length)];
        animator.SetTrigger(randomAttack);
        
        float attackTime = 0.1f; // Sesuaikan durasi serangan sesuai kebutuhan
        float elapsedTime = 0f;

        while (elapsedTime < attackTime)
        {
            elapsedTime += Time.deltaTime;

            // Jika pemain menjauh selama serangan, kembali ke animasi "Idle"
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer >= AttackDistance)
            {
                animator.SetTrigger("Idle");
                isAttacking = false;
                yield break;
            }

            yield return null;
        }

        animator.SetTrigger("Idle");
        isAttacking = false;
        yield return null;
    }

    void MoveTowardsPlayer()
    {
        if(hidup == true){

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
    }

    // void Attack()
    // {
    //     animator.SetTrigger("Attack1");
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
        // Menonaktifkan komponen Rigidbody2D agar bos tidak bergerak saat die
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
        hidup =false;
        
        // animator.SetTrigger("Die");
        // Destroy(gameObject, 2f);
        StartCoroutine(ShowDeathVFX());
    }

    IEnumerator ShowDeathVFX()
    {
        // Tunggu beberapa detik untuk memastikan animasi selesai
        yield return new WaitForSeconds(0.1f); // Ubah angka sesuai dengan durasi animasi "Die"

        // Instansiasi efek kematian di posisi bos
        if (deathVFX != null)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
        }
        // ending.LoadScene("MainMenu");  
        BossHealthBar3.SetActive(false);
        changeScene.SetActive(true);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
            TakeDamage(arrow.damageArrow);

            StartCoroutine(flashEffect.FlashRoutine());
            
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("SwordCollider"))
        {
            DamageSource sword = collision.gameObject.GetComponent<DamageSource>();
            TakeDamage(sword.damageAmount);
            
            StartCoroutine(flashEffect.FlashRoutine());
        }
    }

    void TakeDamage(int damage)
    {
        hit.Play();
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // void MoveTowardsPlayer()
    // {
    //     if(hidup == true)
    //     {
    //         if (player != null)
    //         {
    //             Vector3 direction = (player.position - transform.position).normalized;
                
    //             RaycastHit2D hit = Physics2D.Raycast(grim.position,player.position-grim.position,2.6f,obstacleLayerMask);

    //             if (hit.collider != null)
    //             {
    //                 Debug.Log(transform.right);
    //                 // Hit obstacle, calculate new direction
    //                 // Vector3 newDirection = Vector3.Reflect(direction, hit.normal); 
    //                 // transform.position += moveSpeed * Time.deltaTime * newDirection;
    //                 Vector3 obstacleDirection = Quaternion.Euler(0, 0, 90) * direction; // Putar arah 90 derajat
    //                 transform.position += obstacleDirection * moveSpeed * Time.deltaTime;
    //             }
    //             else
    //             {
    //                 // No obstacle hit, move towards player
    //                 transform.position += moveSpeed * Time.deltaTime * direction;
    //             }
    //             Debug.DrawRay(grim.position,player.position-grim.position, Color.green);
    //             Debug.Log(grim.position);
    //         }

    //         if (transform.position.x < player.position.x && !facingRight)
    //         {
    //             Flip();
    //         }
    //         else if (transform.position.x > player.position.x && facingRight)
    //         {
    //             Flip();
    //         }
    //     }
    // }


}
