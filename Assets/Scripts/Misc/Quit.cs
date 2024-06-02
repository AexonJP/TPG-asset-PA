using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    // Fungsi untuk keluar dari aplikasi
    public void QuitGame()
    {
        // Jika sedang menjalankan di editor Unity
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // Jika menjalankan build aplikasi
                Application.Quit();
        #endif
    }
}
