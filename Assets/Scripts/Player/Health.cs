using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private Flash player;
    public BossHealthBar healthBarFill;
    [SerializeField] private GameObject trigger;

    
    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Flash>();
        Time.timeScale = 1f;
        currentHealth = maxHealth;
        healthBarFill.SetMaxHealth(currentHealth);
        // story.
        // UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        // currentHealth -= damageAmount;
        // currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        // UpdateHealthBar();
        currentHealth -= damage;
        healthBarFill.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }

        // if (currentHealth <= 0)
        // {
        //     Die();
        // }
    }

    public void TakeDamagePlayer(int damage)
    {
        // currentHealth -= damageAmount;
        // currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        // UpdateHealthBar();
        
        StartCoroutine(player.FlashRoutine());
        currentHealth -= damage;
        healthBarFill.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }

        // if (currentHealth <= 0)
        // {
        //     Die();
        // }
    }

    // public void Heal(int healAmount)
    // {
    //     // currentHealth += healAmount;
    //     // currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    //     UpdateHealthBar();
    // }

    // private void UpdateHealthBar()
    // {
    //     if (healthBarFill != null)
    //     {
    //         float fillAmount = (float)currentHealth / maxHealth;
    //         healthBarFill.fillAmount = fillAmount;
    //     }
    // }

    private void Die()
    {
        // currentHealth = 100;
        // healthBarFill.SetHealth(currentHealth);
        // Implement your player death logic here
        trigger.SetActive(true);
        Debug.Log("Player died!");
    }
}