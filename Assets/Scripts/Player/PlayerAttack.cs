using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private Transform rotasi;
    private float waitTime = 1.0f;
    private float timer = 0.0f;
    private bool pukul = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if ((timer < waitTime) && (pukul == true)){ 
            // Debug.Log("click kiri");
            //  rotasi.m_LocalRotation= new Vector3(0,0,39);
            rotasi.Rotate(0, 0, -360 * Time.deltaTime);
            // Debug.Log(Time.deltaTime);
        }
        else if ((timer >= waitTime) && (pukul == true)){
            timer = 0;
            pukul = false;
        }
        else{
            pukul = Input.GetMouseButton(0);
        }
        
    }
}
