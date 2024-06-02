using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] public int damageAmount = 5;
    
    private void OnTriggerEnter2D(Collider2D other) {
        // // Cek jika objek yang bersentuhan memiliki komponen BossController
        // FlyingEye boss1 = other.gameObject.GetComponent<FlyingEye>();
        // if (boss1 != null)
        // {
        //     boss1.TakeDamage(damageAmount);
        //     return; // Keluar dari fungsi setelah menyerang bos
        // }

        SlimeHP enemyHealth = other.gameObject.GetComponent<SlimeHP>();
        enemyHealth?.TakeDamage(damageAmount);
    }
}
