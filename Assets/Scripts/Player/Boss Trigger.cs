using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BossTrigger : MonoBehaviour
{
    private string playerTag = "Player";
    private bool kena = false;
    [SerializeField] private Camera kamera;
    private GameObject boss;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            boss = GameObject.FindGameObjectWithTag("minotaur");
            boss.SetActive(false);
        }
        catch
        {
            // boss = GameObject.FindGameObjectWithTag("grim");
            // boss.SetActive(false);
        }
        // Ensure the GameObject has a BoxCollider2D component
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true; // Make sure the collider is a trigger
    }

    // Update is called once per frame
    void Update()
    {
        if (kena && kamera.orthographicSize <= 13)
        {
            kamera.orthographicSize += 0.1f;
        }
        else if(kamera.orthographicSize >= 13){
            kena = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            boss.SetActive(true);
            kena = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            // boss.SetActive(false);
            boxCollider.isTrigger = false;
            // kena = false;
        }
    }
}
