using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public int level;
    public int levelSize;
    [Tooltip("levelin ground tutan liste")]
    public List<GameObject> LevelGround;

    public PlayerController Playercontroller;
    public UIManager UIManager;

    #region Oyun degiskenleri
    public static bool isGameStarted = false;
    public static bool isGameEnded = false;
    public static bool isGameWined = false;
    public static bool isGameFailed = false;

    public int Item; //artist

    public TextMeshProUGUI ItemJob;


    public GameObject ItemParent;
    public List<ItemController> ItemList;
    public static GameManager instance;
    #endregion

    private void Awake()
    {
        if (instance == null) { instance = this; }

    }

    private void Start()
    {
        isGameStarted = false;
        isGameEnded = false;
        isGameWined = false;
        isGameFailed = false;


        #region level islemleri
        level = PlayerPrefs.GetInt("level");
        levelSize = PlayerPrefs.GetInt("levelSize");
        
        if (level >= LevelGround.Count) { level = 0; }
        ItemParent = LevelGround[level].transform.GetChild(0).gameObject;//yeni levelda item parent levelin en üsteki cocugu olmalı
        LevelGround[level].SetActive(true);
        UIManager.LevelTextWrite();
        #endregion
        ItemListAddElement();

    }
    public void Update()
    {

    }

    #region Oyun ilerleme islemleri
    public void OnGameStart()
    {
        if (isGameStarted == false && isGameEnded == false)
        {
            isGameStarted = true;
            isGameEnded = false;
        }
    }
    public void OnGameEnded() 
    {
        if (isGameEnded == false && isGameStarted == true)
        {
            isGameEnded = true;
            isGameStarted = false;

        }
        if (isGameEnded && isGameFailed) OnlevelFailed();
        if (isGameEnded && isGameWined) OnlevelCompleted();
    }
    #endregion
    /// <summary>
    /// Oyun kazanildigi zaman calisir
    /// </summary>
    public void OnlevelCompleted()
    {
        if (isGameEnded == true && isGameWined == true)
        {
            UIManager.PanelActive("WinPanel");
        }
    }
    /// <summary>
    /// Oyun kaybedildigi zaman calisir
    /// </summary>
    public void OnlevelFailed()
    {
        if (isGameEnded == true && isGameFailed == true)
        {
            UIManager.PanelActive("FailPanel");
        }
    }
    public void ItemAdd(int size)
    {
        /* if (size<0)
         {
             if (Item<=0)
             {

                 Item = 0;
                 ItemJob.text = Item.ToString();
             }
             else
             {
                 if (Item-size<=0)
                 {

                 }
             }
         }*/
        if (Item+size<=0)
        {
            Item = 0;
            ItemJob.text = Item.ToString();
        }
        else
        {
            Item += size;
            ItemJob.text = Item.ToString();
        }
    }
    public PlayerController GetPlayer() { return Playercontroller; }

    public void ItemListAddElement()
    {
        for(int i = 0; i < ItemParent.transform.childCount; i++)
        {
            ItemList.Add(ItemParent.transform.GetChild(i).gameObject.GetComponent<ItemController>());
        }
    }
    public void ItemListObjeActive()
    {
        ItemList.ForEach(x =>
        x.DoorObjectActive(Playercontroller.MyJobType));
    }
   
    public void ItemImageActive()
    {
        UIManager.imageActive();
    }


}