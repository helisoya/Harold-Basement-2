using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject key_template;

    public int key;


    public void SpawnKey(){
        GameObject instance = Instantiate(key_template,transform);
        instance.GetComponent<Key>().key=key;
    }

    public void DestroyKey(){
        for(int i = 0; i< transform.childCount;i++){
            Destroy(transform.GetChild(i).gameObject);
        }
        
    }
}
