using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoom
{
    public bool leftWall;
    public bool rightWall;
    public bool upWall;
    public bool downWall;
    public bool isExit;

    public bool hasStatue;

    public MazeRoom(){
        leftWall = true;
        rightWall = true;
        upWall = true;
        downWall = true;
        isExit = false;
        hasStatue = false;
    }

    public bool IsEmpty(){
        if(!leftWall)return false; 
        if(!rightWall)return false; 
        if(!upWall)return false; 
        if(!downWall)return false;  
        return true;
    }

    public void OpenWall(int[] move,bool opposite){
        if(opposite){
            move = new int[]{-move[0],-move[1]};
        }
        if(move[0] == -1){
            leftWall = false;
        }else if(move[0] == 1){
            rightWall = false;
        }else if(move[1] == 1){
            downWall = false;
        }else if(move[1] == -1){
            upWall = false;
        }
    }
}
