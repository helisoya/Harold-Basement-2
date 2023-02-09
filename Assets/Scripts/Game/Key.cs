using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int key;

    private GameObject player;

    private Manager manager;

    public AudioClip pickup;

    private new AudioSource audio;

    private bool isSetToDie = false;

    private Vector3[] anim;

    private int curr = 0;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        manager = GameObject.Find("Manager").GetComponent<Manager>();

        anim = new Vector3[2];
        anim[0] = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
        anim[1] = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }

    IEnumerator Destroy(){
        yield return new WaitWhile(()=> audio.isPlaying);
        Destroy(gameObject);

    }


    void Update()
    {
        if(transform.position!=anim[curr]){
            transform.position = Vector3.MoveTowards(transform.position,anim[curr],0.01f);
        }else{
            if(curr==0){
                curr=1;
            }else{
                curr=0;
            }
        }

        float dist = Mathf.Sqrt(
            Mathf.Pow(transform.position.x-player.transform.position.x,2)+
            Mathf.Pow(transform.position.y-player.transform.position.y,2)+
            Mathf.Pow(transform.position.z-player.transform.position.z,2));

        if(dist<=4 && !isSetToDie){
            isSetToDie = true;
            manager.AddKey(key);
            audio.clip=pickup;
            audio.Play();
            GetComponent<SpriteRenderer>().sprite=null;
            StartCoroutine("Destroy");
        }
    }
}
