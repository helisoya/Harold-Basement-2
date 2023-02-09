using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap_Cam : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float camHeight;

    void Update()
    {
        transform.position = new Vector3(player.position.x,camHeight,player.position.z);
    }
}
