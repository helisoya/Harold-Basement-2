using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject player;

    void Start(){
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.LookAt(player.transform);
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
    }
}
