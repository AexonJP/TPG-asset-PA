using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayerBox2D : MonoBehaviour
{
    private string playerTag = "Player";
    private BoxCollider2D boxCollider;
    public int damageAmount = 10;
    GameObject player;
    // private Flash DamagePlayerEffect;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // DamagePlayerEffect = player.GetComponent<Flash>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }
    private void DamagePlayer(GameObject player)
    {
        
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamagePlayer(damageAmount);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            DamagePlayer(player);
        }
    }
}
