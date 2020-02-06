using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEvent : MonoBehaviour
{
    private Color _cTmp_Grid_Color; //原本的格子顏色
    private Color _cTmp_Area_Color; //原本的區域顏色
    public Vector3 _vDestination; //格子的位置
    public GameObject _gGameManager; //管理器
    public GameObject _gNow_Player; //現在控制的旗子
    public bool _bOn_Move_It;            //是否有在移動的格子上面
    public bool _bOn_Set_it;        //是否有要設定的格子上面

    private void Awake()
    {
        _gGameManager = GameObject.Find("GameManager");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Area")
        {
            _cTmp_Area_Color = GetComponent<Renderer>().material.color;
        }
        else
        {
            _cTmp_Grid_Color = GetComponent<Renderer>().material.color;
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
            if (_bOn_Move_It == true && GameManager._sPlayer_One_Finish == "Start" ||GameManager._sPlayer_Two_Finish=="Start"&&tag == "Grid")
            {
                _gNow_Player.GetComponent<Move>().Cal_Road();
            }
            if(_bOn_Set_it == true && GameManager._sSet_Area_Finish_One=="Start"&&GameManager._iPlayer1_Transfer_Area_Count>0&&this.tag=="Area")
            {

                Debug.Log(GameManager._iPlayer1_Transfer_Area_Count);
               
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
            else if(_bOn_Set_it==true&& GameManager._sSet_Area_Finish_Two == "Start" && GameManager._iPlayer2_Transfer_Area_Count > 0 && this.tag == "Area")
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
        if (gameObject.tag == "Area"&&(GameManager._sSet_Area_Finish_One=="Start"||GameManager._sSet_Area_Finish_Two=="Start"))
        {
            GetComponent<Renderer>().material.color = Color.blue;
            _bOn_Set_it = true;
        }
        else if( gameObject.tag == "Grid")
        {
            if ((_gNow_Player.transform.position.x != _vDestination.x) || (_gNow_Player.transform.position.z != _vDestination.z))
            {
                GetComponent<Renderer>().material.color = Color.black;
                _gNow_Player.GetComponent<Move>()._gMove_Pos = gameObject;
                _bOn_Move_It = true;
            }
        }
        


    }
    private void OnMouseExit()
    {
        if (gameObject.tag == "Area")
        {
            GetComponent<Renderer>().material.color = _cTmp_Area_Color;
            _bOn_Set_it = false;
        }
        else if(gameObject.tag == "Grid")
        {
            GetComponent<Renderer>().material.color = _cTmp_Grid_Color;
            _gNow_Player.GetComponent<Move>()._gMove_Pos = null;
            _bOn_Move_It = false;
        }
        else 
        {
            if(GameManager._iPlayer1_Transfer_Area_Count!=0)
            {
                
            }
            else
            {
               
            }
            
        }

    }

}
