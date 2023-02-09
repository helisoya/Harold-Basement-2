using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager_GUI : MonoBehaviour
{

    public GameObject template_key;

    public GameObject holder;



    public void ShowKeys(int[] keys){
        for(int i = 0;i<keys.Length;i++){
            GameObject key = Instantiate(template_key,holder.transform);
            key.transform.position += new Vector3(35,0,0) * i;
            key.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = keys[i].ToString();
        }
    }

    public void DeleteAllKeys(){
        for(int i =0;i<holder.transform.childCount;i++){
            Destroy(holder.transform.GetChild(i).gameObject);
        }
    }

}
