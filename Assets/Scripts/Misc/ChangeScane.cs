// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class ChangeScane : MonoBehaviour
// {
//     [SerializeField] private Dimana ok;
//     private PlayerController player;
//     // [SerializeField]private GameObject player;
//     public int dimanax =0;
//     public void LoadScene(string sceneName){
//         try{

//         ok.oke = dimanax;

//         }
//         catch{
            
//         }
//         try{
//             player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
//             player.OnDisable();
//         }
//         catch{

//         }
//         SceneManager.LoadScene(sceneName);
//     }
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class ChangeScane : MonoBehaviour
// {
//     [SerializeField] private Dimana ok;
//     private PlayerController player;
//     public int dimanax = 0;

//     public void LoadScene(string sceneName)
//     {
//         try
//         {
//             ok.oke = dimanax;
//         }
//         catch
//         {
//             // Handle exception if needed
//         }

//         try
//         {
//             player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
//             if (player != null)
//             {
//                 player.OnDisable(); // Disable player controls
//             }
//         }
//         catch
//         {
//             // Handle exception if needed
//         }

//         SceneManager.LoadScene(sceneName);
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScane : MonoBehaviour
{
    [SerializeField] private Dimana ok;
    private PlayerController player;
    public int dimanax = 0;

    public void LoadScene(string sceneName)
    {
        try
        {
            GameObject[] yok = GameObject.FindGameObjectsWithTag("dimana");
            // Dimana yoi 
            for (int i =0;i<yok.Length;i++){
                Dimana yoi =  yok[i].GetComponent<Dimana>();
                yoi.oke = dimanax;
            }
        }
        catch
        {
            // Handle exception if needed
        }

        try
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (player != null)
            {
                player.OnDisable(); // Disable player controls
            }
        }
        catch
        {
            // Handle exception if needed
        }

        SceneManager.LoadScene(sceneName);
    }

    private void OnLevelWasLoaded(int level)
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (player != null)
            {
                player.OnEnable(); // Re-enable player controls
            }
        }
        catch
        {
            // Handle exception if needed
        }
    }
}

