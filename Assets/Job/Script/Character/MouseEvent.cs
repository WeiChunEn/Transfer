using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEvent : MonoBehaviour
{
    private Color _cTmp_Grid_Color; //原本的格子顏色
    private Color _cTmp_Area_Color; //原本的區域顏色
    public Color _cTmp_Atk_Grid_Color; //原本的攻擊區域顏色
    public Vector3 _vDestination; //格子的位置
    public GameObject _gGameManager; //管理器
    public GameObject _gNow_Player; //現在控制的旗子
    public List<GameObject> _gEnmey_List; //攻擊的敵人
    public bool _bOn_Move_It;            //是否有在移動的格子上面
    public bool _bOn_Set_it;        //是否有要設定的格子上面
    public bool _bOn_Tranf_it;      //在轉職區域上面
    public bool _bOn_Attack_It;         //是否在攻擊的格子上面
    public bool _bOn_Player_It;        //在棋子上面
    private int m_Player_Index;
    public Path path;


    private void Awake()
    {
        _gGameManager = GameObject.Find("GameManager");
        path = _gGameManager.GetComponent<Path>();
    }
    // Start is called before the first frame update
    void Start()
    {
        switch (tag)
        {
            case "Area":
                _cTmp_Area_Color = GetComponent<Renderer>().material.color;
                break;
            case "Grid":
                _cTmp_Grid_Color = GetComponent<Renderer>().material.color;
                break;
            case "Atk_Grid":
                _cTmp_Atk_Grid_Color = GetComponent<Renderer>().material.color;
                break;
        }



        _vDestination = transform.position;
        

    }

    // Update is called once per frame
    void Update()
    {
        _gNow_Player = _gGameManager.GetComponent<GameManager>().m_NowPlayer;
        //如果在按左鍵的事件
        if (Input.GetButtonDown("Fire1"))
        {

            //移動按鈕滑鼠事件
            if (_bOn_Move_It == true && (GameManager._sPlayer_One_Finish == "Start" || GameManager._sPlayer_Two_Finish == "Start") && tag == "Grid")
            {
                _gNow_Player.GetComponent<Move>().Cal_Road();
                _gNow_Player.GetComponent<Character>().Chess.Now_State = "Have_Moved";
                _gNow_Player.GetComponent<Character>().Chess.Have_Moved = true;
                //_gGameManager.GetComponent<GameManager>()._gMove_UI.SetActive(false);
                if (_gNow_Player.tag == "A")
                {
                    _gGameManager.GetComponent<GameManager>()._gPlayer1_UI.SetActive(false);
                    _gGameManager.GetComponent<GameManager>().Set_Character_Btn();
                }
                else if (_gNow_Player.tag == "B")
                {
                    _gGameManager.GetComponent<GameManager>()._gPlayer2_UI.SetActive(false);
                    _gGameManager.GetComponent<GameManager>().Set_Character_Btn();
                }

            }
            //攻擊Recall按鈕滑鼠事件
            if (_bOn_Attack_It == true && (GameManager._sPlayer_One_Finish == "Start" || GameManager._sPlayer_Two_Finish == "Start") && tag == "Atk_Grid")
            {
                Atk_Mouse_Clk();

            }
            if (gameObject.tag == "A" && GameManager._sPlayer_One_Finish == "Start" && _bOn_Player_It == true && !EventSystem.current.IsPointerOverGameObject())
            {

               
                if (_gGameManager.GetComponent<GameManager>().m_NowPlayer == null)
                {
                    if (gameObject.GetComponent<Character>().Chess.Now_State != "Finish" && gameObject.GetComponent<Character>().Chess.Now_State != "Death")
                    {
                        _gGameManager.GetComponent<GameManager>().Select_Player(m_Player_Index);
                    }

                  
                }
                else if (_gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Character>().Chess.Job == "Preist" && _gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Character>().Chess.Now_State == "Ready_Attack")
                {
                    for (int i = 0; i < _gGameManager.GetComponent<Path>()._gInit_Grid.transform.childCount; i++)
                    {
                        if (gameObject.transform.position.x == _gGameManager.GetComponent<Path>()._gInit_Grid.transform.GetChild(i).transform.position.x && gameObject.transform.position.z == _gGameManager.GetComponent<Path>()._gInit_Grid.transform.GetChild(i).transform.position.z)
                        {
                            Atk_Mouse_Clk();
                        }
                        else if(i== _gGameManager.GetComponent<Path>()._gInit_Grid.transform.childCount-1)
                        {
                            _gGameManager.GetComponent<GameManager>().Select_Player(m_Player_Index);
                        }
                    }
                    
                }
                else if (_gGameManager.GetComponent<GameManager>().m_NowPlayer != null && _gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Move>()._bIs_Moving == false)
                {
                    if (gameObject.GetComponent<Character>().Chess.Now_State != "Finish" && gameObject.GetComponent<Character>().Chess.Now_State != "Death")
                    {
                        _gGameManager.GetComponent<GameManager>().Select_Player(m_Player_Index);
                    }
                    //if (_gGameManager.GetComponent<GameManager>()._gMove_UI.tag == "In")
                    //{
                    //    _gGameManager.GetComponent<GameManager>().In_And_Out();
                    //}
                }



            }
            if (gameObject.tag == "B" && GameManager._sPlayer_Two_Finish == "Start" && _bOn_Player_It == true && !EventSystem.current.IsPointerOverGameObject())
            {

                if (_gGameManager.GetComponent<GameManager>().m_NowPlayer == null)
                {
                    if (gameObject.GetComponent<Character>().Chess.Now_State != "Finish" && gameObject.GetComponent<Character>().Chess.Now_State != "Death")
                    {
                        _gGameManager.GetComponent<GameManager>().Select_Player(m_Player_Index);
                    }
                }
                else if (_gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Character>().Chess.Job == "Preist" && _gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Character>().Chess.Now_State == "Ready_Attack")
                {
                    for (int i = 0; i < _gGameManager.GetComponent<Path>()._gInit_Grid.transform.childCount; i++)
                    {
                        if (gameObject.transform.position.x == _gGameManager.GetComponent<Path>()._gInit_Grid.transform.GetChild(i).transform.position.x && gameObject.transform.position.z == _gGameManager.GetComponent<Path>()._gInit_Grid.transform.GetChild(i).transform.position.z)
                        {
                            Atk_Mouse_Clk();
                        }
                        else if (i == _gGameManager.GetComponent<Path>()._gInit_Grid.transform.childCount - 1)
                        {
                            _gGameManager.GetComponent<GameManager>().Select_Player(m_Player_Index);
                        }
                    }
                    
                }
                else if (_gGameManager.GetComponent<GameManager>().m_NowPlayer != null && _gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Move>()._bIs_Moving == false)
                {
                    if (gameObject.GetComponent<Character>().Chess.Now_State != "Finish" && gameObject.GetComponent<Character>().Chess.Now_State != "Death")
                    {
                        _gGameManager.GetComponent<GameManager>().Select_Player(m_Player_Index);
                    }
                    
                }

            }
            //轉職區按鈕滑鼠事件
            if (_bOn_Set_it == true && GameManager._sSet_Area_Finish_One == "Start" && GameManager._iPlayer1_Transfer_Area_Count > 0 && this.tag == "Area")
            {
                GameObject Trans_Obj;
                Trans_Obj = Instantiate(_gGameManager.GetComponent<GameManager>()._gTransfer_Obj[0], new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z), transform.rotation, _gGameManager.GetComponent<Path>()._gPlane.transform);
                Trans_Obj.tag = "TransferA";
                //Destroy(gameObject);
                GameManager._iPlayer1_Transfer_Area_Count--;
                path._lTransfer_A.Add(transform.position);

            }
            else if (_bOn_Set_it == true && GameManager._sSet_Area_Finish_Two == "Start" && GameManager._iPlayer2_Transfer_Area_Count > 0 && this.tag == "Area")
            {
                GameObject Trans_Obj;

                Trans_Obj = Instantiate(_gGameManager.GetComponent<GameManager>()._gTransfer_Obj[1], new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z), transform.rotation, _gGameManager.GetComponent<Path>()._gPlane.transform);
                Trans_Obj.tag = "TransferB";
                // Destroy(gameObject);
                GameManager._iPlayer2_Transfer_Area_Count--;
                path._lTransfer_B.Add(transform.position);

            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (_gGameManager.GetComponent<GameManager>().m_NowPlayer != null && _gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Move>()._bIs_Moving == false && _gGameManager.GetComponent<GameManager>()._bCamera_Move == false)
            {
                if (_gGameManager.GetComponent<GameManager>()._gNow_Player_Function_UI != null)
                {
                    _gGameManager.GetComponent<GameManager>()._gNow_Player_Function_UI.SetActive(false);
                    _gGameManager.GetComponent<GameManager>().Set_Character_Btn();
                    _gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Move>().Reset_Data();
                    _gGameManager.GetComponent<GameManager>().In_And_Out();

                }
            }
            if (GameManager._sSet_Area_Finish_One == "Start" && gameObject.tag == "TransferA" && _bOn_Tranf_it == true)
            {
                GameManager._iPlayer1_Transfer_Area_Count++;
                Destroy(gameObject);
            }
            if (GameManager._sSet_Area_Finish_Two == "Start" && gameObject.tag == "TransferB" && _bOn_Tranf_it == true)
            {
                GameManager._iPlayer2_Transfer_Area_Count++;
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (gameObject.tag == "Area" && GameManager._sSet_Area_Finish_One == "Start")
        {

            GetComponent<Renderer>().material.color = Color.gray;
            _bOn_Set_it = true;
        }
        if (gameObject.tag == "Area" && GameManager._sSet_Area_Finish_Two == "Start" && _gGameManager.GetComponent<GameManager>()._bCamera_Move == false)
        {

            GetComponent<Renderer>().material.color = Color.gray;
            _bOn_Set_it = true;
        }
        if (gameObject.tag == "TransferA" && GameManager._sSet_Area_Finish_One == "Start")
        {
            _bOn_Tranf_it = true;
        }
        if (gameObject.tag == "TransferB" && GameManager._sSet_Area_Finish_Two == "Start")
        {
            _bOn_Tranf_it = true;
        }
        if (gameObject.tag == "Grid" && (GameManager._sPlayer_One_Finish == "Start" || GameManager._sPlayer_Two_Finish == "Start") && !EventSystem.current.IsPointerOverGameObject())
        {
            if ((_gNow_Player.transform.position.x != _vDestination.x) || (_gNow_Player.transform.position.z != _vDestination.z))
            {
                GetComponent<Renderer>().material.color = Color.black;
                _gNow_Player.GetComponent<Move>()._gMove_Pos = gameObject;
                _bOn_Move_It = true;
            }
        }
        if (gameObject.tag == "Atk_Grid" && (GameManager._sPlayer_One_Finish == "Start" || GameManager._sPlayer_Two_Finish == "Start") && !EventSystem.current.IsPointerOverGameObject())
        {
            Atk_Mouse_On();
        }
        if ((gameObject.tag == "A" || gameObject.tag == "B") && (GameManager._sPlayer_One_Finish == "Start" || GameManager._sPlayer_Two_Finish == "Start") && !EventSystem.current.IsPointerOverGameObject())
        {

            Player_Atk_On();

        }
        if (gameObject.tag == "A" && GameManager._sPlayer_One_Finish == "Start" && !EventSystem.current.IsPointerOverGameObject())
        {
            PlayerA_Mouse_On();

        }
        if (gameObject.tag == "B" && GameManager._sPlayer_Two_Finish == "Start" && !EventSystem.current.IsPointerOverGameObject())
        {
            PlayerB_Mouse_On();

        }



    }
    private void OnMouseExit()
    {
        if (gameObject.tag == "Area")
        {
            GetComponent<Renderer>().material.color = _cTmp_Area_Color;
            _bOn_Set_it = false;
        }
        if (gameObject.tag == "TransferA" && GameManager._sSet_Area_Finish_One == "Start")
        {
            _bOn_Tranf_it = false;
        }
        if (gameObject.tag == "TransferB" && GameManager._sSet_Area_Finish_Two == "Start")
        {
            _bOn_Tranf_it = false;
        }
        if (gameObject.tag == "Grid")
        {
            GetComponent<Renderer>().material.color = _cTmp_Grid_Color;
            _gNow_Player.GetComponent<Move>()._gMove_Pos = null;
            _bOn_Move_It = false;
        }
        if (gameObject.tag == "Atk_Grid")
        {

            Atk_Mouse_Out();

        }
        if (gameObject.tag == "A" && GameManager._sPlayer_One_Finish == "Start")
        {
            PlayerA_Mouse_Out();

        }
        if (gameObject.tag == "B" && GameManager._sPlayer_Two_Finish == "Start")
        {
            PlayerB_Mouse_Out();

        }
        if ((gameObject.tag == "A" || gameObject.tag == "B") && (GameManager._sPlayer_One_Finish == "Start" || GameManager._sPlayer_Two_Finish == "Start"))
        {

            Player_Atk_Out();

        }



    }



    public void Atk_Mouse_On()
    {
        if (_gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Character>().Chess.Job == "Warrior")
        {

            if (path._lCan_Attack_Enmey.Count == 0)
            {


                for (int i = 0; i < path._gInit_Grid.transform.childCount; i++)
                {
                    if (_gNow_Player.GetComponent<Character>().Chess.Type == "A")
                    {
                        for (int j = 0; j < path._gEnmey.transform.childCount; j++)
                        {
                            if ((path._gInit_Grid.transform.GetChild(i).transform.position.x == path._gEnmey.transform.GetChild(j).gameObject.transform.position.x) && (path._gInit_Grid.transform.GetChild(i).transform.position.z == path._gEnmey.transform.GetChild(j).gameObject.transform.position.z))
                            {

                                path._lCan_Attack_Enmey.Add(path._gEnmey.transform.GetChild(j).gameObject);

                            }
                        }

                    }
                    else if (_gNow_Player.GetComponent<Character>().Chess.Type == "B")
                    {
                        for (int j = 0; j < path._gPartner.transform.childCount; j++)
                        {
                            if ((path._gInit_Grid.transform.GetChild(i).transform.position.x == path._gPartner.transform.GetChild(j).gameObject.transform.position.x) && (path._gInit_Grid.transform.GetChild(i).transform.position.z == path._gPartner.transform.GetChild(j).gameObject.transform.position.z))
                            {

                                path._lCan_Attack_Enmey.Add(path._gPartner.transform.GetChild(j).gameObject);

                            }
                        }
                    }



                    path._gInit_Grid.transform.GetChild(i).GetComponent<Renderer>().material.color = Color.yellow;


                    path._gInit_Grid.transform.GetChild(i).GetComponent<MouseEvent>()._bOn_Attack_It = true;
                }
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.yellow;
            _bOn_Attack_It = true;
        }

    }
    public void Atk_Mouse_Out()
    {
        if (_gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Character>().Chess.Job == "Warrior")
        {
            path._lCan_Attack_Enmey.Clear();
            for (int i = 0; i < path._gInit_Grid.transform.childCount; i++)
            {

                path._gInit_Grid.transform.GetChild(i).GetComponent<Renderer>().material.color = _cTmp_Atk_Grid_Color;


                path._gInit_Grid.transform.GetChild(i).GetComponent<MouseEvent>()._bOn_Attack_It = false;
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = _cTmp_Atk_Grid_Color;
            _bOn_Attack_It = false;
        }
    }


    public void Atk_Mouse_Clk()
    {
        GameObject Enmey;


        if (_gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Character>().Chess.Type == "A" && _gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Character>().Chess.Job != "Preist")
        {

            for (int i = 0; i < path._gEnmey.transform.childCount; i++)
            {
                if ((gameObject.transform.position.x == path._gEnmey.transform.GetChild(i).gameObject.transform.position.x) && (gameObject.transform.position.z == path._gEnmey.transform.GetChild(i).gameObject.transform.position.z))
                {

                    if (_gNow_Player.GetComponent<Character>().Chess.Job == "Warrior")
                    {
                        Enmey = path._gEnmey.transform.GetChild(i).gameObject;
                        _gNow_Player.GetComponent<Attack>().Attack_Enmey(Enmey);
                        


                    }
                    else
                    {
                        Enmey = path._gEnmey.transform.GetChild(i).gameObject;
                        _gNow_Player.GetComponent<Attack>().Attack_Enmey(Enmey);
                        _gNow_Player.GetComponent<Character>().Chess.Have_Attacked = true;
                        _gNow_Player.GetComponent<Character>().Chess.Have_Moved = true;
                        _gNow_Player.GetComponent<Character>().Chess.Now_State = "Have_Attacked";
                        _gNow_Player.GetComponent<Attack>().Destory_AttackGrid();
                        _gGameManager.GetComponent<GameManager>().Set_Character_Btn();
                        _gGameManager.GetComponent<GameManager>().In_And_Out();
                        _gGameManager.GetComponent<GameManager>().End_Btn_Click();
                    }




                }
            }






        }





        else if (_gNow_Player.GetComponent<Character>().Chess.Type == "B" && _gNow_Player.GetComponent<Character>().Chess.Job != "Preist")
        {

            for (int i = 0; i < path._gPartner.transform.childCount; i++)
            {
                if ((gameObject.transform.position.x == path._gPartner.transform.GetChild(i).gameObject.transform.position.x) && (gameObject.transform.position.z == path._gPartner.transform.GetChild(i).gameObject.transform.position.z))
                {
                    if (_gNow_Player.GetComponent<Character>().Chess.Job == "Warrior")
                    {
                        Enmey = path._gPartner.transform.GetChild(i).gameObject;
                        _gNow_Player.GetComponent<Attack>().Attack_Enmey(Enmey);
                        //_gNow_Player.GetComponent<Character>().Chess.Have_Attacked = true;
                        //_gNow_Player.GetComponent<Character>().Chess.Have_Moved = true;
                        //_gNow_Player.GetComponent<Character>().Chess.Now_State = "Have_Attacked";
                        //_gGameManager.GetComponent<GameManager>().End_Btn_Click();

                    }
                    else
                    {
                        Enmey = path._gPartner.transform.GetChild(i).gameObject;
                        _gNow_Player.GetComponent<Attack>().Attack_Enmey(Enmey);
                        _gNow_Player.GetComponent<Character>().Chess.Have_Attacked = true;
                        _gNow_Player.GetComponent<Character>().Chess.Have_Moved = true;
                        _gNow_Player.GetComponent<Character>().Chess.Now_State = "Have_Attacked";
                        _gNow_Player.GetComponent<Attack>().Destory_AttackGrid();
                        _gGameManager.GetComponent<GameManager>().Set_Character_Btn();
                        _gGameManager.GetComponent<GameManager>().In_And_Out();
                        _gGameManager.GetComponent<GameManager>().End_Btn_Click();
                    }

                    // _gGameManager.GetComponent<GameManager>().End_Btn_Click();
                }
            }

        }

        //回溯職業
        GameObject Partner;
        if (_gNow_Player.GetComponent<Character>().Chess.Type == "A" && _gNow_Player.GetComponent<Character>().Chess.Job == "Preist")
        {


            for (int i = 0; i < path._gPartner.transform.childCount; i++)
            {
                if ((gameObject.transform.position.x == path._gPartner.transform.GetChild(i).gameObject.transform.position.x) && (gameObject.transform.position.z == path._gPartner.transform.GetChild(i).gameObject.transform.position.z))
                {
                    Partner = path._gPartner.transform.GetChild(i).gameObject;
                    if (Partner.GetComponent<Character>().Chess.Job != "Minion")
                    {

                        _gNow_Player.GetComponent<Attack>().Recall_Partner(Partner);
                        _gNow_Player.GetComponent<Character>().Chess.Have_Attacked = true;
                        _gNow_Player.GetComponent<Character>().Chess.Have_Moved = true;
                        _gNow_Player.GetComponent<Character>().Chess.Now_State = "Have_Attacked";
                        _gNow_Player.GetComponent<Attack>().Destory_AttackGrid();
                        _gGameManager.GetComponent<GameManager>().Set_Character_Btn();
                        _gGameManager.GetComponent<GameManager>().In_And_Out();
                        _gGameManager.GetComponent<GameManager>().End_Btn_Click();
                    }

                }
            }


        }
        else if (_gNow_Player.GetComponent<Character>().Chess.Type == "B" && _gNow_Player.GetComponent<Character>().Chess.Job == "Preist")
        {


            for (int i = 0; i < path._gEnmey.transform.childCount; i++)
            {
                if ((gameObject.transform.position.x == path._gEnmey.transform.GetChild(i).gameObject.transform.position.x) && (gameObject.transform.position.z == path._gEnmey.transform.GetChild(i).gameObject.transform.position.z))
                {
                    Partner = path._gEnmey.transform.GetChild(i).gameObject;
                    if (Partner.GetComponent<Character>().Chess.Job != "Minion")
                    {
                        _gNow_Player.GetComponent<Attack>().Recall_Partner(Partner);
                        _gNow_Player.GetComponent<Character>().Chess.Have_Attacked = true;
                        _gNow_Player.GetComponent<Character>().Chess.Have_Moved = true;
                        _gNow_Player.GetComponent<Character>().Chess.Now_State = "Have_Attacked";
                        _gNow_Player.GetComponent<Attack>().Destory_AttackGrid();
                        _gGameManager.GetComponent<GameManager>().Set_Character_Btn();
                        _gGameManager.GetComponent<GameManager>().In_And_Out();
                        _gGameManager.GetComponent<GameManager>().End_Btn_Click();
                    }
                }
            }
        }

    }

    public void PlayerA_Mouse_On()
    {
        for (int i = 0; i < _gGameManager.GetComponent<GameManager>()._gPlayer1.transform.childCount; i++)
        {
            if ((gameObject.name == _gGameManager.GetComponent<GameManager>()._gPlayer1.transform.GetChild(i).name))
            {

                //GetComponent<Renderer>().material.color = Color.blue;
                _bOn_Player_It = true;
                m_Player_Index = i;
            }
        }

    }
    public void PlayerA_Mouse_Out()
    {
        for (int i = 0; i < _gGameManager.GetComponent<GameManager>()._gPlayer1.transform.childCount; i++)
        {
            if ((gameObject.name == _gGameManager.GetComponent<GameManager>()._gPlayer1.transform.GetChild(i).name))
            {


                _bOn_Player_It = false;
            }
        }

    }
    public void PlayerB_Mouse_On()
    {
        for (int i = 0; i < _gGameManager.GetComponent<GameManager>()._gPlayer2.transform.childCount; i++)
        {
            if ((gameObject.name == _gGameManager.GetComponent<GameManager>()._gPlayer2.transform.GetChild(i).name))
            {
                //GetComponent<Renderer>().material.color = Color.blue;

                _bOn_Player_It = true;
                m_Player_Index = i;
            }
        }

    }
    public void PlayerB_Mouse_Out()
    {
        for (int i = 0; i < _gGameManager.GetComponent<GameManager>()._gPlayer2.transform.childCount; i++)
        {
            if ((gameObject.name == _gGameManager.GetComponent<GameManager>()._gPlayer2.transform.GetChild(i).name))
            {
                //GetComponent<Renderer>().material.color = Color.blue;

                _bOn_Player_It = false;
            }
        }

    }

    public void Player_Atk_On()
    {

        if (_gGameManager.GetComponent<GameManager>().m_NowPlayer != null && _gGameManager.GetComponent<Path>()._lCan_Attack_List.Count != 0)
        {

            for (int i = 0; i < _gGameManager.GetComponent<Path>()._gInit_Grid.transform.childCount; i++)
            {
                if (gameObject.transform.position.x == _gGameManager.GetComponent<Path>()._gInit_Grid.transform.GetChild(i).transform.position.x && gameObject.transform.position.z == _gGameManager.GetComponent<Path>()._gInit_Grid.transform.GetChild(i).transform.position.z)
                {
                    _gGameManager.GetComponent<Path>()._gInit_Grid.transform.GetChild(i).GetComponent<MouseEvent>().Atk_Mouse_On();
                }
            }
        }
    }
    public void Player_Atk_Out()
    {
        if (_gGameManager.GetComponent<GameManager>().m_NowPlayer != null && _gGameManager.GetComponent<Path>()._lCan_Attack_List.Count != 0)
        {
            for (int i = 0; i < _gGameManager.GetComponent<Path>()._gInit_Grid.transform.childCount; i++)
            {
                if (gameObject.transform.position.x == _gGameManager.GetComponent<Path>()._gInit_Grid.transform.GetChild(i).transform.position.x && gameObject.transform.position.z == _gGameManager.GetComponent<Path>()._gInit_Grid.transform.GetChild(i).transform.position.z)
                {
                    _gGameManager.GetComponent<Path>()._gInit_Grid.transform.GetChild(i).GetComponent<MouseEvent>().Atk_Mouse_Out();
                }
            }
        }
    }

}
