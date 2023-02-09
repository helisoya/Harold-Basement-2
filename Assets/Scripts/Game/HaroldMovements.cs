using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HaroldMovements : MonoBehaviour
{

    private NavMeshAgent agent;

    public int speed = 5;

    private bool PlayerSeen = false;

    private new AudioSource audio;

    private int cooldown_audio = 10000;
    public AudioClip[] search_clips;
    public AudioClip[] found_clips;
    public AudioClip[] lost_clips;
    private Vector3 start;
    private GameObject[] default_waypoints;
    public bool UltraInstinct = false;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
    }


    public void SetNewSpawn(Vector3 newSpawn){
        start=newSpawn;
        transform.position = newSpawn;
    }

    public void Init(){
        agent.SetDestination(GetRandomTarget());
    }

    Vector3 GetRandomTarget(){
        Vector2 room = Manager.instance.maze.GetRandomRoom(false);
        return new Vector3(room.x*10,0,room.y*10);
    }

    void PlayAudio(AudioClip clip){
        audio.Stop();
        audio.clip = clip;
        audio.Play();
        cooldown_audio = 10000;
        
    }

    public bool CanSeePlayer(){
        RaycastHit raycast;

        if(Physics.Raycast(transform.position,PlayerMovement.body.transform.position-transform.position,out raycast)){
            if(raycast.transform==PlayerMovement.body.transform){ // Harold a vu le joueur
                return true;
            }
        }
        return false;
    }


    void Update(){

        if(!Manager.instance.inGame) return;

        if(cooldown_audio>0){
            cooldown_audio--;
        }else{
            PlayAudio(search_clips[Random.Range(0,search_clips.Length)]);
        }

        if(agent.speed!=speed){
            agent.speed = speed;
        }
        
        if(agent.remainingDistance==0){
            agent.SetDestination(GetRandomTarget());
            if(PlayerSeen){
                PlayAudio(lost_clips[Random.Range(0,lost_clips.Length)]);
                PlayerSeen=false;
            }
        }


        if(CanSeePlayer() || UltraInstinct){
            if(!PlayerSeen){
                PlayAudio(found_clips[Random.Range(0,found_clips.Length)]);
            }
            PlayerSeen = true;
            agent.SetDestination(PlayerMovement.body.transform.position);
                
        }

        float dist_p = Mathf.Sqrt(
            Mathf.Pow(transform.position.x-PlayerMovement.body.transform.position.x,2)+
            Mathf.Pow(transform.position.y-PlayerMovement.body.transform.position.y,2)+
            Mathf.Pow(transform.position.z-PlayerMovement.body.transform.position.z,2));

        if(dist_p<=5 && PlayerSeen){ // Joueur Attrapé

            if(Manager.instance.endlessMode){
                Manager.instance.EndLevel(true);
                return;
            }


            PlayerMovement.body.transform.position = Manager.instance.start;
            GameObject.Find("Manager").GetComponent<Manager>().ResetAllKeys();
            agent.Warp(start);
            agent.SetDestination(GetRandomTarget());
        }
    }
}
