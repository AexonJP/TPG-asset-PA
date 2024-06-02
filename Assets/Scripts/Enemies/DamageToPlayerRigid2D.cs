using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayerRigid2D : MonoBehaviour
{
    private string playerTag = "Player";
    // private Rigidbody2D boxCollider;
    public int damageAmount = 10;
    GameObject player;
    // private Flash DamagePlayerEffect;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // DamagePlayerEffect = player.GetComponent<Flash>();
        // boxCollider = gameObject.GetComponent<Rigidbody2D>();
        // boxCollider.isTrigger = true;
    }
    private void DamagePlayer(GameObject player)
    {
        // StartCoroutine(DamagePlayerEffect.FlashRoutine());
        
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamagePlayer(damageAmount);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            DamagePlayer(player);
        }
    }
}
