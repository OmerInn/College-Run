using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager UIinstance;
    public GameObject StartPanel;
    public GameObject FailPanel;
    public GameObject WinPanel;
    public GameObject GamePanel;
    public GameObject Images;
    public GameObject FinishPanel;
    public static UIManager UI;



    private void Awake()
    {
        UI = this;
    }

    public void StartButton()
    {
        PlayerController.playerController.animationCase = PlayerController.AnimationCase.Walk;
        PlayerController.playerController.ChangeAnimation();
        PathCreation.Examples.PathFollower.pathFollower.speed = PlayerController.playerController.Speed;
        GameManager.isGameStarted = true;
        GamePanel.SetActive(true);
        StartPanel.SetActive(false);
        WinPanel.SetActive(false);
        Images.SetActive(true);
        FinishPanel.SetActive(true);
    }
    public void FailButton()
    {
        GameManager.isGameFailed = true;
        SceneManager.LoadScene("0");


    }
    public void NextLevelButton()
    {

    }
}
