using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject LevelText;
    [Tooltip("Toplam win ")]
    public GameObject WinScoreText;
    [Tooltip("level de?erini tutar")]
    public int level;
    [Tooltip("levelin ground tutan liste")]
    public List<GameObject> LevelGround;




    #region Oyun degiskenleri
    public static bool isGameStarted = false;
    public static bool isGameEnded = false;
    public static bool isGameWined = false;
    public static bool isGameFailed = false;

//harf notu/bara etkisi var
    public int tubeGlas; //deney tüpü/bilim insani
    public int JawyerItem;//kitap ve terazi
    public int Item; //artist

    //harf notu bara etki edicek geri kalanları hem bar hemde sağdaki texte

    public TextMeshProUGUI ItemJob;
    public TextMeshProUGUI tubeGlasUI;
    public TextMeshProUGUI JawyerItemUI;


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


        // #region level islemleri
        // level = PlayerPrefs.GetInt("level");
        // if (level >= 10) { level = 0; }

        // LevelGround[level].SetActive(true);
        // level++;
        // LevelText.GetComponent<TMPro.TextMeshProUGUI>().text = "LEVEL " + level.ToString();
        // level--;
        //#endregion

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

          
            UIManager.UI.GamePanel.SetActive(false);
            UIManager.UI.WinPanel.SetActive(true);

        }

    }
    /// <summary>
    /// Oyun kaybedildigi zaman calisir
    /// </summary>
    public void OnlevelFailed()
    {
        if (isGameEnded == true && isGameFailed == true)
        {



            UIManager.UI.GamePanel.SetActive(false);
            UIManager.UI.FailPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Start butonuna bas?nca ?al???r
    /// </summary>
    public void StartButton()
    {

        OnGameStart();
        UIManager.UI.StartPanel.SetActive(false);
        UIManager.UI.GamePanel.SetActive(true);
        // PathCreation.Examples.PathFollower.instance.SpeedSet(PlayerManager.instance.Speed);

    }

    /// <summary>
    /// oyunu kazand?ktan sonra ??kan paneldeki next level butonuna bas?nca ?al???r
    /// </summary>
    public void NextLevelButton()
    {
        level++;
        PlayerPrefs.SetInt("level", level);
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// oyunu kaybetti?inde sonra ??kan paneldeki restart butonuna bas?nca ?al???r
    /// </summary>
    public void RestartButton()
    {
        SceneManager.LoadScene(2);
    }


    public void ItemAdd(int size)
    {
        Item += size;   
        ItemJob.text = Item.ToString();
    }
}