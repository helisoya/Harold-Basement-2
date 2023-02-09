using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Maze : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navmesh;
    [SerializeField] private GameObject prefabRoom;

    [SerializeField] private Transform Roomsparent;
    private MazeRoom[][] maze;
    [HideInInspector] public int mazeSize;

    private int[] down = new int[]{0,1};

    private int[] up = new int[]{0,-1};

    private int[] left = new int[]{-1,0};

    private int[] right = new int[]{1,0};


    public Vector2 GetRandomRoom(bool checkForExit){
        int x = Random.Range(0,mazeSize);
        int y = Random.Range(0,mazeSize);

        while(maze[y][x].IsEmpty() || (maze[y][x].isExit && checkForExit) || maze[y][x].hasStatue){
            x = Random.Range(0,mazeSize);
            y = Random.Range(0,mazeSize);
        }

        return new Vector2(x,y);
    }

    IEnumerator ResetMaze(){
        maze = new MazeRoom[mazeSize][];
        for(int y = 0;y < mazeSize;y++){
            maze[y] = new MazeRoom[mazeSize];
            for(int x = 0; x < mazeSize; x++){
                maze[y][x] = new MazeRoom();
            }
        }
        yield return null;
    }

    IEnumerator GenerationAlgorithm(){
        List<int[]> ways;
        int maxTunnel = 5 * mazeSize;
        int maxSizeHallway = 5;
        int randomSizeHallway = Random.Range(1,maxSizeHallway+1);
        int currentSizeHallway = 0;
        int x = 0;
        int y = 0;

        while(maxTunnel > 0){
            ways = new List<int[]>();
            ways.Add(up);
            ways.Add(left);
            ways.Add(down);
            ways.Add(right);
            if (x <= 0){
                ways.Remove(left);
            }  
            if( x >= mazeSize-1){
                ways.Remove(right);
            } 
            if (y <= 0){
                ways.Remove(up);
            }  
            if (y >= mazeSize-1){
                ways.Remove(down);
            }
                
            int[] move = ways[Random.Range(0,ways.Count)];
            while(currentSizeHallway < randomSizeHallway){
                if(x+move[0] > mazeSize-1 || y+move[1] > mazeSize-1 || x+move[0] < 0 || y+move[1] < 0){
                    currentSizeHallway = randomSizeHallway;
                    break;
                }

                maze[y][x].OpenWall(move,false);
                x+=move[0];
                y+=move[1];
                maze[y][x].OpenWall(move,true);
                currentSizeHallway++;

                if(Random.Range(0,100) <= 2){
                    maze[y][x].hasStatue = true;
                }
                
            }

            currentSizeHallway = 0;
            randomSizeHallway = Random.Range(1,maxSizeHallway+1);
            maxTunnel--;
            
        }

        yield return null;
        maze[y][x].isExit = true;
        maze[y][x].hasStatue = false;
    }

    public IEnumerator GenerateMaze(){
        yield return ResetMaze();

        yield return GenerationAlgorithm();

        yield return Create3DMaze();
    }

    IEnumerator Create3DMaze(){

        foreach(Transform child in Roomsparent){
            Destroy(child.gameObject);
        }

        for(int y = 0;y < mazeSize;y++){
            for(int x = 0; x < mazeSize; x++){
                if(maze[y][x].IsEmpty()) continue;
                Instantiate(prefabRoom,new Vector3(x*10,0,y*10),new Quaternion(),Roomsparent).GetComponent<Room>().Initialize(maze[y][x]);

                yield return new WaitForEndOfFrame();
            }
        }
        yield return null;
    }

    public void GenerateNavMesh(){
        navmesh.BuildNavMesh();
    }

}
