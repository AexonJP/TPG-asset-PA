using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimana : MonoBehaviour
{
    public int oke=0;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("dimana").Length < 2){
            DontDestroyOnLoad(gameObject);
        }
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
