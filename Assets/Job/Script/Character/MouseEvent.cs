using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEvent : MonoBehaviour
{
    private Color _cTmp_Grid_Color; //原本的格子顏色
    private Color _cTmp_Area_Color; //原本的區域顏色
    private Color _cTmp_Atk_Grid_Color; //原本的攻擊區域顏色
    public Vector3 _vDestination; //格子的位置
    public GameObject _gGameManager; //管理器
    public GameObject _gNow_Player; //現在控制的旗子
    public bool _bOn_Move_It;            //是否有在移動的格子上面
    public bool _bOn_Set_it;        //是否有要設定的格子上面
    public bool _bOn_Attack_It;         //是否在攻擊的格子上面
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
        _gNow_Player = _gGameManager.GetComponent<Path>()._gPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        //如果在格子上面按左鍵
        if (Input.GetButtonDown("Fire1"))
        {

            //移動按鈕滑鼠事件
            if (_bOn_Move_It == true && (GameManager._sPlayer_One_Finish == "Start" || GameManager._sPlayer_Two_Finish == "Start") && tag == "Grid")
            {
                _gNow_Player.GetComponent<Move>().Cal_Road();
                _gNow_Player.GetComponent<Character>().Chess.Now_State = "Have_Moved";
                _gNow_Player.GetComponent<Character>().Chess.Have_Moved = true;
                _gGameManager.GetComponent<GameManager>()._gMove_UI.SetActive(false);
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
            //攻擊按鈕滑鼠事件
            if (_bOn_Attack_It == true && (GameManager._sPlayer_One_Finish == "Start" || GameManager._sPlayer_Two_Finish == "Start") && tag == "Atk_Grid")
            {
                GameObject Enmey;
                if (_gNow_Player.GetComponent<Character>().Chess.Type == "A")
                {
                  
                    for (int i = 0; i < path._gEnmey.transform.childCount;i++ )
                    {
                        if ((gameObject.transform.position.x == path._gEnmey.transform.GetChild(i).gameObject.transform.position.x)&& (gameObject.transform.position.z == path._gEnmey.transform.GetChild(i).gameObject.transform.position.z))
                        {
                            Enmey = path._gEnmey.transform.GetChild(i).gameObject;
                            _gNow_Player.GetComponent<Attack>().Attack_Enmey(Enmey);
                            _gNow_Player.GetComponent<Character>().Chess.Have_Attacked = true;
                            _gNow_Player.GetComponent<Character>().Chess.Have_Moved = true;
                            _gNow_Player.GetComponent<Character>().Chess.Now_State = "Have_Attacked";
                            _gNow_Player.GetComponent<Attack>().Destory_AttackGrid();
                            _gGameManager.GetComponent<GameManager>().Set_Character_Btn();
                            _gGameManager.GetComponent<GameManager>().In_And_Out();
                        }
                    }
                    

                }
                else if(_gNow_Player.GetComponent<Character>().Chess.Type == "B")
                {

                    for (int i = 0; i < path._gPartner.transform.childCount; i++)
                    {
                        if ((gameObject.transform.position.x == path._gPartner.transform.GetChild(i).gameObject.transform.position.x) && (gameObject.transform.position.z == path._gPartner.transform.GetChild(i).gameObject.transform.position.z))
                        {
                            Enmey = path._gPartner.transform.GetChild(i).gameObject;
                            _gNow_Player.GetComponent<Attack>().Attack_Enmey(Enmey);
                            _gNow_Player.GetComponent<Character>().Chess.Have_Attacked = true;
                            _gNow_Player.GetComponent<Character>().Chess.Have_Moved = true;
                            _gNow_Player.GetComponent<Character>().Chess.Now_State = "Have_Attacked";
                            _gNow_Player.GetComponent<Attack>().Destory_AttackGrid();
                            _gGameManager.GetComponent<GameManager>().Set_Character_Btn();
                            _gGameManager.GetComponent<GameManager>().In_And_Out();
                        }
                    }
                }
                
                
            }

            //轉職區按鈕滑鼠事件
            if (_bOn_Set_it == true && GameManager._sSet_Area_Finish_One == "Start" && GameManager._iPlayer1_Transfer_Area_Count > 0 && this.tag == "Area")
            {


                
                switch (GameManager._iPlayer1_Transfer_Area_Count)
                {
                    case 3:
                        this.tag = "Warrior";

                        GetComponent<Renderer>().material.color = Color.green;
                        break;
                    case 2:
                        this.tag = "Archor";
                        GetComponent<Renderer>().material.color = Color.green;
                        break;
                    case 1:
                        this.tag = "Magic";
                        GetComponent<Renderer>().material.color = Color.green;
                        break;
                }
                GameManager._iPlayer1_Transfer_Area_Count--;

            }
            else if (_bOn_Set_it == true && GameManager._sSet_Area_Finish_Two == "Start" && GameManager._iPlayer2_Transfer_Area_Count > 0 && this.tag == "Area")
            {
                switch (GameManager._iPlayer2_Transfer_Area_Count)
                {
                    case 3:
                        this.tag = "Warrior";
                        GetComponent<Renderer>().material.color = Color.gray;
                        break;
                    case 2:
                        this.tag = "Archor";
                        GetComponent<Renderer>().material.color = Color.gray;
                        break;
                    case 1:
                        this.tag = "Magic";
                        GetComponent<Renderer>().material.color = Color.gray;
                        break;
                }
                GameManager._iPlayer2_Transfer_Area_Count--;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (gameObject.tag == "Area" && (GameManager._sSet_Area_Finish_One == "Start" || GameManager._sSet_Area_Finish_Two == "Start")&&!EventSystem.current.IsPointerOverGameObject())
        {
            GetComponent<Renderer>().material.color = Color.blue;
            _bOn_Set_it = true;
        }
        if (gameObject.tag == "Grid" && (GameManager._sPlayer_One_Finish == "Start" || GameManager._sPlayer_Two_Finish == "Start")&&!EventSystem.current.IsPointerOverGameObject())
        {
            if ((_gNow_Player.transform.position.x != _vDestination.x) || (_gNow_Player.transform.position.z != _vDestination.z))
            {
                GetComponent<Renderer>().material.color = Color.black;
                _gNow_Player.GetComponent<Move>()._gMove_Pos = gameObject;
                _bOn_Move_It = true;
            }
        }
        if (gameObject.tag == "Atk_Grid" && (GameManager._sPlayer_One_Finish == "Start" || GameManager._sPlayer_Two_Finish == "Start")&&!EventSystem.current.IsPointerOverGameObject())
        {
            GetComponent<Renderer>().material.color = Color.yellow;
            _bOn_Attack_It = true;
        }



    }
    private void OnMouseExit()
    {
        if (gameObject.tag == "Area")
        {
            GetComponent<Renderer>().material.color = _cTmp_Area_Color;
            _bOn_Set_it = false;
        }
        if (gameObject.tag == "Grid")
        {
            GetComponent<Renderer>().material.color = _cTmp_Grid_Color;
            _gNow_Player.GetComponent<Move>()._gMove_Pos = null;
            _bOn_Move_It = false;
        }
        if (gameObject.tag == "Atk_Grid")
        {
            GetComponent<Renderer>().material.color = _cTmp_Atk_Grid_Color;
            _bOn_Attack_It = false;
        }


    }

}
