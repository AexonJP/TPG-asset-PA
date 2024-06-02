// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class TextMunculHilang : MonoBehaviour
// {
//     public Transform player;
//     public float maxDistance = 10f; // Jarak maksimum di mana teks akan muncul
//     public float minDistance = 2f;  // Jarak minimum di mana teks akan tersembunyi

//     void Update()
//     {
//         float distance = Vector3.Distance(transform.position, player.position);
//         TextMeshPro textMesh = GetComponent<TextMeshPro>(); // Mendapatkan komponen TextMeshPro dari objek ini
//         if (distance <= maxDistance && distance >= minDistance)
//         {
//             textMesh.enabled = true; // Munculkan teks jika dalam jarak yang ditentukan
//         }
//         else
//         {
//             textMesh.enabled = false; // Sembunyikan teks jika di luar jarak yang ditentukan
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextMunculHilang : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 10f; // Jarak maksimum di mana teks akan muncul
    public float minDistance = 2f;  // Jarak minimum di mana teks akan tersembunyi
    public float fadeInDuration = 0.5f; // Durasi fade in
    public float fadeOutDuration = 0.5f; // Durasi fade out

    private TextMeshPro textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>(); // Mendapatkan komponen TextMeshPro dari objek ini
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= maxDistance && distance >= minDistance)
        {
            // Fade in animasi
            textMesh.DOFade(1f, fadeInDuration);
        }
        else
        {
            // Fade out animasi
            textMesh.DOFade(0f, fadeOutDuration);
        }
    }
}
