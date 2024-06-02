using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeleportDie : MonoBehaviour
{
    [SerializeField] private ScreenFader screenFader; // Add reference to ScreenFader
    private Transform player;
    public float x;
    public float y;
    public float z;
    [SerializeField] private Camera kamera;
    public bool OnKamera=false;

    public bool fade = false;

    [SerializeField]private GameObject boss;

    public GameObject trigger;

    public bool animasi = false;
    // private BossTrigger1 bosstrigger;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // bosstrigger = trigger.GetComponent<BossTrigger1>();
        // grim = GameObject.FindGameObjectWithTag("grim");
        // try{
        // grim = GameObject.FindGameObjectWithTag("grim");
        // if(grim != null){

        
        // }
        // else{
        //     Debug.Log("error di grim");
        // }
        // }
        // catch{
        //     // grim = GameObject.FindGameObjectWithTag("grim");
        //     // oke = new grim.activeInHierarchy;
        // }
        if(!animasi){
            StartCoroutine(Teleport());
        }
        else{
            StartCoroutine(Teleports());
        }
    }

    // Update is called once per frame
    IEnumerator Teleport()
    {
        // yield return new WaitForSeconds(3f);
        yield return StartCoroutine(screenFader.FadeOut());

        player.position = new Vector3(x,y,z);

        // bosstrigger.kena=false;
        if(OnKamera){
            kamera.orthographicSize=5.734053f;
        }
        // yield return new WaitForSeconds(1f);

        if(!fade){

            yield return StartCoroutine(screenFader.FadeIn());
            // yield return new WaitForSeconds(1f);
        }
        
        if(boss != null){

            boss.SetActive(true);
        }
    }

    IEnumerator Teleports()
    {
        // yield return new WaitForSeconds(3f);
        yield return StartCoroutine(screenFader.FadeOut());

        player.position = new Vector3(x,y,z);

        // bosstrigger.kena=false;
        if(OnKamera){
            kamera.orthographicSize=5.734053f;
        }
        // yield return new WaitForSeconds(1f);

        if(boss != null){

            boss.SetActive(true);
        }
        
    }

}
