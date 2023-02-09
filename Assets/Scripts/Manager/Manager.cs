using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Maze maze;

    public static Manager instance;

    void Awake()
    {
        instance = this;
        endlessMode = false;
    }

    public void StartNewEndlessGame(){
        endlessMode = true;
        maze.mazeSize = 5;
        endlessModeLevel = 1;
        MenuGUI.instance.UpdateCurrentLevelLabel(endlessModeLevel);
        StartLevel();
    }

    public void StartNewNormalGame(int mapSize){
        endlessMode = false;
        endlessModeLevel = 1;
        maze.mazeSize = mapSize;
        MenuGUI.instance.UpdateCurrentLevelLabel(endlessModeLevel);
        StartLevel();
    }

    public void EndLevel(bool force){
        inGame = false;
        if(endlessMode && !force){
            endlessModeLevel++;
            MenuGUI.instance.UpdateCurrentLevelLabel(endlessModeLevel);
            if(endlessModeLevel%2 == 0 && maze.mazeSize<80)maze.mazeSize++;
            StartLevel();
        }else{
            MenuGUI.instance.ShowMainHUD();
            MenuGUI.instance.ShowPauseMenu(false);
            Time.timeScale = 1;
        }
    }

    public void StartLevel(){
        StartCoroutine(LaunchGame());
    }


    private List<int> keys;

    public Vector3 start;

    public GameObject[] Key_Spawners;

    [SerializeField] private KeyManager_GUI Key_UI;
    [SerializeField] private HaroldMovements harold;

    public bool inGame;

    public bool endlessMode;

    private int endlessModeLevel;

    IEnumerator LaunchGame(){
        inGame = false;
        yield return maze.GenerateMaze();

        Vector2 room = maze.GetRandomRoom(true);
        start = new Vector3(room.x*10,5,room.y*10);
        ResetAllKeys(); 

        PlayerMovement.body.transform.position = start;
        
        Vector2 roomH = maze.GetRandomRoom(false);
        while(roomH.Equals(room)){
            roomH = maze.GetRandomRoom(false);
        }
        harold.SetNewSpawn(new Vector3(roomH.x*10,5,roomH.y*10));

        maze.GenerateNavMesh();
        
        inGame = true;
        MenuGUI.instance.ShowGameHUD();
        yield return null;
    }


    void Pause(){
        if(Time.timeScale==0){
            MenuGUI.instance.ShowPauseMenu(false);
            Time.timeScale = 1;
        }else{
            MenuGUI.instance.ShowPauseMenu(true);
            GameObject.Find("GamePad").transform.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value = GameObject.Find("Player").GetComponent<PlayerMovement>().rotationspeed;
            Time.timeScale = 0;
        }
    }

    public void AddKey(int key){
        if(!keys.Contains(key)){
            keys.Add(key);
            Update_UI();
        }
    }

    public bool HasKey(int key){
        return keys.Contains(key);
    }

    void Update_UI(){
        Key_UI.DeleteAllKeys();
        Key_UI.ShowKeys(keys.ToArray());
    }

    public void ResetAllKeys(){
        keys = new List<int>();
        keys.Add(0);
        for(int i = 0;i<Key_Spawners.Length;i++){
            Key_Spawners[i].GetComponent<KeySpawner>().DestroyKey();
            Key_Spawners[i].GetComponent<KeySpawner>().SpawnKey();
        }
        Update_UI();
    }


    void Update(){
        if(!inGame) return;

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button6)){
            Pause();
        }
    }
}
