﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public GameObject _gAttack_Grid; //生成的戰鬥格子
    public GameObject _gGrid_Group; //放格子的位置
    public Path path;        //路徑的Class

    public GameObject[] _gEffect = new GameObject[5]; //攻擊的特效
    public GameObject[] _gBallet = new GameObject[4];   //遠程攻擊的物品
    public GameObject _gEnmey;

    public List<GameObject> _lAttacked;
    public GameObject _gGameManager;
    public int _iJob_Num;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 產生攻擊的地板方塊
    /// </summary>
    public void Attack_Grid_Instant()
    {
        for (int i = 0; i < path._lCan_Attack_List.Count; i++)
        {

            if (gameObject.transform.position.x != path._lCan_Attack_List[i].x || gameObject.transform.position.z != path._lCan_Attack_List[i].z)
            {
                Instantiate(_gAttack_Grid, new Vector3(path._lCan_Attack_List[i].x, 0.01f, path._lCan_Attack_List[i].z), _gAttack_Grid.transform.rotation, _gGrid_Group.transform);
            }
           

        }
    }

    /// <summary>
    /// 消滅攻擊的地板方塊
    /// </summary>
    public void Destory_AttackGrid()
    {
        for (int i = 0; i < _gGrid_Group.transform.childCount; i++)
        {

            Destroy(_gGrid_Group.transform.GetChild(i).gameObject);
        }
    }



    /// <summary>
    /// 攻擊
    /// </summary>
    /// <param name="Enmey"></param>
    public void Attack_Enmey(GameObject Enmey)
    {

        _iJob_Num = gameObject.GetComponent<Character>()._iNow_Class_Count;
        //_gGameManager.GetComponent<GameManager>()._aBattle_Scene_Anim.SetTrigger("Go");
        //_gGameManager.GetComponent<GameManager>()._gNormal_Camera.SetActive(false);
        //_gGameManager.GetComponent<GameManager>()._gWhole_UI.SetActive(false);
        switch (gameObject.GetComponent<Character>().Chess.Job)
        {
            
            case "Minion":
             


                break;
            case "Magician":
            
                if (Enmey.tag == "A")
                {
                    _gGameManager.GetComponent<GameManager>()._B_Model = Instantiate(_gGameManager.GetComponent<GameManager>()._gA_Team_Model[Enmey.GetComponent<Character>()._iNow_Class_Count], _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform.position, _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform.rotation, _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform);
                    _gGameManager.GetComponent<GameManager>()._aB_Battle_Anim = _gGameManager.GetComponent<GameManager>()._B_Model.GetComponent<Animator>();
                    _gGameManager.GetComponent<GameManager>()._B_Model.SetActive(true);

                }
                else if (Enmey.tag == "B")
                {
                    _gGameManager.GetComponent<GameManager>()._B_Model = Instantiate(_gGameManager.GetComponent<GameManager>()._gB_Team_Model[Enmey.GetComponent<Character>()._iNow_Class_Count], _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform.position, _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform.rotation, _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform);
                    _gGameManager.GetComponent<GameManager>()._B_Model.SetActive(true);
                    _gGameManager.GetComponent<GameManager>()._aB_Battle_Anim = _gGameManager.GetComponent<GameManager>()._B_Model.GetComponent<Animator>();


                }
                GameObject Tmp_Ball;
                Tmp_Ball = _gBallet[_iJob_Num];
                Tmp_Ball.name = gameObject.GetComponent<Character>().Chess.Attack.ToString();
                Tmp_Ball.tag = gameObject.GetComponent<Character>().Chess.Job;
                Instantiate(Tmp_Ball, new Vector3(Enmey.transform.position.x, Enmey.transform.position.y + 3.0f, Enmey.transform.position.z), Tmp_Ball.transform.rotation);

              
                
                break;
            case "Warrior":
               
               


                if (path._lCan_Attack_Enmey.Count == 0)
                {
                    Destory_AttackGrid();
                    gameObject.GetComponent<Character>().Chess.Have_Attacked = true;
                    gameObject.GetComponent<Character>().Chess.Have_Moved = true;
                    
                    path.gameObject.GetComponent<GameManager>().Set_Character_Btn();
                    path.gameObject.GetComponent<GameManager>().In_And_Out();
                    path.gameObject.GetComponent<GameManager>().End_Btn_Click();
                }
                break;


            case "Archer":
           
                if (Enmey.tag == "A")
                {
                    
                    _gGameManager.GetComponent<GameManager>()._B_Model = Instantiate(_gGameManager.GetComponent<GameManager>()._gA_Team_Model[Enmey.GetComponent<Character>()._iNow_Class_Count], _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform.position, _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform.rotation, _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform);
                    _gGameManager.GetComponent<GameManager>()._aB_Battle_Anim = _gGameManager.GetComponent<GameManager>()._B_Model.GetComponent<Animator>();
                    _gGameManager.GetComponent<GameManager>()._B_Model.SetActive(true);

                }
                else if (Enmey.tag == "B")
                {
                    _gGameManager.GetComponent<GameManager>()._B_Model = Instantiate(_gGameManager.GetComponent<GameManager>()._gB_Team_Model[Enmey.GetComponent<Character>()._iNow_Class_Count], _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform.position, _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform.rotation, _gGameManager.GetComponent<GameManager>()._gB_Battle_Pos.transform);
                    _gGameManager.GetComponent<GameManager>()._B_Model.SetActive(true);
                    _gGameManager.GetComponent<GameManager>()._aB_Battle_Anim = _gGameManager.GetComponent<GameManager>()._B_Model.GetComponent<Animator>();


                }

                GameObject Tmp_Arror;
                _gGameManager.GetComponent<Music>()._eArcher_Shoot.Invoke();
                Tmp_Arror = Instantiate(_gBallet[_iJob_Num], new Vector3(transform.position.x,transform.position.y+1.0f,transform.position.z), _gBallet[_iJob_Num].transform.rotation); ;
                Tmp_Arror.name = gameObject.GetComponent<Character>().Chess.Attack.ToString();
                Tmp_Arror.tag = gameObject.GetComponent<Character>().Chess.Job;
                Tmp_Arror.GetComponent<Arrow>()._gEmney = Enmey;
                break;
        }



    }

    

    public void Battle_Scene_Atk()
    {

    }
    /// <summary>
    /// 回朔
    /// </summary>
    /// <param name="Partner"></param>
    public void Recall_Partner(GameObject Partner)
    {
        _iJob_Num = gameObject.GetComponent<Character>()._iNow_Class_Count;
        if (gameObject.GetComponent<Character>().Chess.Job == "Preist")
        {
         //   _gGameManager.GetComponent<GameManager>().Set_Character_Btn();
            Partner.GetComponent<Character>().Set_Job_Data("Minion");
            Partner.GetComponent<Character>().Chess.HP += gameObject.GetComponent<Character>().Chess.Attack;
            




            _gGameManager.GetComponent<GameManager>()._bPlayer_Btn = Partner.GetComponent<Character>()._gClass_Card.transform.GetChild(Partner.GetComponent<Character>()._iNow_Class_Count).GetComponent<Button>();
            _gGameManager.GetComponent<GameManager>().m_NowPlayer = Partner;
            _gGameManager.GetComponent<GameManager>().Set_Character_Btn();
            _gGameManager.GetComponent<GameManager>().m_NowPlayer = gameObject;
            _gGameManager.GetComponent<GameManager>()._bPlayer_Btn = _gGameManager.GetComponent<GameManager>()._gNow_Player_UI.transform.GetChild(_gGameManager.GetComponent<GameManager>().m_NowPlayer.GetComponent<Character>()._iNow_Class_Count).GetComponent<Button>();

            Instantiate(_gEffect[_iJob_Num], Partner.transform.position, _gEffect[_iJob_Num].transform.rotation);
        }
        
        }

    }



