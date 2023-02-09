using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject player;

    private GameObject harold;

    private Sprite sprite;

    public int key;

    private Manager manager;

    void Start()
    {
        player = GameObject.Find("Player");
        harold = GameObject.Find("Harold");
        sprite = GetComponent<SpriteRenderer>().sprite;
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        transform.Find("number").transform.Find("side1").GetComponent<TMPro.TextMeshPro>().text=key.ToString();
        transform.Find("number").transform.Find("side2").GetComponent<TMPro.TextMeshPro>().text=key.ToString();
    }

    void Update()
    {
        float dist_p = Mathf.Sqrt(
            Mathf.Pow(transform.position.x-player.transform.position.x,2)+
            Mathf.Pow(transform.position.y-player.transform.position.y,2)+
            Mathf.Pow(transform.position.z-player.transform.position.z,2));

        float dist_h = Mathf.Sqrt(
            Mathf.Pow(transform.position.x-harold.transform.position.x,2)+
            Mathf.Pow(transform.position.y-harold.transform.position.y,2)+
            Mathf.Pow(transform.position.z-harold.transform.position.z,2));

        if( ( (dist_p<=4 && manager.HasKey(key)) || dist_h<=4 ) && GetComponent<SpriteRenderer>().sprite!=null){
            GetComponent<AudioSource>().Play();
            GetComponent<BoxCollider>().enabled=false;
            GetComponent<SpriteRenderer>().sprite=null;
            transform.Find("number").gameObject.SetActive(false);
        }else if(dist_p>4 && dist_h>4 && GetComponent<SpriteRenderer>().sprite==null){
            GetComponent<SpriteRenderer>().sprite = sprite;
            GetComponent<BoxCollider>().enabled=true;
            transform.Find("number").gameObject.SetActive(true);
        }
    }
}
