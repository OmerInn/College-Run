using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelBarController : MonoBehaviour
{
    public Slider slider;
    public static LevelBarController levelBarController;
    public float currentLevelBar;
    public int firstLevelBar;

    private void Awake()
    {
        levelBarController = this;
    }
    private void Start()
    {
        currentLevelBar = firstLevelBar;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpLevelBar(float levelBar)
    {
        slider.value = levelBar;
    }
    public void BarController(int i)
    {
        currentLevelBar += i;
        UpLevelBar(currentLevelBar);
        if (currentLevelBar>100)
        {
            if (PlayerController.playerController.JobLevel<2)
            {
                PlayerController.playerController.JobLevel++;
                PlayerController.playerController.TypeJob();
                currentLevelBar = 0;
                UpLevelBar(currentLevelBar);
            }
            else
            {
                currentLevelBar = 100;
                UpLevelBar(currentLevelBar);
            }
        }
        else if (currentLevelBar<0)
        {
            
            if (PlayerController.playerController.JobLevel>0)
            {
                currentLevelBar = 0;
                PlayerController.playerController.JobLevel-=1;
                PlayerController.playerController.TypeJob();
                UpLevelBar(currentLevelBar);
            }
        }
        
    }
}
