using System.Collections;
using UnityEngine;

public class Minotaur : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] private GameObject teleportTrigger;
    private Transform player;
    private Transform minotaur;
    private Animator animator;
    public float AttackDistance = 1.5f;
    private bool facingRight = true;
    public LayerMask obstacleLayerMask;
    private bool isAttacking = false;
    public AudioSource audioSource;
    public AudioSource hit;
    private AudioClip dataAudio;

    [SerializeField] private GameObject deathVFX;
    [SerializeField] public int maxHealth = 10000;
    private int currentHealth;
    float lama = 5;

    private Flash flashEffect;

    public BossHealthBar healthBar;
    private bool hidup = true;
    private bool isPlayed = false;
    [SerializeField] private AudioSource lagu;

    private string[] attackAnimations = { "Attack1", "Attack2", "Attack3" };

    [SerializeField] private ScreenFader screenFader; // Add reference to ScreenFader

    public GameObject BossHealthBar2;

    void Start()
    {
        dataAudio = audioSource.clip;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        minotaur = GameObject.FindGameObjectWithTag("minotaur").transform;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        lagu.Play();

        animator.SetTrigger("Walk");

        flashEffect = GetComponent<Flash>();
        obstacleLayerMask = LayerMask.GetMask("Water");

        
        BossHealthBar2.SetActive(true);
    }

    void Update()
    {
        MoveTowardsPlayer();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < AttackDistance && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
        else if (distanceToPlayer >= AttackDistance && isAttacking)
        {
            animator.SetTrigger("Walk");
            isAttacking = false;
        }
        else if (distanceToPlayer >= AttackDistance && !isAttacking)
        {
            animator.SetTrigger("Walk");
        }
        if (distanceToPlayer >= AttackDistance)
        {
            if (lama <= 0)
            {
                moveSpeed = 10f;
                animator.SetTrigger("Attack3");
            }
            else
            {
                lama -= Time.deltaTime;
            }
        }
        else
        {
            lama = 5;
            moveSpeed = 2f;
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        string randomAttack = attackAnimations[Random.Range(0, attackAnimations.Length)];
        animator.SetTrigger(randomAttack);

        float attackTime = 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < attackTime)
        {
            elapsedTime += Time.deltaTime;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer >= AttackDistance)
            {
                animator.SetTrigger("Walk");
                isAttacking = false;
                yield break;
            }

            yield return null;
        }

        animator.SetTrigger("Walk");
        isAttacking = false;
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Die()
    {
        if (!isPlayed)
        {
            audioSource.PlayOneShot(dataAudio);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0f;
            }
            hidup = false;

            animator.SetTrigger("Die");
            // Instantiate(teleportTrigger, new Vector3(30.9699993f, -16.9300003f,0), new Quaternion(0, 0, 0, 1));
            teleportTrigger.SetActive(true);
            // StartCoroutine(Teleport());
            StartCoroutine(ShowDeathVFX());
        }

        isPlayed = true;
    }

    // IEnumerator Teleport()
    // {
    //     // yield return new WaitForSeconds(2.5f);
    //     yield return StartCoroutine(screenFader.FadeOut());

    //     player.position = new Vector3(-79.75f, -12.11f, 0);

    //     yield return new WaitForSeconds(0.5f);

    //     yield return StartCoroutine(screenFader.FadeIn());
    // }

    IEnumerator ShowDeathVFX()
    {
        // Tunggu beberapa detik untuk memastikan animasi selesai
        yield return new WaitForSeconds(2f); // Ubah angka sesuai dengan durasi animasi "Die"
        

        // Instansiasi efek kematian di posisi bos
        if (deathVFX != null)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
        }

        BossHealthBar2.SetActive(false);

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

    void MoveTowardsPlayer()
    {
        if(hidup == true)
        {
            if (player != null)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                
                RaycastHit2D hit = Physics2D.Raycast(minotaur.position,player.position-minotaur.position,2.6f,obstacleLayerMask);

                if (hit.collider != null)
                {
                    Debug.Log(transform.right);
                    // Hit obstacle, calculate new direction
                    // Vector3 newDirection = Vector3.Reflect(direction, hit.normal); 
                    // transform.position += moveSpeed * Time.deltaTime * newDirection;
                    Vector3 obstacleDirection = Quaternion.Euler(0, 0, 90) * direction; // Putar arah 90 derajat
                    transform.position += obstacleDirection * moveSpeed * Time.deltaTime;
                }
                else
                {
                    // No obstacle hit, move towards player
                    transform.position += moveSpeed * Time.deltaTime * direction;
                }
                // Debug.DrawRay(minotaur.position,player.position-minotaur.position, Color.green);
                Debug.Log(minotaur.position);
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


}
