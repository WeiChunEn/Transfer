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
    public static int _iPlayer1_Transfer_Area_Count; //1轉職區域的數量
    public static int _iPlayer2_Transfer_Area_Count; //2轉職區域的數量

    static GameStateManager m_GameStateManager = new GameStateManager();
    public GameObject _gTmp_Player_UI;
    public GameObject _gPlayer1;
    public GameObject _gPlayer2;
    private GameObject m_NowPlayer;
    //public GameObject _gNow_Player_UI;

    public GameObject _gPlayer1_UI;         //A team 整個UI物件
    public GameObject _gPlayer2_UI;         //B team 整個UI物件
    public GameObject _gNow_Player_UI;      //Now Player 整個UI物件
    public GameObject _gNow_Player_Function_UI; //Now Player 功能UI物件
    public GameObject _gPlayer_One_Camera; //A team Camera
    public GameObject _gPlayer_Two_Camera; //B team Camera

    public GameObject _gWin_Menu; //勝利畫面
    public GameObject _gAWin_Image;
    public GameObject _gBWin_Image;

    public Button _bMove_Btn;
    public Button _bAttack_Btn;
    public Button _bNew_Game_Btn;
    public Button _bMain_Menu_Btn;


   
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

            //m_GameStateManager.Set_GameState(new SetArea_One(m_GameStateManager));
            m_GameStateManager.Set_GameState(new Player1Turn(m_GameStateManager));
            _sPlayer_One_Finish = "Start";
            _sSet_Area_Finish_One = "End";
            _sSet_Area_Finish_Two = "End";
            _sScene_Transfer_End = "End";
            Set_Now_Team();
            //_sScene_Transfer_End = "End";
            //_sSet_Area_Finish_One = "Start";
            //_sGame_Start = "Start";
        }
        if(_gPlayer1.transform.childCount==0)
        {
            _gWin_Menu.SetActive(true);
            _gBWin_Image.SetActive(true);
        }
        else if(_gPlayer2.transform.childCount==0)
        {
            _gWin_Menu.SetActive(true);
            _gAWin_Image.SetActive(true);
        }

        //if (m_NowPlayer!=null)
        //{
        //    Set_Character_Btn();
        //}
        
        

    }

    /// <summary>
    /// 顯示當下隊伍的UI
    /// </summary>
    public void Set_Now_Team()
    {
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
    /// <summary>
    /// 選擇腳色
    /// </summary>
    /// <param name="index"></param>
    public void Select_Player(int index)
    {
        //if (_gTmp_Player_UI != null)
        //{
        //    _gTmp_Player_UI.SetActive(false);
        //}
        if (GameManager._sPlayer_One_Finish == "Start")
        {
            _gNow_Player_UI = _gPlayer1_UI.transform.GetChild(index).gameObject;
            _gNow_Player_Function_UI = _gNow_Player_UI.transform.GetChild(0).gameObject;
            _gNow_Player_UI.SetActive(true);
            _gNow_Player_Function_UI.SetActive(true);
            gameObject.GetComponent<Path>()._gPlayer = _gPlayer1.transform.GetChild(index).gameObject;
            m_NowPlayer = _gPlayer1.transform.GetChild(index).gameObject;
            //_gTmp_Player_UI = _gPlayer1_UI.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject;
            _bMove_Btn = _gNow_Player_Function_UI.transform.GetChild(0).GetComponent<Button>();
            _bAttack_Btn = _gNow_Player_Function_UI.transform.GetChild(1).GetComponent<Button>();

        }
        else if (GameManager._sPlayer_Two_Finish == "Start")
        {
            _gNow_Player_UI = _gPlayer2_UI.transform.GetChild(index).gameObject;
            _gNow_Player_Function_UI = _gNow_Player_UI.transform.GetChild(0).gameObject;
            _gNow_Player_UI.SetActive(true);
            _gNow_Player_Function_UI.SetActive(true);
            gameObject.GetComponent<Path>()._gPlayer = _gPlayer2.transform.GetChild(index).gameObject;
            m_NowPlayer = _gPlayer2.transform.GetChild(index).gameObject;
           // _gTmp_Player_UI = _gPlayer2_UI.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject;
            _bMove_Btn = _gNow_Player_Function_UI.transform.GetChild(0).GetComponent<Button>();
            _bAttack_Btn = _gNow_Player_Function_UI.transform.GetChild(1).GetComponent<Button>();
        }
        
    }

    /// <summary>
    /// 腳色移動的按鈕
    /// </summary>
    /// <param name="Move_Btn"></param>
    public void Move_Btn_Click(Button Move_Btn)
    {
        
        m_NowPlayer.GetComponent<Move>().Reset_Data();
        gameObject.GetComponent<Path>().Reset_List();
        gameObject.GetComponent<Path>().Find_Move_Way();
        m_NowPlayer.GetComponent<Move>().Move_Grid_Instant();
        Set_Character_Btn();
        Move_Btn.interactable = false;
       
    }


    /// <summary>
    /// 攻擊的按鈕
    /// </summary>
    /// <param name="Attack_Btn"></param>
    public void Attack_Btn_Click(Button Attack_Btn)
    {

        m_NowPlayer.GetComponent<Move>().Reset_Data();
        gameObject.GetComponent<Path>().Reset_List();
        gameObject.GetComponent<Path>().Find_Attack_Way();
        m_NowPlayer.GetComponent<Attack>().Attack_Grid_Instant();
        Set_Character_Btn();
        Attack_Btn.interactable = false;
    }

    /// <summary>
    /// 結束的按鈕
    /// </summary>
    public void End_Btn_Click()
    {
        _gNow_Player_Function_UI.SetActive(false);
        
        if (_sPlayer_One_Finish == "Start")
        {
            _sPlayer_One_Finish = "End";
            _gPlayer_One_Camera.SetActive(false);
            _gPlayer_Two_Camera.SetActive(true);

        }
        if (_sPlayer_Two_Finish == "Start")
        {
            _sPlayer_Two_Finish = "End";
            _gPlayer_One_Camera.SetActive(true);
            _gPlayer_Two_Camera.SetActive(false);


        }
        
        m_NowPlayer.GetComponent<Move>().Reset_Data();
        gameObject.GetComponent<Path>().Reset_List();
        m_NowPlayer.GetComponent<Character>().Chess.Have_Attacked = false;
        m_NowPlayer.GetComponent<Character>().Chess.Have_Moved = false;
        Set_Character_Btn();

    }


    /// <summary>
    /// 設定腳色移動攻擊的按鈕狀態
    /// </summary>
    public void Set_Character_Btn()
    {
        if (m_NowPlayer.GetComponent<Character>().Chess.Have_Moved == true)
        {
            _bMove_Btn.interactable = false;

        }
        else
        {
            _bMove_Btn.interactable = true;
        }

        if(m_NowPlayer.GetComponent<Character>().Chess.Have_Attacked == true)
        {
            _bAttack_Btn.interactable = false;
        }
        else
        {
            _bAttack_Btn.interactable = true;
        }
    }
}
