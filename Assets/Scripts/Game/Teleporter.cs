using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private GameObject player;
    private GameObject harold;

    public GameObject tp_out;
    public bool CanTeleport = true;

    public bool CanTeleport_H = true;

    void Start()
    {
        player = GameObject.Find("Player");
        harold = GameObject.Find("Harold");
    }

    void Update()
    {
        float dist_p = Mathf.Sqrt(
            Mathf.Pow(transform.position.x-player.transform.position.x,2)+
            Mathf.Pow(transform.position.y-player.transform.position.y,2)+
            Mathf.Pow(transform.position.z-player.transform.position.z,2));

        if(dist_p<=4 && CanTeleport){
            tp_out.GetComponent<Teleporter>().CanTeleport = false;
            player.transform.position = tp_out.transform.position;
        }else if(dist_p>4 && !CanTeleport){
            CanTeleport = true;
        }

        float dist_h = Mathf.Sqrt(
            Mathf.Pow(transform.position.x-harold.transform.position.x,2)+
            Mathf.Pow(transform.position.y-harold.transform.position.y,2)+
            Mathf.Pow(transform.position.z-harold.transform.position.z,2));

        if(dist_h<=5 && CanTeleport_H){
            tp_out.GetComponent<Teleporter>().CanTeleport_H = false;
            harold.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(tp_out.transform.position);
        }else if(dist_h>5 && !CanTeleport_H){
            CanTeleport_H = true;
        }
    }
}
