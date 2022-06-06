using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Cinemachine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Player saga ve sola gidebilecegi max ve min deger")]
    [SerializeField] Vector2 MinMaxPlayerPos;
    [Tooltip("Player saga ve sola gidebilecegi max ve min savrulma degeri")]
    [SerializeField] Vector2 MinMaxPlayerSensivity;
    float distanceFixer = 0;
    private GameObject OffSetObj;
    private Vector3 temp, temp2;
    [Tooltip("Player?n sahip olmas? gereken y?kseklik")]
    public float PlayerPosY;
    Vector3 oldPosition = Vector3.zero;
    public static PlayerController playerController;
    public int Speed = 6;
    #region JobList
    public List<GameObject> JudgeList; //yarg??
    public List<GameObject> TheScientistList;// bilim insan?
    public List<GameObject> ArtistList;// Ressam 
    #endregion
    public int JobLevel;
   

    public Constants.PlayerJobType MyJobType;
    public List<GameObject> JobList;
    public GameObject Judge;
    public GameObject TheScientist;
    public GameObject Artist;
    public GameObject Jobless;
    public ItemController itemController;


    #region Characters Animation
    public enum AnimationCase { Idle, Walk, Dance, Pose, Sad };
    public GameObject FirstCharacter;
    public GameObject DirtyJudge;
    public GameObject JudgeAnim;
    public GameObject DirtyTheScientist;
    public GameObject TheScientistAnim;
    public GameObject DirtyArtist;
    public GameObject ArtistAnim;
    public AnimationCase animationCase;
    #endregion 
    public GameObject[] Targets;
    public GameObject Player;

    

    public ParticleSystem CapParticle;
    public CinemachineVirtualCamera cinemachine;
    public CinemachineTransposer body;
    private void Awake()
    {
        playerController = this;
    }
    void Start()
    {
        OffSetObj = new GameObject();
        OffSetObj.name = "OffSetObje";
        OffSetObj.transform.parent = this.transform.parent;
        body = cinemachine.GetCinemachineComponent<CinemachineTransposer>();
        CapParticle = GetComponent<ParticleSystem>();
        
    }
    void Update()
    {
        #region Movement
        if (GameManager.isGameStarted && !GameManager.isGameEnded && !GameManager.isGameFailed)
        {
           
            if (Input.GetMouseButtonDown(0)) //down dokundu?umda bir kere
            {
                OffSetObj.transform.localPosition = new Vector3(CalculateX() * 3, PlayerPosY, 0); //dokundu?um noktay? game sahnesinde alg?lamas?na yarar.
                temp = this.transform.localPosition - OffSetObj.transform.localPosition; //temp=dokundu?um nokta ile offsetim aras?ndaki mesafe.
                distanceFixer = Vector3.Distance(this.transform.position, OffSetObj.transform.localPosition);
                oldPosition = OffSetObj.transform.localPosition; //old positiona ilk dokundu?um noktay? alg?lat?yoruz.
            }
            if (Input.GetMouseButton(0)) //dokundu?um s?rece ?al???r.
            {
                OffSetObj.transform.localPosition = new Vector3(CalculateX() * 3, PlayerPosY, 0);
                temp2 = OffSetObj.transform.localPosition + temp; //dokundu?um nokta ve mesafenin (temp) toplam? temp2 ye e?it olsun.
                                                                  //(temp2.y = PlayerPosY; //y sini hep sabit tutmas? i?in e?itliyoruz.
                temp2.z = 0;
                temp2.x = Mathf.Clamp(temp2.x, MinMaxPlayerPos.x, MinMaxPlayerPos.y); //temp2 nin x de?erini min max de?erler aras?na ta??yorum.Y dede ayn?s?.

                this.transform.localPosition = temp2; //pozisyon atamas? yap?l?yor.

                if (distanceFixer - 0.1f > Vector3.Distance(this.transform.position, OffSetObj.transform.position)) //t?klay?p b?rakma i?leminde sa? sol yapmamak i?in vard?r.
                {
                    OffSetObj.transform.localPosition = new Vector3(CalculateX() * 3, PlayerPosY, 0);
                    temp = this.transform.localPosition - OffSetObj.transform.localPosition;
                    distanceFixer = Vector3.Distance(this.transform.position, OffSetObj.transform.localPosition);
                }
            }
        }
        #endregion
    }
    float CalculateX() //Dokundugum nokta ekranda nerede
    {
        Vector3 location = Input.mousePosition;
        return (location.x / (Screen.width / (MinMaxPlayerSensivity.y + Mathf.Abs(MinMaxPlayerSensivity.x)))) - 5;
    }


    public void PlayerStartSet()
    {
        animationCase = PlayerController.AnimationCase.Walk;
        ChangeAnimation();
        PathCreation.Examples.PathFollower.pathFollower.speed =Speed;
    }



    #region Character Transform (Karakter De?i?imi , typeSet)
    public void TypeJob()
    {

        JobListNonActive();
        JobList[(int)MyJobType].SetActive(true);
        if (JobLevel == 1)
        {
            JobList[(int)MyJobType].transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (JobLevel == 2)
        {
            JobList[(int)MyJobType].transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void JobListNonActive()
    {
        JobList.ForEach(x => { x.SetActive(false); });
    }
    #endregion
    #region OnTrigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (other.GetComponent<ItemController>().MyTypes == Constants.ItemTypes.DestroyerGirl || other.GetComponent<ItemController>().MyTypes == Constants.ItemTypes.DestroyerMan)
            {

                LevelBarController.levelBarController.BarController(other.GetComponent<ItemController>().DestroyerSize);
                other.gameObject.GetComponent<FollowCharacters>().enabled = true;
                other.gameObject.GetComponent<FollowCharacters>().ImpactCharacter(this.gameObject);
            }
            else if (other.GetComponent<ItemController>().MyTypes == Constants.ItemTypes.FMinus)
            {
                ItemSizePlus(other.GetComponent<ItemController>().FminusSize);
                other.gameObject.SetActive(false);
            }
            else if (other.GetComponent<ItemController>().MyTypes == Constants.ItemTypes.APlus)
            {
                ItemSizePlus(other.GetComponent<ItemController>().APlusSize);
                other.gameObject.SetActive(false);
            }
            else
            {
                ItemSizePlus(other.GetComponent<ItemController>().ItemSize);
                other.gameObject.SetActive(false);
            }
           
        }
        if (other.gameObject.CompareTag("Door"))
        {
            JobLevel = 2;
            other.GetComponent<Collider>().enabled = false;
            LevelBarController.levelBarController.BarController(other.GetComponent<DoorSelectionController>().gateSize);
            MyJobType = other.GetComponent<DoorSelectionController>().DoorType;
            if (JobLevel < 2)
            {
                JobLevel++;
            }
            TypeJob();
            GameManager.instance.ItemListObjeActive();
            GameManager.instance.ItemImageActive();
        }
      
       
        if (other.gameObject.CompareTag("Finish"))
        {
            PathCreation.Examples.PathFollower.pathFollower.pathCreator = null;
            if (LevelBarController.levelBarController.slider.value > 85 && GameManager.instance.Item > 35&&JobLevel==2)
            {
                Player.transform.DOMove(Targets[4].transform.position, 3f).OnComplete(() =>
                {
                    animationCase = AnimationCase.Walk;
                    ChangeAnimation();
                }).OnComplete(() =>
                {
                    
                        Player.transform.DOMove(Targets[5].transform.position, 2f).OnComplete(()=> {
                            Player.transform.DORotate(new Vector3(0, 180, 0), 0.2f).SetRelative().OnComplete(() =>
                            {
                                body.m_FollowOffset.z = 10;
                                DOTween.Play(CapParticle);
                                animationCase = AnimationCase.Idle;
                                ChangeAnimation();
                                StartCoroutine(FlameSetactive()); 
                            });                                            
                    });
                   
                });

                


            }
            else if (LevelBarController.levelBarController.slider.value > 80 && GameManager.instance.Item > 30)
            {
                Player.transform.DOMove(Targets[3].transform.position, 2f).OnComplete(() =>
                {
                    animationCase = AnimationCase.Dance;
                    ChangeAnimation();
                });
            }
            else if (LevelBarController.levelBarController.slider.value > 60 && GameManager.instance.Item > 25)
            {
                Player.transform.DOMove(Targets[2].transform.position, 2f).OnComplete(() =>
                {
                    animationCase = AnimationCase.Dance;
                    ChangeAnimation();
                });
              
            }
            else if (LevelBarController.levelBarController.slider.value > 40 && GameManager.instance.Item > 20)
            {
                Player.transform.DOMove(Targets[1].transform.position, 2f).OnComplete(() =>
                {
                    animationCase = AnimationCase.Dance; 
                    ChangeAnimation();
                });
            }
            else if (LevelBarController.levelBarController.slider.value >20 && GameManager.instance.Item > 5)
            {
                Player.transform.DOMove(Targets[0].transform.position, 1f).OnComplete(() =>
                {
                    animationCase = AnimationCase.Dance;
                    ChangeAnimation();
                });
            }
        }

    }

    public void ItemSizePlus(int Size)
    {
        GameManager.instance.ItemAdd(Size);
        LevelBarController.levelBarController.BarController(Size);
    }
    private IEnumerator FlameSetactive()
    {
        GameManager.instance.UIManager.FinishScenes();
        yield return new WaitForSeconds(0.1f);

        //Turn My game object that is set to false(off) to True(on).
        GameManager.instance.UIManager.imageList[4].SetActive(true);

        yield return new WaitForSeconds(0.1f);

        //Game object will turn off
        GameManager.instance.UIManager.imageList[4].SetActive(false);

        yield return new WaitForSeconds(1);
        GameManager.instance.UIManager.imageList[3].SetActive(true);


        //Turn the Game Oject back off after 1 sec.

    }

    #endregion
    //{ Idle, Walk, Dance, Pose, Sad };
    #region Character Animation SwichCase
    public void ChangeAnimation()
    {
        switch (animationCase)
        {
            case AnimationCase.Idle:
                CharacterChange();
                break;
            case AnimationCase.Walk:
                CharacterWalkAnim();
                break;
            case AnimationCase.Dance:
                CharacterDanceAnim();
                break;
            case AnimationCase.Pose:
                CharacterPoseAnim();
                break;

            case AnimationCase.Sad:
                CharacterSadAnim();
                break;
        }
    }
    #endregion

    #region Character Animation 
    public void CharacterChange()
    {
        switch (MyJobType)
        {
            case Constants.PlayerJobType.Jobless:
                FirstCharacter.GetComponent<Animator>().Play("Idle");
                break;
           case Constants.PlayerJobType.Judge:
            switch (JobLevel)
                {

                    case 1:
                        DirtyJudge.GetComponent<Animator>().Play("Idle");
                        break;
                    case 2:
                        JudgeAnim.GetComponent<Animator>().Play("Idle");
                        break;
                }
                    break;
            
            case Constants.PlayerJobType.Artist:
            switch (JobLevel)
                {
                    case 1:
                        DirtyArtist.GetComponent<Animator>().Play("Idle");
                        break;
                    case 2:
                        ArtistAnim.GetComponent<Animator>().Play("Idle");
                        break;
                }
                   break;
            case Constants.PlayerJobType.TheScientist:
            switch (JobLevel)
                {
                    case 1:
                        DirtyTheScientist.GetComponent<Animator>().Play("Idle");
                        break;
                    case 2:
                        TheScientistAnim.GetComponent<Animator>().Play("Idle");
                        break;
                }
                break;
        }

    }
   
    public void CharacterWalkAnim()
    {
        switch (MyJobType)
        {
            case Constants.PlayerJobType.Jobless:
                 FirstCharacter.GetComponent<Animator>().Play("Walking");
                break;
            case Constants.PlayerJobType.Judge:
                switch (JobLevel)
                {

                    case 1:
                        DirtyJudge.GetComponent<Animator>().Play("Walking");
                        break;
                    case 2:
                        JudgeAnim.GetComponent<Animator>().Play("Walking");
                        break;
                }
                break;
            case Constants.PlayerJobType.Artist:
                switch (JobLevel)
                {
                    case 1:
                        DirtyArtist.GetComponent<Animator>().Play("Walking");
                        break;
                    case 2:
                        ArtistAnim.GetComponent<Animator>().Play("Walking");
                        break;
                }
                break;
            case Constants.PlayerJobType.TheScientist: //çalıştırıyımmı sadece dance 1 2 yaptım artistte deneyelim
                switch (JobLevel)
                {

                    case 1:
                       DirtyTheScientist.GetComponent<Animator>().Play("Walking");
                        break;
                    case 2:
                        TheScientistAnim.GetComponent<Animator>().Play("Walking");
                        break;
                }
                break;
        }
    }
    public void CharacterSadAnim()
    {
        switch (MyJobType)
        {
            case Constants.PlayerJobType.Jobless:
                FirstCharacter.GetComponent<Animator>().Play("Sad");
                break;
            case Constants.PlayerJobType.Judge:
                switch (JobLevel)
                {

                    case 1:
                        DirtyJudge.GetComponent<Animator>().Play("Sad");
                        break;
                    case 2:
                        JudgeAnim.GetComponent<Animator>().Play("Sad");
                        break;
                }
                break;
            case Constants.PlayerJobType.Artist:
                switch (JobLevel)
                {
                    case 1:
                        DirtyArtist.GetComponent<Animator>().Play("Sad");
                        break;
                    case 2:
                        ArtistAnim.GetComponent<Animator>().Play("Sad");
                        break;
                }
                break;
            case Constants.PlayerJobType.TheScientist:
                switch (JobLevel)
                {

                    case 1:
                        DirtyTheScientist.GetComponent<Animator>().Play("Sad");
                        break;
                    case 2:
                        TheScientistAnim.GetComponent<Animator>().Play("Sad");
                        break;
                }
                break;
        }
    }
    public void CharacterPoseAnim()
    {
        switch (MyJobType)
        {
            case Constants.PlayerJobType.Jobless:
                FirstCharacter.GetComponent<Animator>().Play("Pose");
                break;
            case Constants.PlayerJobType.Judge:
                switch (JobLevel)
                {

                    case 1:
                        DirtyJudge.GetComponent<Animator>().Play("Pose");
                        break;
                    case 2:
                        JudgeAnim.GetComponent<Animator>().Play("Pose");
                        break;
                }
                break;
            case Constants.PlayerJobType.Artist:
                switch (JobLevel)
                {
                    case 1:
                        DirtyArtist.GetComponent<Animator>().Play("Pose");
                        break;
                    case 2:
                        ArtistAnim.GetComponent<Animator>().Play("Pose");
                        break;
                }
                break;
            case Constants.PlayerJobType.TheScientist:
                switch (JobLevel)
                {

                    case 1:
                        DirtyTheScientist.GetComponent<Animator>().Play("Pose");
                        break;
                    case 2:
                        TheScientistAnim.GetComponent<Animator>().Play("Pose");
                        break;
                }
                break;
        }
    }
    public void CharacterDanceAnim()
    {
        switch (MyJobType)
        {
            
            case Constants.PlayerJobType.Jobless:
                FirstCharacter.GetComponent<Animator>().Play("Dance");
                break;
            case Constants.PlayerJobType.Judge:
                switch (JobLevel) 
                {
     //2 dkk değiştiriyim
                    case 1:
                        DirtyJudge.GetComponent<Animator>().Play("Dance");
                        break;
                    case 2:
                        Judge.GetComponent<Animator>().Play("Dance");
                        break;
                }
                break;
            case Constants.PlayerJobType.Artist:
                switch (JobLevel)
                {
                    case 1: 
                        DirtyArtist.GetComponent<Animator>().Play("Dance");
                        break;
                    case 2:
                        Debug.Log("dance");

                        ArtistAnim.GetComponent<Animator>().Play("Dance"); 
                        break;
                }
                break;
            case Constants.PlayerJobType.TheScientist:
                switch (JobLevel)
                {

                    case 1:
                        DirtyTheScientist.GetComponent<Animator>().Play("Dance");
                        break;
                    case 2:
                        TheScientistAnim.GetComponent<Animator>().Play("Dance");
                        break;
                }
                break;
        }
    }
  
    #endregion 


}


