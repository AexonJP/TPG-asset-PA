using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public string playerTag = "Player";

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.CompareTag(playerTag))
    //     {
    //         DamagePlayer(other.gameObject);
    //     }
    // }

    private void DamagePlayer(GameObject player)
    {
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

    // This function is called from the animation event
    public void DamagePlayerFromAnimation()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(playerTag))
            {
                DamagePlayer(collider.gameObject);
                break;
            }
        }
    }
}