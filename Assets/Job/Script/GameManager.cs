using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string _sSet_Area_Finish_One;
    public static string _sSet_Area_Finish_Two;
    public static string _sPlayer_One_Finish;
    public static string _sPlayer_Two_Finish;
    public static string _sGame_Start;
    public static string _sGame_End;
    public static string _sScene_Transfer_End;
    public static int _iPlayer1_Transfer_Area_Count;
    public static int _iPlayer2_Transfer_Area_Count;
   
    GameStateManager m_GameStateManager = new GameStateManager();
    public GameObject _gTmp_Player_UI;
    public GameObject _gPlayer1;
    public GameObject _gPlayer2;
    private GameObject m_NowPlayer;
    //public GameObject _gNow_Player_UI;

    public GameObject _gPlayer1_UI;
    public GameObject _gPlayer2_UI;

    public bool _bMove_Btn_Click;
    public bool _bAttack_Btn_Click;

    [SerializeField]
    private Button m_TmpBtn;
    // Start is called before the first frame update
    void Start()
    {
        _iPlayer1_Transfer_Area_Count = 3;
        _iPlayer2_Transfer_Area_Count = 3;

    }

    // Update is called once per frame
    void Update()
    {
        m_GameStateManager.GameStateUpdate();
        if (_sScene_Transfer_End == "Start")
        {
            m_GameStateManager.Set_GameState(new SetArea_One(m_GameStateManager));
            _sScene_Transfer_End = "End";
            _sSet_Area_Finish_One = "Start";
            //_sGame_Start = "Start";
        }
        
        if (_sPlayer_One_Finish == "Start")
        {
            _gPlayer1_UI.SetActive(true);
            _gPlayer2_UI.SetActive(false);
        }
        else if (_sPlayer_Two_Finish == "Start")
        {
            _gPlayer1_UI.SetActive(false);
            _gPlayer2_UI.SetActive(true);
        }

        
    }

    public void Select_Player(int index)
    {
        if(_gTmp_Player_UI!=null)
        {
            _gTmp_Player_UI.SetActive(false);
        }
        if (GameManager._sPlayer_One_Finish == "Start")
        {
            _gPlayer1_UI.transform.GetChild(index).gameObject.SetActive(true);
            _gPlayer1_UI.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            //gameObject.GetComponent<Path>().Reset_List();
            gameObject.GetComponent<Path>()._gPlayer = _gPlayer1.transform.GetChild(index).gameObject;
            //gameObject.GetComponent<Path>().Save_CharacterPos();
            //gameObject.GetComponent<Path>().Find_Move_Way();
            //gameObject.GetComponent<Path>().Find_Attack_Way();
            m_NowPlayer = _gPlayer1.transform.GetChild(index).gameObject;
            _gTmp_Player_UI = _gPlayer1_UI.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject;
        }
        else if (GameManager._sPlayer_Two_Finish == "Start")
        {
            _gPlayer2_UI.transform.GetChild(index).gameObject.SetActive(true);
            _gPlayer2_UI.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(true);
           // gameObject.GetComponent<Path>().Reset_List();
            gameObject.GetComponent<Path>()._gPlayer = _gPlayer2.transform.GetChild(index).gameObject;
           // gameObject.GetComponent<Path>().Save_CharacterPos();
            
            
            m_NowPlayer = _gPlayer2.transform.GetChild(index).gameObject;
            _gTmp_Player_UI = _gPlayer2_UI.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject;
        }
    }

    public void Move_Btn_Click(Button Move_Btn)
    {
        if(_bAttack_Btn_Click==true)
        {
            m_NowPlayer.GetComponent<Move>().Reset_Data();
            gameObject.GetComponent<Path>().Reset_List();
            _bAttack_Btn_Click = false;
        }
        if (_bMove_Btn_Click == false)
        {
            if(m_TmpBtn!=null)
            {
                m_TmpBtn.interactable = true;
            }
           
            Move_Btn.interactable = false;
            m_TmpBtn = Move_Btn;
            //Attack_Btn.interactable = true;
        }
        gameObject.GetComponent<Path>().Find_Move_Way();
        m_NowPlayer.GetComponent<Move>().Move_Grid_Instant();
        _bMove_Btn_Click = true;
    }


    public void Attack_Btn_Click(Button Attack_Btn)
    {
        if (_bMove_Btn_Click == true)
        {
            m_NowPlayer.GetComponent<Move>().Reset_Data();
            gameObject.GetComponent<Path>().Reset_List();
            _bMove_Btn_Click = false;
        }
        if(_bAttack_Btn_Click == false)
        {
            if (m_TmpBtn != null)
            {
                m_TmpBtn.interactable = true;
            }
            Attack_Btn.interactable = false;
            m_TmpBtn = Attack_Btn;
        }
        _bAttack_Btn_Click = true;
        gameObject.GetComponent<Path>().Find_Attack_Way();
        m_NowPlayer.GetComponent<Attack>().Attack_Grid_Instant();
    }

    public void End_Btn_Click()
    {
        if(m_TmpBtn!=null)
        {
            m_TmpBtn.interactable = true;
            m_TmpBtn = null;
        }
        if (_sPlayer_One_Finish == "Start")
        {
            _sPlayer_One_Finish = "End";
           
        }
        if (_sPlayer_Two_Finish == "Start")
        {
           _sPlayer_Two_Finish = "End";
            
           
        }

        m_NowPlayer.GetComponent<Move>().Reset_Data();
        gameObject.GetComponent<Path>().Reset_List();
        _bMove_Btn_Click = false;
        _bAttack_Btn_Click = false;


    }
}
