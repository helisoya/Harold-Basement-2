using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonoBehaviour
{
    private GameObject player;

    private GameObject harold;

    public AudioClip[] found_clips;

    private int cooldown = 0;

    private new AudioSource audio;

    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        harold = GameObject.Find("Harold").gameObject;
        audio = GetComponent<AudioSource>();
    }

    void PlayAudio(AudioClip clip){
        audio.Stop();
        audio.clip = clip;
        audio.Play();
        cooldown = 1500;
        
    }

    void SetColor_Eyes(Color32 Color){
        if(transform.Find("Statue").Find("eye1").GetComponent<Renderer>().material.color==Color){
            return;
        }
        transform.Find("Statue").Find("eye1").GetComponent<Renderer>().material.color = Color;
        transform.Find("Statue").Find("eye2").GetComponent<Renderer>().material.color = Color;
    }


    void Update()
    {

        if(cooldown>0){
            cooldown--;
        }

        RaycastHit raycast;

        if(Physics.Raycast(transform.position,player.transform.position-transform.position,out raycast)){
            if(raycast.transform==player.transform){ // Trolley a vu le joueur

                float dist_p = Mathf.Sqrt(
                    Mathf.Pow(transform.position.x-player.transform.position.x,2)+
                    Mathf.Pow(transform.position.y-player.transform.position.y,2)+
                    Mathf.Pow(transform.position.z-player.transform.position.z,2));

                if(dist_p<=8){ // Distance <= 10 pour etre vu
                    SetColor_Eyes(new Color32(255,0,0,255));
                    harold.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(player.transform.position);
                    if(cooldown==0){
                        PlayAudio(found_clips[Random.Range(0,found_clips.Length)]);
                        cooldown = 1500;
                    }
                }else{
                    SetColor_Eyes(new Color32(0,255,0,255));
                }
                
            }

        }else{
            SetColor_Eyes(new Color32(0,255,0,255));
        }


        
    }
}
