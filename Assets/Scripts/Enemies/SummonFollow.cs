using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SummonFollow : MonoBehaviour
{
    private Transform player;
    private float moveSpeed =5f;
    private bool facingRight = true;
    private int maxHealth = 1;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                
                // RaycastHit2D hit = Physics2D.Raycast(transform.position,player.position-transform.position,2.6f,obstacleLayerMask);

                // if (hit.collider != null)
                // {
                //     Debug.Log(transform.right);
                //     // Hit obstacle, calculate new direction
                //     // Vector3 newDirection = Vector3.Reflect(direction, hit.normal); 
                //     // transform.position += moveSpeed * Time.deltaTime * newDirection;
                //     Vector3 obstacleDirection = Quaternion.Euler(0, 0, 90) * direction; // Putar arah 90 derajat
                //     transform.position += obstacleDirection * moveSpeed * Time.deltaTime;
                // }
                // else
                // {
                    // No obstacle hit, move towards player
                    transform.position += moveSpeed * Time.deltaTime * direction;
                // }
                // Debug.DrawRay(grim.position,player.position-grim.position, Color.green);
                // Debug.Log(grim.position);
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

     void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
            // TakeDamage(arrow.damageArrow);
            Destroy(gameObject);

            // StartCoroutine(flashEffect.FlashRoutine());
            
            // Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("SwordCollider"))
        {
            DamageSource sword = collision.gameObject.GetComponent<DamageSource>();
            // TakeDamage(sword.damageAmount);
            Destroy(gameObject);
            
            // StartCoroutine(flashEffect.FlashRoutine());
        }
        else if (collision.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
    
    // void TakeDamage(int damage)
    // {
    //     currentHealth -= damage;
    //     // healthBar.SetHealth(currentHealth);
    //     if (currentHealth <= 0)
    //     {
    //         Destroy(gameObject);
    //     }
    // // }
    // void Die()
    // {   
    //     // Menonaktifkan komponen Rigidbody2D agar bos tidak bergerak saat die
    //     Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //     if (rb != null)
    //     {
    //         rb.velocity = Vector2.zero;
    //         rb.gravityScale = 0f;
    //     }
        
    //     // animator.SetTrigger("Die");
    //     // Destroy(gameObject, 2f);
    //     StartCoroutine(ShowDeathVFX());
    // }
    // IEnumerator ShowDeathVFX()
    // {
    //     // Tunggu beberapa detik untuk memastikan animasi selesai
    //     yield return new WaitForSeconds(2f); // Ubah angka sesuai dengan durasi animasi "Die"

    //     // // Instansiasi efek kematian di posisi bos
    //     // if (deathVFX != null)
    //     // {
    //     //     Instantiate(deathVFX, transform.position, Quaternion.identity);
    //     // }

    //     Destroy(gameObject);
    // }

}
