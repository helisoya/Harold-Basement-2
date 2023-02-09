using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuGUI : MonoBehaviour
{
    [SerializeField] private GameObject gameHUD;
    [SerializeField] private GameObject mainMenuHUD;
    [SerializeField] private GameObject normalGameMenuHUD;
    [SerializeField] private GameObject endlessGameMenuHUD;

    [SerializeField] private Slider normalGameMapSize;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private TextMeshProUGUI floorLabel;

    public static MenuGUI instance;

    void Awake(){
        instance = this;
    }

    public void UpdateCurrentLevelLabel(int newFloor){
        floorLabel.text = "Etage "+newFloor.ToString();
    }


    public void ShowPauseMenu(bool value){
        pauseMenu.SetActive(value);
    }


    public void LaunchNormalGame(){
        Manager.instance.StartNewNormalGame((int)(normalGameMapSize.value));
    }

    public void LaunchEndlessGame(){
        Manager.instance.StartNewEndlessGame();
    }


    public void ShowNormalHUD(){
        mainMenuHUD.SetActive(false);
        endlessGameMenuHUD.SetActive(false);
        gameHUD.SetActive(false);
        normalGameMenuHUD.SetActive(true);
    }


    public void ShowEndlessHUD(){
        mainMenuHUD.SetActive(false);
        endlessGameMenuHUD.SetActive(true);
        gameHUD.SetActive(false);
        normalGameMenuHUD.SetActive(false);
    }

    public void ShowMainHUD(){
        PlayerMovement.body.transform.position = new Vector3(0,300,0);
        mainMenuHUD.SetActive(true);
        endlessGameMenuHUD.SetActive(false);
        gameHUD.SetActive(false);
        normalGameMenuHUD.SetActive(false);
    }

    public void ShowGameHUD(){
        mainMenuHUD.SetActive(false);
        endlessGameMenuHUD.SetActive(false);
        gameHUD.SetActive(true);
        normalGameMenuHUD.SetActive(false);
    }

}
