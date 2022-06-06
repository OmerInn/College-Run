using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FollowCharacters : MonoBehaviour
{
    public GameObject DestroyerMan;
    public GameObject DestroyerGirl;


    public void ImpactCharacter(GameObject obj)
    {
        if (DestroyerMan.activeSelf)
        {
            this.transform.DOMove(obj.transform.position, 5f); ;
            DestroyerMan.GetComponent<Animator>().Play("Walking");
            Destroy(DestroyerMan,10f);
        }
        else if (DestroyerGirl.activeSelf)
        {
            this.transform.DOMove( obj.transform.position, 5f);
            DestroyerMan.GetComponent<Animator>().Play("Walking");
            Destroy(DestroyerGirl, 10f);
        }
    }
}
