using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsRandom : MonoBehaviour
{
    public GameObject[] spawness;
    int a ,b= 0;
    private void Start()
    {
        RandomGate();
    }
    public void RandomGate()
    {
        RandomGet();
        
        spawness[a].SetActive(true);
        spawness[a].transform.localPosition=new Vector3( 2.3f,0,0);
        spawness[b].SetActive(true);
        spawness[b].transform.localPosition = new Vector3(-0.5f, 0, 0);

    }
    public void RandomGet()
    {
        a = Random.Range(0, 3);
        b = Random.Range(0, 3);
        if (a == b)
        {
            RandomGet();
        }
        else
        {
            return;
        }
    }
}
