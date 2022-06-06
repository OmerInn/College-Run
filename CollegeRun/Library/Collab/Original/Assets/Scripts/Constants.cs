using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static Constants instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public enum PlayerJobType
    {
        Judge=0, TheScientist=1, Artist=2, Jobless=3
    }
    public enum ItemTypes
    {
        Book=0,Blance=1, SmallTube=2, TestCube=3,Palette=4,Paint=5,APlus=6,FMinus=7,DestroyerMan=8,DestroyerGirl=9
    }
}
