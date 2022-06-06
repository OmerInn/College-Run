using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsRandom : MonoBehaviour
{
    public GameObject[] spawness;


    public Transform SpawnPos;
    int randomInt;
    private void Update()
    {
          
    }
    void RandomS()
    {
        randomInt = Random.Range(0, spawness.Length);
        Instantiate(spawness[randomInt], SpawnPos.position, SpawnPos.rotation);
    }
}
