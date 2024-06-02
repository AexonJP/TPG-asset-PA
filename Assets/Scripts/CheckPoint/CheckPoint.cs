using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private List<Vector3> tempat;

    [SerializeField] private List<GameObject> story;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera kamera;
    private Dimana dimanas;
    private int dimana;
    // public int dimana=0;
    private void Start()
    {
        dimanas = GameObject.FindGameObjectWithTag("dimana").GetComponent<Dimana>();
        dimana = dimanas.oke;
        // DontDestroyOnLoad(gameObject);
        player.transform.position = tempat[dimana];
        for (int i =0;i<story.Count;i++){
            if(i == dimana){
                story[i].SetActive(true);
            }
            else{
                story[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        if ((dimana == 2) && kamera.orthographicSize <= 13 )
        {
            kamera.orthographicSize += 0.1f;
        }
    }
}
