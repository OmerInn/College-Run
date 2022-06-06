using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSelectionController : MonoBehaviour
{
    public int gateSize=50;
    // public PlayerController.JobType GateStatus;
    public Constants.PlayerJobType DoorType;
    public static DoorSelectionController doorSelectionController;

    // Start is called before the first frame update
    void Awake()
    {
        doorSelectionController = this;
    }

   
}
