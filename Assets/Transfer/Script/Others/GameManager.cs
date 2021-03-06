﻿using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    public Animator _aCamera_Anim;  //相機的動畫
    public Animator _aScene_Light; //場景的燈光
    public Animator _aBattle_Scene_Anim; //切換去戰鬥場景的動畫

    public Animator _aA_Battle_Anim;  //A戰鬥角色的動畫
    public Animator _aB_Battle_Anim;  //B戰鬥角色的動畫

    public GameObject[] _gA_Team_Model = new GameObject[5];   //A隊模型
    public GameObject[] _gB_Team_Model = new GameObject[5];     //B隊模型


    public GameObject _gA_Battle_Pos;   //打的位置
    public GameObject _gB_Battle_Pos;   //被打的位置
    public GameObject _gBattle_Camera; //戰鬥的相機
    public GameObject _gNormal_Camera; //普通的相機

    public Material _mSky_Box;

    public GameObject[] _gNow_State_UI;         //現在的狀態UI
    public GameObject[] _gTransfer_Obj;         //轉職的格子
    public GameObject _A_Model;
    public GameObject _B_Model;


    public GameObject _gPlayer1;            //A team腳色父物件
    public GameObject _gPlayer2;            //B team腳色父物件
    public GameObject _gMove_UI;            //動UI的
    public GameObject _gMove_Camera;        //動Camera的
    public GameObject m_NowPlayer;             //現在遊玩的腳色

    public GameObject _gWhole_UI;           //最上層的UI

    public GameObject _gTranfer_UI;         //轉職的UI


    public GameObject _gPlayer1_UI;         //A team 整個UI物件
    public GameObject _gPlayer2_UI;         //B team 整個UI物件
    public GameObject _gNow_Player_UI;      //Now Player 整個UI物件
    public GameObject _gNow_Player_Function_UI; //Now Player 功能UI物件

    public GameObject _gStateName;

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
    public bool _bCamera_Move;  //相機是否在轉動
    public bool _bGameState_Check;  //確認GameState結束
    public bool _bGameOver_Check;  //確認GameState結束


    public Image _sCamera_Sprite;
    public Sprite _sCamera_ASprite;
    public Sprite _sCamera_BSprite;
    public Path path;

    private float Sky_Time;

    public Material[] _mAll_Mat;
    public Material[] _mAll_Stone_Mat;

    public UnityEvent _eSound;
    // Start is called before the first frame update
    void Start()
    {
        _iPlayer1_Transfer_Area_Count = 5;
        _iPlayer2_Transfer_Area_Count = 5;
        for (int i = 0; i < _mAll_Mat.Length; i++)
        {
            _mAll_Mat[i].SetFloat("_AlphaScale", 1.0f);
            _mAll_Mat[i].SetFloat("_DissolveCutoff", 0.0f);
            _mAll_Mat[i].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f));
            _mAll_Mat[i].SetColor("_RimColor", new Color(0.0f, 0.0f, 0.0f));
        }
        for (int i = 0; i < _mAll_Stone_Mat.Length; i++)
        {

            _mAll_Stone_Mat[i].SetFloat("_DissolveCutoff", 0.0f);
            _mAll_Stone_Mat[i].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f));
            _mAll_Stone_Mat[i].SetColor("_RimColor", new Color(0.0f, 0.0f, 0.0f));

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Sky_Time >= 360)
        {
            Sky_Time = 0;
        }

        m_GameStateManager.GameStateUpdate();
        if (_sScene_Transfer_End == "Start")
        {

            m_GameStateManager.Set_GameState(new SetArea_One(m_GameStateManager));

            Set_Now_Team();
            _sScene_Transfer_End = "End";
            _sSet_Area_Finish_One = "Start";
            _sGame_Start = "Start";
        }
        if (_bGameOver_Check == false)
        {
            Check_Win();






            //相機動畫控制
            Move_Camera_Control();

        }





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
            for (int i = 0; i < _gPlayer1.transform.childCount; i++)
            {
                if (_gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Death")
                {
                    _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State = "Idle";
                    if (i != 3)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            _gPlayer1_UI.transform.GetChild(i).transform.GetChild(j).GetComponent<Button>().interactable = true;

                        }
                    }
                    else
                    {
                        _gPlayer1_UI.transform.GetChild(i).transform.GetChild(4).GetComponent<Button>().interactable = true;
                    }

                }

            }
            for (int i = 0; i < _gPlayer2.transform.childCount; i++)
            {
                if (_gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Defense" && _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Death")
                {
                    _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State = "Idle";
                    if (i != 3)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            _gPlayer2_UI.transform.GetChild(i).transform.GetChild(j).GetComponent<Button>().interactable = true;

                        }
                    }
                    else
                    {
                        _gPlayer2_UI.transform.GetChild(i).transform.GetChild(4).GetComponent<Button>().interactable = true;
                    }
                }

            }
            Set_Mat();
        }
        else if (_sPlayer_Two_Finish == "Start")
        {
            _gPlayer1_UI.SetActive(false);
            _gPlayer2_UI.SetActive(true);
            for (int i = 0; i < _gPlayer2.transform.childCount; i++)
            {
                if (_gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Death")
                {
                    _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State = "Idle";
                    if (i != 3)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            _gPlayer2_UI.transform.GetChild(i).transform.GetChild(j).GetComponent<Button>().interactable = true;

                        }
                    }
                    else
                    {
                        _gPlayer2_UI.transform.GetChild(i).transform.GetChild(4).GetComponent<Button>().interactable = true;
                    }
                }

            }
            for (int i = 0; i < _gPlayer1.transform.childCount; i++)
            {
                if (_gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Defense" && _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Death")
                {
                    _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State = "Idle";
                    if (i != 3)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            _gPlayer1_UI.transform.GetChild(i).transform.GetChild(j).GetComponent<Button>().interactable = true;

                        }
                    }
                    else
                    {
                        _gPlayer1_UI.transform.GetChild(i).transform.GetChild(4).GetComponent<Button>().interactable = true;
                    }
                }

            }
            Set_Mat();
        }
    }
    /// <summary>
    /// 選擇腳色
    /// </summary>
    /// <param name="index"></param>
    public void Select_Player(int index)
    {

        if (_gNow_Player_Function_UI != null && _gWhole_UI.activeSelf == true)
        {
            _gNow_Player_Function_UI.SetActive(false);
            if (m_NowPlayer.GetComponent<Character>().Chess.Now_State != "Death" && m_NowPlayer.GetComponent<Character>().Chess.Now_State != "Defense" && m_NowPlayer.GetComponent<Character>().Chess.Now_State != "Finish")
            {
                m_NowPlayer.GetComponent<Character>().Chess.Now_State = "Idle";
            }

            Set_Character_Btn();
            m_NowPlayer.GetComponent<Move>().Reset_Data();
            m_NowPlayer.GetComponent<Character>()._aChess_Anime.SetBool("CancelAtk", true);
            m_NowPlayer.GetComponent<Character>()._aChess_Anime.SetBool("PreAtk", false);


        }


        if (GameManager._sPlayer_One_Finish == "Start" && _gWhole_UI.activeSelf == true)
        {
            m_NowPlayer = _gPlayer1.transform.GetChild(index).gameObject;
            gameObject.GetComponent<Path>().Reset_List();
            if (m_NowPlayer.GetComponent<Character>().Chess.Now_State != "Finish" && m_NowPlayer.GetComponent<Move>()._bIs_Moving == false && m_NowPlayer.GetComponent<Character>().Chess.Now_State != "Defense")
            {

                _gNow_Player_UI = _gPlayer1_UI.transform.GetChild(index).gameObject;
                if (m_NowPlayer.GetComponent<Character>().Chess.Job == "Preist")
                {
                    _gNow_Player_Function_UI = _gNow_Player_UI.transform.GetChild(5).gameObject;

                }
                else
                {
                    _gNow_Player_Function_UI = _gNow_Player_UI.transform.GetChild(4).gameObject;

                }
                _gNow_Player_UI.SetActive(true);
                _gNow_Player_Function_UI.SetActive(true);
                gameObject.GetComponent<Path>()._gPlayer = _gPlayer1.transform.GetChild(index).gameObject;
                m_NowPlayer = _gPlayer1.transform.GetChild(index).gameObject;

                _bMove_Btn = _gNow_Player_Function_UI.transform.GetChild(0).GetComponent<Button>();
                _bAttack_Btn = _gNow_Player_Function_UI.transform.GetChild(1).GetComponent<Button>();
                _bPlayer_Btn = _gNow_Player_UI.transform.GetChild(m_NowPlayer.GetComponent<Character>()._iNow_Class_Count).GetComponent<Button>();
                m_NowPlayer.GetComponent<Character>()._gPlayer_UI = _gNow_Player_UI;
                m_NowPlayer.GetComponent<Character>().Chess.Now_State = "Active";




            }





        }
        else if (GameManager._sPlayer_Two_Finish == "Start" && _gWhole_UI.activeSelf == true)
        {
            m_NowPlayer = _gPlayer2.transform.GetChild(index).gameObject;
            gameObject.GetComponent<Path>().Reset_List();
            if (m_NowPlayer.GetComponent<Character>().Chess.Now_State != "Finish" && m_NowPlayer.GetComponent<Move>()._bIs_Moving == false && m_NowPlayer.GetComponent<Character>().Chess.Now_State != "Defense")
            {

                _gNow_Player_UI = _gPlayer2_UI.transform.GetChild(index).gameObject;
                if (m_NowPlayer.GetComponent<Character>().Chess.Job == "Preist")
                {
                    _gNow_Player_Function_UI = _gNow_Player_UI.transform.GetChild(5).gameObject;

                }
                else
                {
                    _gNow_Player_Function_UI = _gNow_Player_UI.transform.GetChild(4).gameObject;

                }
                _gNow_Player_UI.SetActive(true);
                _gNow_Player_Function_UI.SetActive(true);
                gameObject.GetComponent<Path>()._gPlayer = _gPlayer2.transform.GetChild(index).gameObject;
                m_NowPlayer = _gPlayer2.transform.GetChild(index).gameObject;

                _bMove_Btn = _gNow_Player_Function_UI.transform.GetChild(0).GetComponent<Button>();
                _bAttack_Btn = _gNow_Player_Function_UI.transform.GetChild(1).GetComponent<Button>();
                _bPlayer_Btn = _gNow_Player_UI.transform.GetChild(m_NowPlayer.GetComponent<Character>()._iNow_Class_Count).GetComponent<Button>();
                m_NowPlayer.GetComponent<Character>()._gPlayer_UI = _gNow_Player_UI;
                m_NowPlayer.GetComponent<Character>().Chess.Now_State = "Active";



            }


        }
        Set_Mat();
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
        m_NowPlayer.GetComponent<Character>().Chess.Now_State = "Ready_Move";
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
        m_NowPlayer.GetComponent<Character>().Chess.Now_State = "Ready_Attack";
        _gNow_Player_Function_UI.SetActive(false);
        Set_Character_Btn();
        In_And_Out();
        Attack_Btn.interactable = false;

        m_NowPlayer.GetComponent<Character>()._aChess_Anime.SetBool("CancelAtk", false);
        m_NowPlayer.GetComponent<Character>()._aChess_Anime.SetBool("PreAtk", true);
        //m_NowPlayer.GetComponent<Character>()._aChess_Anime.sett
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
        if (m_NowPlayer.GetComponent<Character>().Chess.Now_State != "Defense")
        {
            m_NowPlayer.GetComponent<Character>().Chess.Now_State = "Finish";
        }



        Set_Character_Btn();
        Set_Mat();
    }



    public void Defense_Btn()
    {
        m_NowPlayer.GetComponent<Character>().Chess.Now_State = "Defense";
        End_Btn_Click();
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

        if (m_NowPlayer.GetComponent<Character>().Chess.Now_State == "Finish" || m_NowPlayer.GetComponent<Character>().Chess.Now_State == "Defense")
        {
            _bPlayer_Btn.interactable = false;


        }
        else if (m_NowPlayer.GetComponent<Character>().Chess.Now_State == "Idle")
        {
            _bPlayer_Btn.interactable = true;

        }
        Set_Mat();
    }

    /// <summary>
    /// 設定腳色身上材質
    /// </summary>
    public void Set_Mat()
    {
        for (int i = 0; i < _gPlayer1.transform.childCount; i++)
        {

            if (_gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Finish")
            {

                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mMat.SetFloat("_AlphaScale", 0.8f);
                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_RimColor", new Color(0, 0, 0));
                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_Color", new Color(0, 0, 0));

                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f));
                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_RimColor", new Color(0.0f, 0.0f, 0.0f));


            }
            else if (_gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Defense")
            {
                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_RimColor", new Color(0.4f, 0.3f, 0));
                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_RimColor", new Color(0.4f, 0.3f, 0));

            }

            else if ((_gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Active" || _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Ready_Attack" || _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Ready_Move") && _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Finish")
            {

                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_RimColor", new Color(0.8f, 0.2f, 0.2f));
                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_RimColor", new Color(0.8f, 0.2f, 0.2f));

            }

            else if (_gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Defense" || _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Finish" || _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Active")
            {

                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mMat.SetFloat("_AlphaScale", 1.0f);
                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_RimColor", new Color(0, 0, 0));
                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_Color", new Color(1, 1, 1));

                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_Color", new Color(1, 1, 1));
                _gPlayer1.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_RimColor", new Color(0, 0, 0));

            }
        }
        for (int i = 0; i < _gPlayer2.transform.childCount; i++)
        {

            if (_gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Finish")
            {

                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mMat.SetFloat("_AlphaScale", 0.8f);
                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_RimColor", new Color(0, 0, 0));
                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_Color", new Color(0, 0, 0));

                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f));
                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_RimColor", new Color(0.0f, 0.0f, 0.0f));


            }
            else if (_gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Defense")
            {
                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_RimColor", new Color(0.4f, 0.3f, 0));
                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_RimColor", new Color(0.4f, 0.3f, 0));

            }
            else if ((_gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Active" || _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Ready_Attack" || _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Ready_Move") && _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Finish")
            {

                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_RimColor", new Color(0, 0.1725f, 0.7254f));
                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_RimColor", new Color(0, 0.1725f, 0.7254f));

            }


            else if (_gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Defense" || _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Finish" || _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Now_State != "Active")
            {

                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mMat.SetFloat("_AlphaScale", 1.0f);
                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_RimColor", new Color(0, 0, 0));
                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mMat.SetColor("_Color", new Color(1, 1, 1));

                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_Color", new Color(1, 1, 1));
                _gPlayer2.transform.GetChild(i).GetComponent<Character>()._mStone_Mat.SetColor("_RimColor", new Color(0, 0, 0));

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
            if (_gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Now_State == "Death")
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
        if (_iA_Team_Num == 1 && _gPlayer1.transform.GetChild(3).GetComponent<Character>().Chess.Now_State != "Death")
        {
            _gWin_Menu.SetActive(true);
            _gBWin_Image.SetActive(true);
            gameObject.GetComponent<AudioSource>().clip = gameObject.GetComponent<Music>()._aVictory_Bgm;
            gameObject.GetComponent<AudioSource>().loop = false;
            gameObject.GetComponent<AudioSource>().Play();

            _bGameOver_Check = true;

        }
        else if (_iA_Team_Num == 0)
        {
            _gWin_Menu.SetActive(true);
            _gBWin_Image.SetActive(true);
            gameObject.GetComponent<AudioSource>().clip = gameObject.GetComponent<Music>()._aVictory_Bgm;
            gameObject.GetComponent<AudioSource>().loop = false;
            gameObject.GetComponent<AudioSource>().Play();

            _bGameOver_Check = true;
        }
        if (_iB_Team_Num == 1 && _gPlayer2.transform.GetChild(3).GetComponent<Character>().Chess.Now_State != "Death")
        {
            _gWin_Menu.SetActive(true);
            _gAWin_Image.SetActive(true);
            gameObject.GetComponent<AudioSource>().clip = gameObject.GetComponent<Music>()._aVictory_Bgm;
            gameObject.GetComponent<AudioSource>().loop = false;
            gameObject.GetComponent<AudioSource>().Play();

            _bGameOver_Check = true;
        }
        else if (_iB_Team_Num == 0)
        {
            _gWin_Menu.SetActive(true);
            _gAWin_Image.SetActive(true);
            gameObject.GetComponent<AudioSource>().clip = gameObject.GetComponent<Music>()._aVictory_Bgm;
            gameObject.GetComponent<AudioSource>().loop = false;
            gameObject.GetComponent<AudioSource>().Play();

            _bGameOver_Check = true;
        }

    }

    /// <summary>
    /// UI進進出出
    /// </summary>
    public void In_And_Out()
    {

    }
    public void Move_UI_Control()
    {
        AnimatorStateInfo UI_info = _aUI_Anim.GetCurrentAnimatorStateInfo(0);
        if (UI_info.normalizedTime >= 0.3f && UI_info.IsName("Move_In") && _gMove_UI.tag == "Out")
        {
            _bMove_In_Btn.gameObject.SetActive(false);
            _bMove_Out_Btn.gameObject.SetActive(true);
            _bMove_Out_Btn.interactable = true;
            _gMove_UI.tag = "In";
        }
        else if (UI_info.normalizedTime >= 0.3f && UI_info.IsName("Move_Out") && _gMove_UI.tag == "In")
        {
            _bMove_In_Btn.gameObject.SetActive(true);
            _bMove_Out_Btn.gameObject.SetActive(false);
            _bMove_In_Btn.interactable = true;
            _gMove_UI.tag = "Out";
        }
    }



    public void GameState_Control()
    {
        AnimatorStateInfo GameState_info = _gStateName.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (GameState_info.normalizedTime >= 2.0f && GameState_info.IsName("GameState") && _bGameState_Check == true)
        {

        }

    }


    /// <summary>
    /// 相機動畫
    /// </summary>
    public void Camera_Move_Anim()
    {
        _bCamera_Move = true;
        for (int i = 0; i < _gPlayer1.transform.childCount; i++)
        {
            _gPlayer1.transform.GetChild(i).GetComponent<Character>()._g3D_UI.SetActive(false);
            _gPlayer2.transform.GetChild(i).GetComponent<Character>()._g3D_UI.SetActive(false);
        }
        if (_gMove_Camera.tag == "A")
        {
            _gWhole_UI.SetActive(false);
            _sCamera_Sprite.sprite = _sCamera_BSprite;
            _aCamera_Anim.SetTrigger("TeamB");
            _aScene_Light.SetTrigger("B");
        }
        else if (_gMove_Camera.tag == "B")
        {
            _gWhole_UI.SetActive(false);
            _sCamera_Sprite.sprite = _sCamera_ASprite;
            _aCamera_Anim.SetTrigger("TeamA");


            _aScene_Light.SetTrigger("A");
        }
    }
    /// <summary>
    /// 相機動畫控制
    /// </summary>
    public void Move_Camera_Control()
    {
        AnimatorStateInfo info = _aCamera_Anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1.0f && info.IsName("TeamA") && _gMove_Camera.tag == "B")
        {
            if (_sSet_Area_Finish_Two == "End")
            {
                _gWhole_UI.SetActive(true);
                for (int i = 0; i < _gPlayer1.transform.childCount; i++)
                {
                    _gPlayer1.transform.GetChild(i).GetComponent<Character>()._g3D_UI.SetActive(true);
                    _gPlayer2.transform.GetChild(i).GetComponent<Character>()._g3D_UI.SetActive(true);
                }

            }
            if (_sSet_Area_Finish_One == "End" && _sSet_Area_Finish_Two == "End")
            {
                _sSet_Area_Finish_One = "";

                _gStateName.GetComponent<Animator>().SetTrigger("Restart");
            }


            _bCamera_Move = false;
            _gMove_Camera.tag = "A";
            if (_bCheck_Team_Finish == true)
            {


                _gStateName.GetComponent<Animator>().SetTrigger("Restart");
                _bCheck_Team_Finish = false;
            }

        }
        else if (info.normalizedTime >= 1.0f && info.IsName("TeamB") && _gMove_Camera.tag == "A")
        {

            if (_sSet_Area_Finish_Two == "End")
            {
                _gWhole_UI.SetActive(true);
                for (int i = 0; i < _gPlayer1.transform.childCount; i++)
                {
                    _gPlayer1.transform.GetChild(i).GetComponent<Character>()._g3D_UI.SetActive(true);
                    _gPlayer2.transform.GetChild(i).GetComponent<Character>()._g3D_UI.SetActive(true);
                }

            }
            if (_sSet_Area_Finish_One == "End" && _sSet_Area_Finish_Two == "Start")
            {


                _gStateName.GetComponent<Animator>().SetTrigger("Restart");
            }

            if (_bCheck_Team_Finish == true)
            {

                _gStateName.GetComponent<Animator>().SetTrigger("Restart");
                _bCheck_Team_Finish = false;
            }
            _bCamera_Move = false;
            _gMove_Camera.tag = "B";


        }
    }
    /// <summary>
    /// 切換到戰鬥場景的動畫
    /// </summary>
    public void Battle_Trans_Control()
    {

        AnimatorStateInfo info = _aBattle_Scene_Anim.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= 0.5f && info.IsName("Go") && _gBattle_Camera.tag == "Out")
        {

            if (m_NowPlayer.tag == "A")
            {
                _A_Model = Instantiate(_gA_Team_Model[m_NowPlayer.GetComponent<Character>()._iNow_Class_Count], _gA_Battle_Pos.transform.position, _gA_Battle_Pos.transform.rotation, _gA_Battle_Pos.transform);

            }
            else if (m_NowPlayer.tag == "B")
            {
                _A_Model = Instantiate(_gB_Team_Model[m_NowPlayer.GetComponent<Character>()._iNow_Class_Count], _gA_Battle_Pos.transform.position, _gA_Battle_Pos.transform.rotation, _gA_Battle_Pos.transform);

            }


            _A_Model.SetActive(true);
            _aA_Battle_Anim = _A_Model.GetComponent<Animator>();
            _gBattle_Camera.SetActive(true);
            _aA_Battle_Anim.SetTrigger("Atk");

            _gBattle_Camera.tag = "In";
        }
        else if (info.normalizedTime >= 0.5f && info.IsName("Back") && _gBattle_Camera.tag == "In")
        {

            _gNormal_Camera.SetActive(true);
            _gMove_Camera.transform.parent.GetComponent<CameraShake>().shakeDuration = 0.8f;

            _gBattle_Camera.tag = "Out";
            _gWhole_UI.SetActive(true);


        }
    }

    /// <summary>
    /// 攻擊腳色的動畫
    /// </summary>
    public void Attack_Anime()
    {
        if (_gBattle_Camera.tag == "In" && _A_Model != null)
        {

            AnimatorStateInfo info = _aA_Battle_Anim.GetCurrentAnimatorStateInfo(0);


            if (_A_Model != null && info.normalizedTime >= 1.3f && info.IsName("Attack"))
            {

                Destroy(_A_Model);
                for (int i = 0; i < _gB_Battle_Pos.transform.childCount; i++)
                {
                    Destroy(_gB_Battle_Pos.transform.GetChild(i).gameObject);
                }

                _gBattle_Camera.SetActive(false);


            }

        }

    }
    /// <summary>
    /// 轉職的按鈕
    /// </summary>
    /// <param name="index"></param>
    public void Transfer(int index)
    {
        switch (index)
        {
            case 0:
                Instantiate(m_NowPlayer.GetComponent<Character>()._gTransfer_Effect, m_NowPlayer.transform.position, m_NowPlayer.GetComponent<Character>()._gTransfer_Effect.transform.rotation);
                m_NowPlayer.GetComponent<Character>().Set_Job_Data("Warrior");
                _gTranfer_UI.SetActive(false);
                gameObject.GetComponent<Music>()._eTransfer_Sound.Invoke();
                break;
            case 1:
                Instantiate(m_NowPlayer.GetComponent<Character>()._gTransfer_Effect, m_NowPlayer.transform.position, m_NowPlayer.GetComponent<Character>()._gTransfer_Effect.transform.rotation);
                m_NowPlayer.GetComponent<Character>().Set_Job_Data("Archer");
                gameObject.GetComponent<Music>()._eTransfer_Sound.Invoke();
                _gTranfer_UI.SetActive(false);
                break;
            case 2:
                Instantiate(m_NowPlayer.GetComponent<Character>()._gTransfer_Effect, m_NowPlayer.transform.position, m_NowPlayer.GetComponent<Character>()._gTransfer_Effect.transform.rotation);
                m_NowPlayer.GetComponent<Character>().Set_Job_Data("Magician");
                gameObject.GetComponent<Music>()._eTransfer_Sound.Invoke();
                _gTranfer_UI.SetActive(false);
                break;
            case 99:
                _gTranfer_UI.SetActive(false);
                break;

        }
    }

    /// <summary>
    /// 強制結束這回合
    /// </summary>
    public void Finish_Turn()
    {
        if (_gNow_Player_Function_UI != null)
        {
            _gNow_Player_Function_UI.SetActive(false);


        }

        _bCheck_Team_Finish = true;


        for (int i = 0; i < _gPlayer1.transform.childCount; i++)
        {
            _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Have_Attacked = false;
            _gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.Have_Moved = false;

            _gNow_Player_UI = _gPlayer1_UI.transform.GetChild(i).gameObject;
            if (i == 3)
            {
                _gNow_Player_Function_UI = _gNow_Player_UI.transform.GetChild(5).gameObject;

            }
            else
            {
                _gNow_Player_Function_UI = _gNow_Player_UI.transform.GetChild(4).gameObject;

            }

            m_NowPlayer = _gPlayer1.transform.GetChild(i).gameObject;
            m_NowPlayer.GetComponent<Move>().Reset_Data();
            _bMove_Btn = _gNow_Player_Function_UI.transform.GetChild(0).GetComponent<Button>();
            _bAttack_Btn = _gNow_Player_Function_UI.transform.GetChild(1).GetComponent<Button>();
            _bPlayer_Btn = _gNow_Player_UI.transform.GetChild(m_NowPlayer.GetComponent<Character>()._iNow_Class_Count).GetComponent<Button>();
            Set_Character_Btn();
            _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Have_Attacked = false;
            _gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.Have_Moved = false;

            _gNow_Player_UI = _gPlayer2_UI.transform.GetChild(i).gameObject;
            if (i == 3)
            {
                _gNow_Player_Function_UI = _gNow_Player_UI.transform.GetChild(5).gameObject;

            }
            else
            {
                _gNow_Player_Function_UI = _gNow_Player_UI.transform.GetChild(4).gameObject;

            }
            m_NowPlayer = _gPlayer2.transform.GetChild(i).gameObject;
            m_NowPlayer.GetComponent<Move>().Reset_Data();
            _bMove_Btn = _gNow_Player_Function_UI.transform.GetChild(0).GetComponent<Button>();
            _bAttack_Btn = _gNow_Player_Function_UI.transform.GetChild(1).GetComponent<Button>();
            _bPlayer_Btn = _gNow_Player_UI.transform.GetChild(m_NowPlayer.GetComponent<Character>()._iNow_Class_Count).GetComponent<Button>();
            m_NowPlayer = _gPlayer2.transform.GetChild(i).gameObject;
            m_NowPlayer.GetComponent<Move>().Reset_Data();
            if (m_NowPlayer.GetComponent<Character>().Chess.Job != "Preist")
            {
                m_NowPlayer.GetComponent<Character>()._aChess_Anime.SetBool("CancelAtk", true);
                m_NowPlayer.GetComponent<Character>()._aChess_Anime.SetBool("PreAtk", false);
            }

            Set_Character_Btn();

        }

        gameObject.GetComponent<Path>().Reset_List();


        if (_sPlayer_One_Finish == "Start" && _bCheck_Team_Finish == true)
        {

            _sPlayer_One_Finish = "End";
            In_And_Out();
            if (_gMove_Camera.tag == "A")
            {
                Camera_Move_Anim();
            }
            else
            {
                _bCheck_Team_Finish = false;
                _gStateName.GetComponent<Animator>().SetTrigger("Restart");
            }



        }
        if (_sPlayer_Two_Finish == "Start" && _bCheck_Team_Finish == true)
        {

            _sPlayer_Two_Finish = "End";
            In_And_Out();
            if (_gMove_Camera.tag == "B")
            {
                Camera_Move_Anim();
            }
            else
            {
                _bCheck_Team_Finish = false;


                _gStateName.GetComponent<Animator>().SetTrigger("Restart");
            }




        }
        Set_Mat();


    }

}
