using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if(col.tag == "Player"){
            Manager.instance.EndLevel(false);
        }
    }
}
