﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public GameObject _gPlane; //放棋盤的
    public GameObject _gInit_Grid; //放生成的格子
    public GameObject _gPlayer;      //現在操縱的旗子
    public GameObject _gEnmey;      //放B隊的
    public GameObject _gPartner;    //放A隊的
    public List<Vector3> _lTransfer_A = new List<Vector3>();        //A隊轉職區位置
    public List<Vector3> _lTransfer_B = new List<Vector3>();        //B隊轉職區位置

    public List<Vector3> _lEnmeyPos_List = new List<Vector3>();    //敵人所在的位置List
    public List<Vector3> _lPartnerPos_List = new List<Vector3>();   //自己人所在的位置List
    public List<Vector3> _lCan_Move_List = new List<Vector3>();     //能移動的位置List
    public List<Vector3> _lCan_Attack_List = new List<Vector3>();      //能攻擊的List
    public List<GameObject> _lCan_Attack_Enmey = new List<GameObject>(); //在格子上能攻擊到的人
    public List<int> _lMove_Weight_List = new List<int>();      //格子權重的List
    public List<int> _lAttack_Weight_List = new List<int>();  //攻擊格子的權重List

    public int _iWalk_Steps; //可移動的數量
    public int _iAttack_Distance; //可攻擊的距離
    public int _iMove_List_Index = 0; //存入移動List用的引數
    public int _iMove_List_Count = 0;   //取入移動List內容用的引數
    public int _iAttack_List_Index = 0;   //取入攻擊List內容用的引數
    public int _iAttack_List_Count = 0;   //取入攻擊List內容用的引數


    private bool m_Have_Something; //判斷有沒有東西
    private bool m_Have_Walked;     //判斷這格是不是有走過
    private bool m_Have_Attacked;   //判斷這格是不是搜尋攻擊格子時已找到的
    private bool m_On_Board;        //格子有沒有在棋盤上
    private Vector3 m_Tmp_Pos;  //暫存計算完的下一步

    //private int m_Move_Steps;               //能移動的步數


 

    /// <summary>
    /// 找能移動的位置
    /// </summary>
    public void Find_Move_Way()
    {
        Save_CharacterPos();
        int Tmp_Count = 0;        //用來看是不是第一次走
        _iWalk_Steps = _gPlayer.GetComponent<Character>()._iWalk_Steps;
        //_lCan_Move_List.Add(gameObject.transform.position);
        // for(int i = 0; i<Walk_Steps;i++)
        while (true)
        {

            //第一輪，上下左右
            if (Tmp_Count == 0)
            {
                _lCan_Move_List.Insert(_iMove_List_Index, _gPlayer.transform.position);
                _lMove_Weight_List.Insert(_iMove_List_Index, _iWalk_Steps);
                _iMove_List_Index++;
                //向上尋找
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.z + 1) == _lEnmeyPos_List[i].z) && (_gPlayer.transform.position.x == _lEnmeyPos_List[i].x))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.z + 1) == _lPartnerPos_List[i].z) && (_gPlayer.transform.position.x == _lPartnerPos_List[i].x))
                    {
                        m_Have_Something = true;
                    }
                }
               
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _gPlayer.transform.position + new Vector3(0, 0, 1);
                    Save_Move_Position();
                    _iWalk_Steps = _gPlayer.GetComponent<Character>()._iWalk_Steps;
                }
                m_Have_Something = false;

                //向下尋找
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.z - 1) == _lEnmeyPos_List[i].z) && (_gPlayer.transform.position.x == _lEnmeyPos_List[i].x))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.z - 1) == _lPartnerPos_List[i].z) && (_gPlayer.transform.position.x == _lPartnerPos_List[i].x))
                    {
                        m_Have_Something = true;
                    }
                }
                
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _gPlayer.transform.position + new Vector3(0, 0, -1);
                    Save_Move_Position();
                    _iWalk_Steps = _gPlayer.GetComponent<Character>()._iWalk_Steps;
                }
                m_Have_Something = false;

                //向左尋找
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.x - 1) == _lEnmeyPos_List[i].x) && (_gPlayer.transform.position.z == _lEnmeyPos_List[i].z))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.x - 1) == _lPartnerPos_List[i].x) && (_gPlayer.transform.position.z == _lPartnerPos_List[i].z))
                    {
                        m_Have_Something = true;
                    }
                }
                
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _gPlayer.transform.position + new Vector3(-1, 0, 0);
                    Save_Move_Position();
                    _iWalk_Steps = _gPlayer.GetComponent<Character>()._iWalk_Steps;
                }
                m_Have_Something = false;

                //向右尋找
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.x + 1) == _lEnmeyPos_List[i].x) && (_gPlayer.transform.position.z == _lEnmeyPos_List[i].z))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.x + 1) == _lPartnerPos_List[i].x) && (_gPlayer.transform.position.z == _lPartnerPos_List[i].z))
                    {
                        m_Have_Something = true;
                    }
                }
                
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _gPlayer.transform.position + new Vector3(1, 0, 0);
                    Save_Move_Position();
                    _iWalk_Steps = _gPlayer.GetComponent<Character>()._iWalk_Steps;
                }

                m_Have_Something = false;
                _iMove_List_Count++;             //改變要取出來的List位置
                Tmp_Count++;
            }

            //第二~底，上下左右
            if (Tmp_Count != 0)
            {

                //向上找
                //
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iMove_List_Count].z + 1) == _lEnmeyPos_List[i].z) && (_lCan_Move_List[_iMove_List_Count].x == _lEnmeyPos_List[i].x) && (_lMove_Weight_List[_iMove_List_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iMove_List_Count].z + 1) == _lPartnerPos_List[i].z) && (_lCan_Move_List[_iMove_List_Count].x == _lPartnerPos_List[i].x) && (_lMove_Weight_List[_iMove_List_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                
                if (m_Have_Something == false)
                {

                    m_Tmp_Pos = _lCan_Move_List[_iMove_List_Count] + new Vector3(0, 0, 1);
                    Check_Have_Walked();
                    if (m_Have_Walked == false && _lMove_Weight_List[_iMove_List_Count] > 0)
                    {
                        Save_Move_Position();
                    }
                }
                m_Have_Walked = false;
                m_Have_Something = false;
                //向下找
                //
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iMove_List_Count].z - 1) == _lEnmeyPos_List[i].z) && (_lCan_Move_List[_iMove_List_Count].x == _lEnmeyPos_List[i].x) && (_lMove_Weight_List[_iMove_List_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iMove_List_Count].z - 1) == _lPartnerPos_List[i].z) && (_lCan_Move_List[_iMove_List_Count].x == _lPartnerPos_List[i].x) && (_lMove_Weight_List[_iMove_List_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _lCan_Move_List[_iMove_List_Count] + new Vector3(0, 0, -1);
                    Check_Have_Walked();
                    if (m_Have_Walked == false && _lMove_Weight_List[_iMove_List_Count] > 0)
                    {
                        Save_Move_Position();
                    }
                }
                m_Have_Walked = false;
                m_Have_Something = false;

                //向左找
                //
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iMove_List_Count].x - 1) == _lEnmeyPos_List[i].x) && (_lCan_Move_List[_iMove_List_Count].z == _lEnmeyPos_List[i].z) && (_lMove_Weight_List[_iMove_List_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iMove_List_Count].x - 1) == _lPartnerPos_List[i].x) && (_lCan_Move_List[_iMove_List_Count].z == _lPartnerPos_List[i].z) && (_lMove_Weight_List[_iMove_List_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _lCan_Move_List[_iMove_List_Count] + new Vector3(-1, 0, 0);
                    Check_Have_Walked();
                    if (m_Have_Walked == false && _lMove_Weight_List[_iMove_List_Count] > 0)
                    {
                        Save_Move_Position();
                    }
                }
                m_Have_Walked = false;
                m_Have_Something = false;


                //向右找
                //
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iMove_List_Count].x + 1) == _lEnmeyPos_List[i].x) && (_lCan_Move_List[_iMove_List_Count].z == _lEnmeyPos_List[i].z) && (_lMove_Weight_List[_iMove_List_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iMove_List_Count].x + 1) == _lPartnerPos_List[i].x) && (_lCan_Move_List[_iMove_List_Count].z == _lPartnerPos_List[i].z) && (_lMove_Weight_List[_iMove_List_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _lCan_Move_List[_iMove_List_Count] + new Vector3(1, 0, 0);
                    Check_Have_Walked();
                    if (m_Have_Walked == false && _lMove_Weight_List[_iMove_List_Count] > 0)
                    {
                        Save_Move_Position();
                    }
                }
                m_Have_Walked = false;
                m_Have_Something = false;
                _iMove_List_Count++;             //改變要取出來的List位置

            }
            //如果算完後跳出迴圈
            if (_iMove_List_Count >= _iMove_List_Index)
            {
                // _gPlayer.GetComponent<Move>().Instant();
                break;
            }

        }
        Check_On_Board();


    }


    /// <summary>
    /// 找攻擊的位置
    /// </summary>
    public void Find_Attack_Way()
    {
        int Tmp_Count = 0;
        switch (_gPlayer.GetComponent<Character>().Chess.Job)
        {
            case "Minion":
                Tmp_Count = 0;    //用來看是不是第一次算攻擊距離
                _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;
                //_lCan_Move_List.Add(gameObject.transform.position);
                // for(int i = 0; i<Walk_Steps;i++)
                while (true)
                {

                    //第一輪，上下左右
                    if (Tmp_Count == 0)
                    {
                        //_lCan_Move_List.Insert(_iList_Index, _gPlayer.transform.position);
                        _lAttack_Weight_List.Insert(_iAttack_List_Index, _iAttack_Distance);
                        _lCan_Attack_List.Insert(_iAttack_List_Index, _gPlayer.transform.position);
                        // _lMove_Weight_List.Insert(_iList_Index, _iWalk_Steps);
                        _iAttack_List_Index++;
                        //向上尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(0, 0, 1);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;




                        //向下尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(0, 0, -1);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;



                        //向左尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(-1, 0, 0);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;



                        //向右尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(1, 0, 0);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;



                        _iAttack_List_Count++;             //改變要取出來的List位置
                        Tmp_Count++;
                    }

                    //第二~底，上下左右
                    if (Tmp_Count != 0)
                    {

                        //向上找
                        //




                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(0, 0, 1);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;

                        //向下找
                        //


                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(0, 0, -1);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;


                        //向左找
                        //


                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(-1, 0, 0);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;



                        //向右找
                        //


                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(1, 0, 0);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;

                        _iAttack_List_Count++;             //改變要取出來的List位置

                    }
                    //如果算完後跳出迴圈
                    if (_iAttack_List_Count >= _iAttack_List_Index)
                    {
                        // _gPlayer.GetComponent<Move>().Instant();
                        break;
                    }

                }
                Check_On_Board();
                break;
            case "Preist":
                Tmp_Count = 0;    //用來看是不是第一次算攻擊距離
                _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;
                //_lCan_Move_List.Add(gameObject.transform.position);
                // for(int i = 0; i<Walk_Steps;i++)
                while (true)
                {

                    //第一輪，上下左右
                    if (Tmp_Count == 0)
                    {
                        //_lCan_Move_List.Insert(_iList_Index, _gPlayer.transform.position);
                        _lAttack_Weight_List.Insert(_iAttack_List_Index, _iAttack_Distance);
                        _lCan_Attack_List.Insert(_iAttack_List_Index, _gPlayer.transform.position);
                        // _lMove_Weight_List.Insert(_iList_Index, _iWalk_Steps);
                        _iAttack_List_Index++;
                        //向上尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(0, 0, 1);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;




                        //向下尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(0, 0, -1);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;



                        //向左尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(-1, 0, 0);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;



                        //向右尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(1, 0, 0);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;



                        _iAttack_List_Count++;             //改變要取出來的List位置
                        Tmp_Count++;
                    }

                    //第二~底，上下左右
                    if (Tmp_Count != 0)
                    {

                        //向上找
                        //




                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(0, 0, 1);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;

                        //向下找
                        //


                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(0, 0, -1);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;


                        //向左找
                        //


                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(-1, 0, 0);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;



                        //向右找
                        //


                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(1, 0, 0);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;

                        _iAttack_List_Count++;             //改變要取出來的List位置

                    }
                    //如果算完後跳出迴圈
                    if (_iAttack_List_Count >= _iAttack_List_Index)
                    {
                        // _gPlayer.GetComponent<Move>().Instant();
                        break;
                    }

                }
                Check_On_Board();
                break;
            case "Archer":
                _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;
                m_Tmp_Pos = _gPlayer.transform.position;
                _lCan_Attack_List.Add(m_Tmp_Pos);
                _iAttack_List_Index++;
                _iAttack_List_Count++;
                bool _Have_Enmey = false;
                Save_CharacterPos();
                //向上算
                for (int i= 1; i < _iAttack_Distance+1;i++)
                {
                    if (_gPlayer.tag == "B")
                    {
                        for (int j = 0; j < _lPartnerPos_List.Count; j++)
                        {
                            if (_lPartnerPos_List[j] == new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z + i))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z + i));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                        for (int j = 0; j < _lEnmeyPos_List.Count; j++)
                        {
                          if (_lEnmeyPos_List[j] == new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z + i))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z + i));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }


                    }
                    
                    else if(_gPlayer.tag == "A")
                    {
                        for (int j = 0; j < _lPartnerPos_List.Count; j++)
                        {
                            if (_lPartnerPos_List[j] == new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z + i))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z + i));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                        for (int j = 0; j < _lEnmeyPos_List.Count; j++)
                        {
                            if (_lEnmeyPos_List[j] == new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z + i))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z + i));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                    }
                    
                    if (_Have_Enmey == true)
                    {
                        _Have_Enmey = false;
                        break;
                    }
                    else
                    {
                        _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z + i));
                        _iAttack_List_Index++;
                        _iAttack_List_Count++;
                    }
                 

                }
                //向下
                for (int i = 1; i < _iAttack_Distance+1; i++)
                {
                    if (_gPlayer.tag == "B")
                    {
                        for (int j = 0; j < _lPartnerPos_List.Count; j++)
                        {
                            if (_lPartnerPos_List[j] == new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z - i))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z - i));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                        for (int j = 0; j < _lEnmeyPos_List.Count; j++)
                        {
                            if (_lEnmeyPos_List[j] == new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z - i))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z - i));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }


                    }

                    else if (_gPlayer.tag == "A")
                    {
                        for (int j = 0; j < _lPartnerPos_List.Count; j++)
                        {
                            if (_lPartnerPos_List[j] == new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z - i))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z - i));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                        for (int j = 0; j < _lEnmeyPos_List.Count; j++)
                        {
                            if (_lEnmeyPos_List[j] == new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z - i))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z - i));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                    }
                    if (_Have_Enmey == true)
                    {
                        _Have_Enmey = false;
                        break;
                    }
                    else
                    {
                        _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x, 0, m_Tmp_Pos.z - i));
                        _iAttack_List_Index++;
                        _iAttack_List_Count++;
                    }
                   
                }
                //向左
                for (int i = 1; i < _iAttack_Distance + 1; i++)
                {

                    if (_gPlayer.tag == "B")
                    {
                        for (int j = 0; j < _lPartnerPos_List.Count; j++)
                        {
                            if (_lPartnerPos_List[j] == new Vector3(m_Tmp_Pos.x - i, 0, m_Tmp_Pos.z))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x - i, 0, m_Tmp_Pos.z));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                        for (int j = 0; j < _lEnmeyPos_List.Count; j++)
                        {
                            if (_lEnmeyPos_List[j] == new Vector3(m_Tmp_Pos.x - i, 0, m_Tmp_Pos.z))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x - i, 0, m_Tmp_Pos.z));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }


                    }

                    else if (_gPlayer.tag == "A")
                    {
                        for (int j = 0; j < _lPartnerPos_List.Count; j++)
                        {
                            if (_lPartnerPos_List[j] == new Vector3(m_Tmp_Pos.x - i, 0, m_Tmp_Pos.z))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x - i, 0, m_Tmp_Pos.z));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                        for (int j = 0; j < _lEnmeyPos_List.Count; j++)
                        {
                            if (_lEnmeyPos_List[j] == new Vector3(m_Tmp_Pos.x - i, 0, m_Tmp_Pos.z))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x - i, 0, m_Tmp_Pos.z));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                    }
                    if (_Have_Enmey == true)
                    {
                        _Have_Enmey = false;
                        break;
                    }
                    else
                    {
                        _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x - i, 0, m_Tmp_Pos.z));
                        _iAttack_List_Index++;
                        _iAttack_List_Count++;
                    }
                 
                }
                // 向右
                for (int i = 1; i < _iAttack_Distance + 1; i++)
                {
                    if (_gPlayer.tag == "B")
                    {
                        for (int j = 0; j < _lPartnerPos_List.Count; j++)
                        {
                            if (_lPartnerPos_List[j] == new Vector3(m_Tmp_Pos.x + i, 0, m_Tmp_Pos.z))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x + i, 0, m_Tmp_Pos.z));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                        for (int j = 0; j < _lEnmeyPos_List.Count; j++)
                        {
                            if (_lEnmeyPos_List[j] == new Vector3(m_Tmp_Pos.x + i, 0, m_Tmp_Pos.z))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x + i, 0, m_Tmp_Pos.z));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }


                    }

                    else if (_gPlayer.tag == "A")
                    {
                        for (int j = 0; j < _lPartnerPos_List.Count; j++)
                        {
                            if (_lPartnerPos_List[j] == new Vector3(m_Tmp_Pos.x + i, 0, m_Tmp_Pos.z))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x + i, 0, m_Tmp_Pos.z));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                        for (int j = 0; j < _lEnmeyPos_List.Count; j++)
                        {
                            if (_lEnmeyPos_List[j] == new Vector3(m_Tmp_Pos.x + i, 0, m_Tmp_Pos.z))
                            {
                                _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x + i, 0, m_Tmp_Pos.z));
                                _iAttack_List_Index++;
                                _iAttack_List_Count++;
                                _Have_Enmey = true;
                            }
                        }
                    }

                    if (_Have_Enmey == true)
                    {
                        _Have_Enmey = false;
                        break;
                    }
                    else
                    {
                        _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x + i, 0, m_Tmp_Pos.z));
                        _iAttack_List_Index++;
                        _iAttack_List_Count++;
                    }
                   
                }
                List<Vector3> Archor_Count = new List<Vector3>();


                for (int i = 0; i < _lCan_Attack_List.Count; i++)
                {
                    m_On_Board = false;
                    for (int j = 0; j < _gPlane.transform.childCount; j++)
                    {
                        if (_lCan_Attack_List[i] == _gPlane.transform.GetChild(j).gameObject.transform.position)
                        {
                            m_On_Board = true;
                            Archor_Count.Add(_lCan_Attack_List[i]);

                        }

                    }
                    if (m_On_Board == false)
                    {
                        //_lCan_Attack_List.Remove(_lCan_Attack_List[i]);
                        //_lAttack_Weight_List.Remove(_lAttack_Weight_List[i]);
                        _iAttack_List_Count--;
                        _iAttack_List_Index--;

                    }

                }
                _lCan_Attack_List.Clear();
                _lAttack_Weight_List.Clear();
                for (int i = 0; i < Archor_Count.Count; i++)
                {
                    _lCan_Attack_List.Add(Archor_Count[i]);

                }

                break;
            case "Warrior":
                _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;
                //m_Tmp_Pos = _gPlayer.transform.position;
                m_Tmp_Pos= new Vector3(_gPlayer.transform.position.x + 1, _gPlayer.transform.position.y, _gPlayer.transform.position.z - 1);
                
                //_lCan_Move_List.Add(gameObject.transform.position);
                // for(int i = 0; i<Walk_Steps;i++)
                for (int i = 0; i < _iAttack_Distance; i++)
                {
                    for (int j = 0;j<_iAttack_Distance;j++)
                    {
                        
                        _lCan_Attack_List.Add(new Vector3(m_Tmp_Pos.x - i, 0, m_Tmp_Pos.z + j));
                        _iAttack_List_Index++;
                        _iAttack_List_Count++;
                    }
                    m_Tmp_Pos = new Vector3(_gPlayer.transform.position.x + 1, _gPlayer.transform.position.y, _gPlayer.transform.position.z - 1);
                }

                List<Vector3> Count = new List<Vector3>();
              
               
                for (int i = 0; i < _lCan_Attack_List.Count; i++)
                {
                    m_On_Board = false;
                    for (int j = 0; j < _gPlane.transform.childCount; j++)
                    {
                        if (_lCan_Attack_List[i] == _gPlane.transform.GetChild(j).gameObject.transform.position)
                        {
                            m_On_Board = true;
                            Count.Add(_lCan_Attack_List[i]);
                            
                        }

                    }
                    if (m_On_Board == false)
                    {
                        //_lCan_Attack_List.Remove(_lCan_Attack_List[i]);
                        //_lAttack_Weight_List.Remove(_lAttack_Weight_List[i]);
                        _iAttack_List_Count--;
                        _iAttack_List_Index--;

                    }

                }
                _lCan_Attack_List.Clear();
                _lAttack_Weight_List.Clear();
                for (int i = 0; i < Count.Count; i++)
                {
                    _lCan_Attack_List.Add(Count[i]);
                   
                }

              
                break;
            case "Magician":
                Tmp_Count = 0;        //用來看是不是第一次算攻擊距離
                _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;
                //_lCan_Move_List.Add(gameObject.transform.position);
                // for(int i = 0; i<Walk_Steps;i++)
                while (true)
                {

                    //第一輪，上下左右
                    if (Tmp_Count == 0)
                    {
                        //_lCan_Move_List.Insert(_iList_Index, _gPlayer.transform.position);
                        _lAttack_Weight_List.Insert(_iAttack_List_Index, _iAttack_Distance);
                        _lCan_Attack_List.Insert(_iAttack_List_Index, _gPlayer.transform.position);
                        // _lMove_Weight_List.Insert(_iList_Index, _iWalk_Steps);
                        _iAttack_List_Index++;
                        //向上尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(0, 0, 1);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;




                        //向下尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(0, 0, -1);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;



                        //向左尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(-1, 0, 0);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;



                        //向右尋找


                        m_Tmp_Pos = _gPlayer.transform.position + new Vector3(1, 0, 0);
                        Save_Attack_Position();
                        _iAttack_Distance = _gPlayer.GetComponent<Character>()._iAttack_Distance;



                        _iAttack_List_Count++;             //改變要取出來的List位置
                        Tmp_Count++;
                    }

                    //第二~底，上下左右
                    if (Tmp_Count != 0)
                    {

                        //向上找
                        //




                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(0, 0, 1);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;

                        //向下找
                        //


                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(0, 0, -1);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;


                        //向左找
                        //


                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(-1, 0, 0);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;



                        //向右找
                        //


                        m_Tmp_Pos = _lCan_Attack_List[_iAttack_List_Count] + new Vector3(1, 0, 0);
                        Check_Have_Attacked();
                        if (m_Have_Attacked == false && _lAttack_Weight_List[_iAttack_List_Count] > 0)
                        {
                            Save_Attack_Position();
                        }

                        m_Have_Attacked = false;

                        _iAttack_List_Count++;             //改變要取出來的List位置

                    }
                    //如果算完後跳出迴圈
                    if (_iAttack_List_Count >= _iAttack_List_Index)
                    {
                        // _gPlayer.GetComponent<Move>().Instant();
                        break;
                    }

                }
                Check_On_Board();
                break;
        }
       
    }

    /// <summary>
    /// 確認存的格子有沒有在棋盤上
    /// </summary>
    public void Check_On_Board()
    {
        
        //把不在棋盤上的移動格子移除
        List<Vector3> Tmp_Count = new List<Vector3>();
        List<int> Tmp_Weight = new List<int>();
        for (int i = 0; i < _lCan_Move_List.Count; i++)
        {
            
            //Debug.Log(Tmp_Count[i]);
            m_On_Board = false;
            for (int j = 0; j < _gPlane.transform.childCount; j++)
            {
                
                if (_lCan_Move_List[i] == _gPlane.transform.GetChild(j).transform.position)
                {
                    m_On_Board = true;
                    Tmp_Count.Add(_lCan_Move_List[i]);
                    Tmp_Weight.Add(_lMove_Weight_List[i]);
                    

                    //Debug.Log(_gPlane.transform.GetChild(j).transform.position);
                }

            }
            
            if (m_On_Board == false)
            {
                
                //Debug.Log(_lCan_Move_List[i]);
                //_lCan_Move_List.Remove(_lCan_Move_List[i]);
                //_lMove_Weight_List.Remove(_lMove_Weight_List[i]);
                _iMove_List_Count--;
                _iMove_List_Index--;
                
                
            }
            
        }
        _lCan_Move_List.Clear();
        _lMove_Weight_List.Clear();
        for(int i = 0; i < Tmp_Count.Count;i++)
        {
          
            _lCan_Move_List.Add(Tmp_Count[i]);
            _lMove_Weight_List.Add(Tmp_Weight[i]);
        }


        //把不在棋盤上的攻擊格子移除陣列
        Tmp_Count.Clear();
        Tmp_Weight.Clear();
        for (int i = 0; i < _lCan_Attack_List.Count; i++)
        {
            m_On_Board = false;
            for (int j = 0; j < _gPlane.transform.childCount; j++)
            {
                if (_lCan_Attack_List[i] == _gPlane.transform.GetChild(j).gameObject.transform.position)
                {
                    m_On_Board = true;
                    Tmp_Count.Add(_lCan_Attack_List[i]);
                    Tmp_Weight.Add(_lAttack_Weight_List[i]);
                }

            }
            if (m_On_Board == false)
            {
                //_lCan_Attack_List.Remove(_lCan_Attack_List[i]);
                //_lAttack_Weight_List.Remove(_lAttack_Weight_List[i]);
                _iAttack_List_Count--;
                _iAttack_List_Index--;

            }

        }
        _lCan_Attack_List.Clear();
        _lAttack_Weight_List.Clear();
        for (int i = 0; i < Tmp_Count.Count; i++)
        {
            _lCan_Attack_List.Add(Tmp_Count[i]);
            _lAttack_Weight_List.Add(Tmp_Weight[i]);
        }
        //Tmp_Count.Clear();
        //if (m_Tmp_Pos == _gPlane.transform.GetChild(i).gameObject.transform.position)
        //{
        //    m_On_Board = true;
        //    break;
        //}
        //else
        //{
        //    m_On_Board = false;
        //}

    }
    /// <summary>
    /// 判斷這一格是否有走過已存在List
    /// </summary>
    public void Check_Have_Walked()
    {
        for (int i = 0; i < _iMove_List_Index; i++)
        {
            if (m_Tmp_Pos == _lCan_Move_List[i])
            {
                m_Have_Walked = true;
                break;
            }
        }
    }


    /// <summary>
    /// 判斷是否有在可以攻擊的List裡面
    /// </summary>
    public void Check_Have_Attacked()
    {
        for (int i = 0; i < _iAttack_List_Index; i++)
        {
            if (m_Tmp_Pos == _lCan_Attack_List[i])
            {
                m_Have_Attacked = true;
                break;
            }
        }
    }

    /// <summary>
    /// 存找到的位置進去陣列
    /// </summary>
    public void Save_Move_Position()
    {
        // Check_On_Board();
        _iWalk_Steps = _lMove_Weight_List[_iMove_List_Count] - 1;        //行動數-1
        _lMove_Weight_List.Insert(_iMove_List_Index, _iWalk_Steps);      //存格子的權重進去List
        _lCan_Move_List.Insert(_iMove_List_Index, m_Tmp_Pos);            //把計算的下一步存進List裡
        _iMove_List_Index++; //存陣列的引數+1



    }


    /// <summary>
    /// 存找到的攻擊格子進去陣列
    /// </summary>
    public void Save_Attack_Position()
    {
        //Check_On_Board();
        _iAttack_Distance = _lAttack_Weight_List[_iAttack_List_Count] - 1;
        _lAttack_Weight_List.Insert(_iAttack_List_Index, _iAttack_Distance);
        _lCan_Attack_List.Insert(_iAttack_List_Index, m_Tmp_Pos);
        _iAttack_List_Index++;

    }



    //存取場上所有角色的位置
    public void Save_CharacterPos()
    {
        for (int i = 0; i < _gEnmey.transform.childCount; i++)
        {
            _lEnmeyPos_List.Add(_gEnmey.transform.GetChild(i).position);
        }
        for (int i = 0; i < _gPartner.transform.childCount; i++)
        {
            _lPartnerPos_List.Add(_gPartner.transform.GetChild(i).position);
        }

    }

    /// <summary>
    /// 重製List
    /// </summary>
    public void Reset_List()
    {
        _lCan_Move_List.Clear();
        _lMove_Weight_List.Clear();
        _iMove_List_Count = 0;
        _iMove_List_Index = 0;
        _iAttack_List_Count = 0;
        _iAttack_List_Index = 0;
        _lPartnerPos_List.Clear();
        _lEnmeyPos_List.Clear();
        _lCan_Attack_List.Clear();
        _lAttack_Weight_List.Clear();

    }


}
