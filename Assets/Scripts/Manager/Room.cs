using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject upWall;
    [SerializeField] private GameObject downWall;

    [SerializeField] private GameObject exit;

    [SerializeField] private GameObject statue;

    public void Initialize(MazeRoom room){
        leftWall.SetActive(room.leftWall);
        rightWall.SetActive(room.rightWall);
        upWall.SetActive(room.upWall);
        downWall.SetActive(room.downWall);
        exit.SetActive(room.isExit);

        if(!room.hasStatue) Destroy(statue);
    }
}
