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
        SceneManager.LoadScene("0");


    }
    public void NextLevelButton()
    {

    }

    public void PanelActive(string PanelName)
    {
        Panels.ForEach(x =>
        {
            if (PanelName == x.name) x.SetActive(true);
            else x.SetActive(false);
        });
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
    //public void LevelTextWrite()
    //{
    //    LevelText.GetComponent<TMPro.TextMeshPro>().text ="Level" + GameManager.level+1;
    //}
}
