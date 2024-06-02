using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBGM : MonoBehaviour
{
    [SerializeField] private GameObject BGM;
    // Start is called before the first frame update
    void Start()
    {
        BGM.SetActive(true);
    }
}
