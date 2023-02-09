using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tableau : MonoBehaviour
{
    public AudioClip[] player_clips;

    public AudioClip[] harold_clips;

    private new AudioSource audio;

    private int cooldown = 0;

    private GameObject player;

    private GameObject harold;

    private float dist_h = 0f;

    private float dist_p = 0f;


    void Start(){
        audio = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        harold = GameObject.Find("Harold");
    }

    void PlayAudio(AudioClip clip){
        audio.Stop();
        audio.clip = clip;
        audio.Play();
        cooldown = 2000;
        
    }

    void Update(){
        if(cooldown>0){
            cooldown--;
        }else{

            RaycastHit hit;

            if(Physics.Raycast(transform.position,player.transform.position-transform.position,out hit)){
                if(hit.transform == player.transform){
                    dist_p = Mathf.Sqrt(
                        Mathf.Pow(transform.position.x-player.transform.position.x,2)+
                        Mathf.Pow(transform.position.y-player.transform.position.y,2)+
                        Mathf.Pow(transform.position.z-player.transform.position.z,2)); 

                    if(dist_p <= 10){
                        PlayAudio(player_clips[Random.Range(0,player_clips.Length)]);
                    }
                }
            }
            if(dist_p > 10){
                if(Physics.Raycast(transform.position,harold.transform.position-transform.position,out hit)){
                    if(hit.transform==harold.transform){
                        dist_h = Mathf.Sqrt(
                            Mathf.Pow(transform.position.x-harold.transform.position.x,2)+
                            Mathf.Pow(transform.position.y-harold.transform.position.y,2)+
                            Mathf.Pow(transform.position.z-harold.transform.position.z,2));

                        if(dist_h <= 10){
                            PlayAudio(harold_clips[Random.Range(0,harold_clips.Length)]);
                        }
                    }

                }
            }




        }
    }
}
