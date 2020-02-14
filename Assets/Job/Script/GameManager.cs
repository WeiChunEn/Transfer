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
    public Animator _aUI_Anim;     //UI的動畫

    //public GameObject _gTmp_Player_UI;
    public GameObject _gPlayer1;            //A team腳色父物件
    public GameObject _gPlayer2;            //B team腳色父物件
    public GameObject _gMove_UI;            //動UI的
    private GameObject m_NowPlayer;             //現在遊玩的腳色

    //public GameObject _gNow_Player_UI;
    public GameObject _gTranfer_UI;         //轉職的UI

    public GameObject _gPlayer1_UI;         //A team 整個UI物件
    public GameObject _gPlayer2_UI;         //B team 整個UI物件
    public GameObject _gNow_Player_UI;      //Now Player 整個UI物件
    public GameObject _gNow_Player_Function_UI; //Now Player 功能UI物件
    public GameObject _gPlayer_One_Camera; //A team Camera
    public GameObject _gPlayer_Two_Camera; //B team Camera

    public GameObject _gWin_Menu; //勝利畫面
    public GameObject _gAWin_Image;
    public GameObject _gBWin_Image;

    public Button _bMove_Btn;           //移動按鈕
    public Button _bAttack_Btn;         //攻擊按鈕
    public Button _bPlayer_Btn;         //玩家按鈕
    public Button _bNew_Game_Btn;       //重玩一次按鈕
    public Button _bMain_Menu_Btn;      //回首頁按鈕
    public Button _bMove_In_Btn;        //移進UI按鈕
    public Button _bMove_Out_Btn;       //移出UI按鈕

    public int _iA_Team_Num;
    public int _iB_Team_Num;

    public bool _bCheck_Team_Finish; //確認隊伍是否都行動完了
    
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
            //m_GameStateManager.Set_GameState(new Player1Turn(m_GameStateManager));
            //_sPlayer_One_Finish = "Start";
            //_sSet_Area_Finish_One = "End";
            //_sSet_Area_Finish_Two = "End";
            //_sScene_Transfer_End = "End";
            Set_Now_Team();
            _sScene_Transfer_End = "End";
            _sSet_Area_Finish_One = "Start";
            _sGame_Start = "Start";
        }
        Check_Win();
        if (_iA_Team_Num == 0)
        {
            _gWin_Menu.SetActive(true);
            _gBWin_Image.SetActive(true);
        }
        else if (_iB_Team_Num == 0)
        {
            _gWin_Menu.SetActive(true);
            _gAWin_Image.SetActive(true);
        }

        //動畫控制
        AnimatorStateInfo info = _aUI_Anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.3f && info.IsName("Move_In") && _gMove_UI.tag == "Out")
        {
            _bMove_In_Btn.gameObject.SetActive(false);
            _bMove_Out_Btn.gameObject.SetActive(true);
            _bMove_Out_Btn.interactable = true;
            _gMove_UI.tag = "In";
        }
        else if (info.normalizedTime >= 0.3f && info.IsName("Move_Out") && _gMove_UI.tag == "In")
        {
            _bMove_In_Btn.gameObject.SetActive(true);
            _bMove_Out_Btn.gameObject.SetActive(false);
            _bMove_In_Btn.interactable = true;
            _gMove_UI.tag = "Out";
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
        if (_gNow_Player_Function_UI != null)
        {
            _gNow_Player_Function_UI.SetActive(false);
            Set_Character_Btn();
            m_NowPlayer.GetComponent<Move>().Reset_Data();

        }
        
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
            _bPlayer_Btn = _gNow_Player_UI.GetComponent<Button>();
            m_NowPlayer.GetComponent<Character>()._gPlayer_UI = _gNow_Player_UI;




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
            _bPlayer_Btn = _gNow_Player_UI.GetComponent<Button>();
            m_NowPlayer.GetComponent<Character>()._gPlayer_UI = _gNow_Player_UI;


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
        _gNow_Player_Function_UI.SetActive(false);
        Set_Character_Btn();
        Move_Btn.interactable = false;
        In_And_Out();

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
        _gNow_Player_Function_UI.SetActive(false);
        Set_Character_Btn();
        In_And_Out();
        Attack_Btn.interactable = false;
    }

    /// <summary>
    /// 結束的按鈕
    /// </summary>
    public void End_Btn_Click()
    {
        _gNow_Player_Function_UI.SetActive(false);


        m_NowPlayer.GetComponent<Move>().Reset_Data();
        gameObject.GetComponent<Path>().Reset_List();
        m_NowPlayer.GetComponent<Character>().Chess.Have_Attacked = false;
        m_NowPlayer.GetComponent<Character>().Chess.Have_Moved = false;
        m_NowPlayer.GetComponent<Character>().Chess.Now_State = "Finish";
        Check_Character_Finish();
        //Set_Character_Btn();
        if (_sPlayer_One_Finish == "Start" && _bCheck_Team_Finish == true)
        {
            for (int i = 0; i < _gPlayer1.transform.childCount; i++)
            {
                if (_gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Death")
                {
                    _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State = "Idle";
                    _gPlayer1_UI.transform.GetChild(i).GetComponent<Button>().interactable = true;
                }
            }
            _sPlayer_One_Finish = "End";
            _gPlayer_One_Camera.SetActive(false);
            _gPlayer_Two_Camera.SetActive(true);
            In_And_Out();
        }
        if (_sPlayer_Two_Finish == "Start" && _bCheck_Team_Finish == true)
        {
            for (int i = 0; i < _gPlayer2.transform.childCount; i++)
            {
                if (_gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Death")
                {
                    _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State = "Idle";
                    _gPlayer2_UI.transform.GetChild(i).GetComponent<Button>().interactable = true;
                }

            }
            _sPlayer_Two_Finish = "End";
            _gPlayer_One_Camera.SetActive(true);
            _gPlayer_Two_Camera.SetActive(false);

            In_And_Out();
        }
        Set_Character_Btn();
        
    }


    /// <summary>
    /// 設定腳色移動攻擊等等按鈕狀態
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

        if (m_NowPlayer.GetComponent<Character>().Chess.Have_Attacked == true)
        {
            _bAttack_Btn.interactable = false;
        }
        else
        {
            _bAttack_Btn.interactable = true;
        }

        if (m_NowPlayer.GetComponent<Character>().Chess.Now_State == "Finish")
        {
            _bPlayer_Btn.interactable = false;
        }
        else if (m_NowPlayer.GetComponent<Character>().Chess.Now_State == "Idle")
        {
            _bPlayer_Btn.interactable = true;
        }
    }

    public void Check_Character_Finish()
    {
        _bCheck_Team_Finish = true;
        if (_sPlayer_One_Finish == "Start")
        {
            for (int i = 0; i < _gPlayer1.transform.childCount; i++)
            {
                if (_gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Finish" && _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Death")
                {
                    _bCheck_Team_Finish = false;


                }


            }

        }
        else if (_sPlayer_Two_Finish == "Start")
        {
            for (int i = 0; i < _gPlayer2.transform.childCount; i++)
            {
                if (_gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Finish" && _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Death")
                {
                    _bCheck_Team_Finish = false;
                    Debug.Log("Test");
                }

            }
        }
    }

    /// <summary>
    /// 判斷誰贏了
    /// </summary>
    public void Check_Win()
    {
        _iA_Team_Num = _gPlayer1.transform.childCount;
        _iB_Team_Num = _gPlayer2.transform.childCount;
        for (int i = 0; i < _gPlayer1.transform.childCount; i++)
        {
            if(_gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State=="Death")
            {
                _iA_Team_Num--;
            }
        }

        for (int i = 0; i < _gPlayer2.transform.childCount; i++)
        {
            if (_gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Death")
            {
                _iB_Team_Num--;
            }
        }

    }

    //UI進進出出
    public void In_And_Out()
    {
        _bMove_In_Btn.interactable = false;
        _bMove_Out_Btn.interactable = false;
        if (_gMove_UI.tag == "In")
        {
            _aUI_Anim.SetTrigger("Out");
        }
       else if(_gMove_UI.tag == "Out")
        {
            _aUI_Anim.SetTrigger("In");
        }
    
    }

    public void Transfer(int index)
    {
        switch(index)
        {
            case 0:
                m_NowPlayer.GetComponent<Character>().Set_Job_Data("Warrior") ;
                _gTranfer_UI.SetActive(false);
                break;
            case 1:
                m_NowPlayer.GetComponent<Character>().Set_Job_Data("Archor");
                _gTranfer_UI.SetActive(false);
                break;
            case 2:
                m_NowPlayer.GetComponent<Character>().Set_Job_Data("Magician");
                _gTranfer_UI.SetActive(false);
                break;
        }
    }
}
