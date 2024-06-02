using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] public int damageArrow = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SlimeHP enemyHealth = collision.gameObject.GetComponent<SlimeHP>();

        // Hancurkan arrow setelah memberikan damage atau bertabrakan dengan objek lain
        Destroy(gameObject);
        enemyHealth?.TakeDamage(damageArrow);
    }
}
