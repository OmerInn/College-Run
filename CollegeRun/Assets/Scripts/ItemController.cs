    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public List<GameObject> ItemList;

    public Constants.ItemTypes MyTypes;
    public int ItemSize;
    public int APlusSize;
    public int FminusSize;
    public int DestroyerSize;
    public ParticleSystem Shine,Shine1;
    public void Start()
    {
        StartItemActiveObject(MyTypes);
    }
    public void DoorObjectActive (Constants.PlayerJobType PlayerjoyType)
    {
        if (MyTypes != Constants.ItemTypes.FMinus&& MyTypes != Constants.ItemTypes.DestroyerGirl&& MyTypes != Constants.ItemTypes.DestroyerMan)
        {
            Shine.Play();
            Shine1.Play();
            if (MyTypes!=Constants.ItemTypes.APlus&& MyTypes != Constants.ItemTypes.FMinus&& MyTypes != Constants.ItemTypes.DestroyerMan &&MyTypes != Constants.ItemTypes.DestroyerGirl)
        {
            nonActive();
            switch (PlayerjoyType)
            {
                case Constants.PlayerJobType.Artist:
                    if (ActiveObjeSize() == 0) ItemList[4].SetActive(true);
                    else { ItemList[5].SetActive(true); }
                    break;
                case Constants.PlayerJobType.Judge:
                    if (ActiveObjeSize() == 0) ItemList[0].SetActive(true);
                    else { ItemList[1].SetActive(true); }
                    break;
                case Constants.PlayerJobType.TheScientist:
                    if (ActiveObjeSize() == 0) ItemList[2].SetActive(true);
                    else { ItemList[3].SetActive(true); }
                    break;
            }
        }

        }
    }
    public void StartItemActiveObject(Constants.ItemTypes itemType)
    {
        ItemList[(int)itemType].SetActive(true);
    }
    public int ActiveObjeSize()
    {
        if ((int)MyTypes % 2 == 0) return 0;
        else return 1;
    }
    public void nonActive()
    {
        ItemList.ForEach(x =>
        {
            x.SetActive(false);
        });
    }
}
