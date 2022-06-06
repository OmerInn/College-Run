using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager UIinstance;

    public List<GameObject> Panels;
    public List<GameObject> imageList;
  //  public GameObject LevelText;
    public static UIManager UI;
    public GameManager GameManager;
    public GameObject levelText;


    private void Awake()
    {
        UI = this;
    }

    public void StartButton()
    {
        GameManager.GetPlayer().PlayerStartSet();
        GameManager.isGameStarted = true;
        PanelActive("GamePanel");
    }
    public void FailButton()
    {
        GameManager.isGameFailed = true;
        SceneManager.LoadScene(0);


    }
    public void NextLevelButton()
    {
        GameManager.level++;
        PlayerPrefs.SetInt("level", GameManager.level);
        GameManager.levelSize++;
        PlayerPrefs.SetInt("levelSize",GameManager.levelSize);
        SceneManager.LoadScene(0);
    }

    public void PanelActive(string PanelName)
    {
        Panels.ForEach(x =>
        {

            if (PanelName == x.name) x.SetActive(true);
            else x.SetActive(false);
        });
        if (GameManager.isGameEnded == true &&GameManager.isGameWined == true)
        {
            Panels[3].SetActive(true);
            Panels[1].SetActive(true);
        }
    }
    public void imageActive()
    {
        imageList.ForEach(x =>
        {
            
         x.SetActive(false);

        });
        imageList[(int)GameManager.GetPlayer().MyJobType].SetActive(true);
    }
    public void FinishScenes()
    {
        PanelActive("FinishPanel");
    }
    public void GameFinish()
    {
        PanelActive("WinPanel");
    }
    public void LevelTextWrite()
    {
        levelText.GetComponent<TMP_Text>().text = "Level  " + (GameManager.levelSize + 1).ToString();
    }
    public void ImagePanel()
    {
        PanelActive("ImagePanel");
    }
}
