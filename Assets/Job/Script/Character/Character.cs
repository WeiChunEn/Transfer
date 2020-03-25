using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public string _sType;
    public string _sJob;
    public int _iWalk_Steps;
    public int _iAttack_Distance;
    public int _iHP;
    public int _iMax_HP;
    public int _iAttack;
    public string _sNow_State;
    public bool _bHave_Moved;
    public bool _bHave_Attacked;

    public GameObject _gPlayer_UI;
    public Slider _sHP_Slider;      //血條Slider
    public TextMeshProUGUI _tHP;    //血條%數
    public TextMeshProUGUI _tName;  //血條旁的名字

    public GameObject _gGameManager; //管理器
    public Slider _sHead_HP;        //頭上的血條Slider
    public TextMeshProUGUI _tHead_HP; //頭上的血條%
    public TextMeshProUGUI _tHead_Name;  //血條旁的名字
    public GameObject _g3D_UI;      //頭上的Canvas

    private float _Death_Time;      //死亡分解的時間
    public int _iNow_Class_Count;   //轉職的編號
    public GameObject _gBack_Model; //背景的模型
    public GameObject[] _gClass = new GameObject[4]; //其他職業的模型
    public GameObject _gClass_Card; //其他職業的卡牌圖

    public GameObject _gTransfer_Effect;        //轉職特效
    public GameObject _gTrnasferA_Stone_Effect; //石頭A轉職特效
    public GameObject _gTrnasferB_Stone_Effect; //石頭B轉職特效

    public Material _mMat;          //角色材質球
    public Material _mStone_Mat;    //石像材質

    public GameObject _gBattle_Pos;
    public Transform _tOri_Pos;

    public GameObject[] _gEffect = new GameObject[5]; //被攻擊以及攻擊的特效
    public Animator _aChess_Anime;     //角色的動畫
    public class Character_Data
    {
        protected string m_Type;
        protected string m_Job;
        protected int m_Walk_Steps;
        protected int m_Attack_Distance;
        protected int m_HP;
        protected int m_Max_HP;
        protected int m_Attack;
        protected string m_Now_State;
        protected bool m_Have_Moved;
        protected bool m_Have_Attacked;
    }

    public class Set_Character : Character_Data
    {


        public Set_Character(string Type, string Job, int Walk_Steps, int Attack_Distance, int HP, int Max_HP, int Attack, string Now_State)
        {
            this.m_Type = Type;
            this.m_Job = Job;
            this.m_Walk_Steps = Walk_Steps;
            this.m_Attack_Distance = Attack_Distance;
            this.m_HP = HP;
            this.m_Max_HP = Max_HP;
            this.m_Attack = Attack;
            this.m_Now_State = Now_State;
        }
        public string Type => m_Type;
        public string Job { get => m_Job; set => m_Job = value; }
        public int Walk_Steps { get => m_Walk_Steps; set => m_Walk_Steps = value; }
        public int Attack_Distance { get => m_Attack_Distance; set => m_Attack_Distance = value; }
        public int Max_HP { get => m_Max_HP; set => m_Max_HP = value; }
        public int HP
        {
            get { return this.m_HP; }
            set
            {
                m_HP = value;

                if (m_HP < 0)
                { m_HP = 0; }
                else if(m_HP>=m_Max_HP)
                {
                    m_HP = m_Max_HP;
                }
            }

        }
        
        public int Attack { get => this.m_Attack; set => m_Attack = value; }

        public string Now_State { get => m_Now_State; set => m_Now_State = value; }
        public bool Have_Moved { get => m_Have_Moved; set => m_Have_Moved = value; }
        public bool Have_Attacked { get => m_Have_Attacked; set => m_Have_Attacked = value; }

    }



    public Set_Character Chess;

    public virtual void Awake()
    {
        if(_gClass_Card.transform.GetChild(4).name=="Preist")
        {
            _sType = gameObject.tag;
            _sJob = "Preist";
            _iWalk_Steps = 2;
            _iAttack_Distance = 1;
            _iHP = 20;

            _iMax_HP = 20;
            _iAttack = 6;
            _sNow_State = "Idle";
            _sHP_Slider.maxValue = _iMax_HP;
            _tHP.text = ((_iHP / _sHP_Slider.maxValue) *100).ToString();
            
            _sHead_HP.maxValue = _sHP_Slider.maxValue;
            _tHead_HP.text = _tHP.text;
            _iNow_Class_Count = 4;
            _mMat = gameObject.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial;
            _mStone_Mat = _gBack_Model.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;
            //_gClass[_iNow_Class_Count].SetActive(true)


        }
        else
        {
            _sType = gameObject.tag;
            _sJob = "Minion";
            _iWalk_Steps = 2;
            _iAttack_Distance = 1;
            _iHP = 20;
            _iMax_HP = 20;
            _iAttack = 4;
            _sNow_State = "Idle";
            _sHP_Slider.maxValue = _iMax_HP;
            _tHP.text = ((_iHP / _sHP_Slider.maxValue) * 100).ToString();
            _sHead_HP.maxValue = _sHP_Slider.maxValue;
            _tHead_HP.text = _tHP.text;
            _iNow_Class_Count = 0;
            _gClass[_iNow_Class_Count].SetActive(true);
            _gClass_Card.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(true);
            _aChess_Anime = _gClass[_iNow_Class_Count].GetComponent<Animator>();
            _mMat = gameObject.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;
            _mStone_Mat = _gBack_Model.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;

        }

    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        Chess = new Set_Character(_sType, _sJob, _iWalk_Steps, _iAttack_Distance, _iHP, _iMax_HP, _iAttack, _sNow_State);

    }

    // Update is called once per frame
    public virtual void Update()
    {

        Set_Debug();
        Look_At_Camera();
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Chess.HP = 0;
           
        }
        if (Chess.HP <= 0&& _gGameManager.GetComponent<GameManager>()._gBattle_Camera.tag == "Out")
        {
            if(_Death_Time == 0.0f)
            {
                _gGameManager.GetComponent<Music>()._eDissolve.Invoke();
            }
            _Death_Time += Time.deltaTime * 0.2f;
            _mMat.SetFloat("_DissolveCutoff", _Death_Time);
            _mStone_Mat.SetFloat("_DissolveCutoff", _Death_Time);
            //_gBack_Model.SetActive(false);
            if (_Death_Time >= 1.0)
            {
                gameObject.SetActive(false);
                gameObject.transform.position = new Vector3(100, 100, 100);
                Chess.Now_State = "Death";
               
            }
            
            _gPlayer_UI.transform.GetChild(_iNow_Class_Count).GetComponent<Button>().interactable = false;

            
        }
    }


    /// <summary>
    /// 寫入職業數據
    /// </summary>
    /// <param name="new_Job"></param>
    public void Set_Job_Data(string new_Job)
    {
        
        switch (new_Job)
        {
            case "Warrior":
                Chess.Job = "Warrior";
                _gClass[_iNow_Class_Count].SetActive(false);
                _gClass_Card.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(false);
                _gBack_Model.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(false);
                _iNow_Class_Count = 1;
                _gClass[_iNow_Class_Count].SetActive(true);
                _gBack_Model.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(true);
                _gClass_Card.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(true);
                if(gameObject.tag=="A")
                {
                    Instantiate(_gTrnasferA_Stone_Effect, _gBack_Model.transform.position, _gTrnasferA_Stone_Effect.transform.rotation);
                }
                else if(gameObject.tag == "B")
                {
                    Instantiate(_gTrnasferB_Stone_Effect, _gBack_Model.transform.position, _gTrnasferB_Stone_Effect.transform.rotation);

                }

                //Chess.Max_HP *= 2;
                //Chess.HP *= 2;

                Chess.Attack = 4;
                Chess.Attack_Distance = 3;
                Chess.Walk_Steps = 1;
                _tHead_Name.text = "W" + _tHead_Name.name;
                _tName.text = "W" + _tName.name;
                _mMat = gameObject.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;
                _mStone_Mat = _gBack_Model.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;

                _aChess_Anime = _gClass[_iNow_Class_Count].GetComponent<Animator>();

                break;
            case "Magician":
                Chess.Job = "Magician";
                _gClass[_iNow_Class_Count].SetActive(false);
                _gClass_Card.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(false);

                _gBack_Model.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(false);
                _iNow_Class_Count = 2;
                _gClass[_iNow_Class_Count].SetActive(true);
                _gBack_Model.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(true);
                _gClass_Card.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(true);
                if (gameObject.tag == "A")
                {
                    Instantiate(_gTrnasferA_Stone_Effect, _gBack_Model.transform.position, _gTrnasferA_Stone_Effect.transform.rotation);
                }
                else if (gameObject.tag == "B")
                {
                    Instantiate(_gTrnasferB_Stone_Effect, _gBack_Model.transform.position, _gTrnasferB_Stone_Effect.transform.rotation);

                }
                //Chess.Max_HP /= 2;
                //Chess.HP /= 2;

                Chess.Attack = 6;
                Chess.Attack_Distance = 2;
                Chess.Walk_Steps = 2;
                _tHead_Name.text = "M" + _tHead_Name.name;
                _tName.text = "M" + _tName.name;
                _aChess_Anime = _gClass[_iNow_Class_Count].GetComponent<Animator>();
                _mMat = gameObject.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMaterial;
                _mStone_Mat = _gBack_Model.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;

                break;
            case "Archer":
                Chess.Job = "Archer";
                _gClass[_iNow_Class_Count].SetActive(false);
                _gBack_Model.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(false);
                _gClass_Card.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(false);

                _iNow_Class_Count = 3;
                _gClass[_iNow_Class_Count].SetActive(true);
                _gBack_Model.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(true);
                _gClass_Card.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(true);
                if (gameObject.tag == "A")
                {
                    Instantiate(_gTrnasferA_Stone_Effect, _gBack_Model.transform.position, _gTrnasferA_Stone_Effect.transform.rotation);
                }
                else if (gameObject.tag == "B")
                {
                    Instantiate(_gTrnasferB_Stone_Effect, _gBack_Model.transform.position, _gTrnasferB_Stone_Effect.transform.rotation);

                }
                Chess.Attack = 6;
                Chess.Attack_Distance = 3;
                Chess.Walk_Steps = 2;
                _tHead_Name.text = "A" + _tHead_Name.name;
                _tName.text = "A" + _tName.name;
                _mMat = gameObject.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;
                _mStone_Mat = _gBack_Model.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;

                _aChess_Anime = _gClass[_iNow_Class_Count].GetComponent<Animator>();
                break;

           
               
            case "Minion":
                Chess.Job = "Minion";
                _gClass[_iNow_Class_Count].SetActive(false);
                _gClass_Card.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(false);
                _gBack_Model.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(false);
                
                //else if(_iNow_Class_Count==2)
                //{
                //    Chess.Max_HP *= 2;
                //    Chess.HP *= 2;
                    
                //}
                _iNow_Class_Count = 0;
                _gClass[_iNow_Class_Count].SetActive(true);
                _gBack_Model.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(true);
                _gClass_Card.transform.GetChild(_iNow_Class_Count).gameObject.SetActive(true);

                if (gameObject.tag == "A")
                {
                    Instantiate(_gTrnasferA_Stone_Effect, _gBack_Model.transform.position, _gTrnasferA_Stone_Effect.transform.rotation);
                }
                else if (gameObject.tag == "B")
                {
                    Instantiate(_gTrnasferB_Stone_Effect, _gBack_Model.transform.position, _gTrnasferB_Stone_Effect.transform.rotation);

                }

                Chess.Attack = 4;
                Chess.Attack_Distance = 1;
                Chess.Walk_Steps = 2;
                _tHead_Name.text = "S" + _tHead_Name.name;
                _tName.text = "S" + _tName.name;
                _mMat = gameObject.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;
                _mStone_Mat = _gBack_Model.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;

                _aChess_Anime = _gClass[_iNow_Class_Count].GetComponent<Animator>();
                break;

        }
    }


    /// <summary>
    /// 印數值在Inspector上面
    /// </summary>
    public void Set_Debug()
    {


        _sJob = Chess.Job;
        _iHP = Chess.HP;
        _iMax_HP = Chess.Max_HP;
        _sHP_Slider.value = _iHP;
        _sHP_Slider.maxValue = _iMax_HP;
        _sNow_State = Chess.Now_State;
        _iAttack = Chess.Attack;
        _iAttack_Distance = Chess.Attack_Distance;
        _bHave_Attacked = Chess.Have_Attacked;
        _bHave_Moved = Chess.Have_Moved;
        _iWalk_Steps = Chess.Walk_Steps;

        _tHP.text = Math.Round(_iHP / _sHP_Slider.maxValue * 100).ToString();
        _sHead_HP.maxValue = _sHP_Slider.maxValue;
        _sHead_HP.value = _sHP_Slider.value;
        _tHead_HP.text = _tHP.text;
    }

    /// <summary>
    /// 設定腳色碰撞血條位置等等
    /// </summary>
    public void Look_At_Camera()
    {
        //Quaternion Tmp_Rota = _g3D_UI.transform.rotation;
        if (_gGameManager.GetComponent<GameManager>()._gMove_Camera.tag == "A")
        {

            if (Chess.Type == "A")
            {
                _g3D_UI.transform.rotation = Quaternion.Euler(60, 180, 0);
                switch(Chess.Job)
                {
                    case "Minion":
                        _g3D_UI.transform.localPosition = new Vector3(0.014f, 1.708f, -0.069f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.8f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.3f;
                        break;
                    case "Warrior":
                        _g3D_UI.transform.localPosition = new Vector3(0.014f, 1.85f, -0.4f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 0.85f, -0.1f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.7f;
                        break;
                    case "Preist":
                        _g3D_UI.transform.localPosition = new Vector3(0.01f, 2.3f, -0.29f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.1f, -0.1f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.18f;
                        gameObject.GetComponent<CapsuleCollider>().height = 2.2f;
                        break;
                    case "Magician":
                        _g3D_UI.transform.localPosition = new Vector3(0.014f, 1.9f, -0.15f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 0.85f, -0.1f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.7f;
                        break;
                    case "Archer":
                        _g3D_UI.transform.localPosition = new Vector3(0.125f, 2.03f, -0.28f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.85f, 0f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 2.2f;
                        break;
                }
            }
            else if (Chess.Type == "B")
            {
                _g3D_UI.transform.rotation = Quaternion.Euler(60, 180, 0);
                switch (Chess.Job)
                {
                    case "Minion":
                        _g3D_UI.transform.localPosition = new Vector3(0.014f, 1.6f, 0.135f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.8f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.3f;
                        break;
                    case "Warrior":
                        _g3D_UI.transform.localPosition = new Vector3(0.014f, 1.85f, -0.4f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 0.85f, -0.1f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.7f;
                        break;
                    case "Preist":
                        _g3D_UI.transform.localPosition = new Vector3(0.168f, 2.3f, 0.6f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.1f, 0.05f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.18f;
                        gameObject.GetComponent<CapsuleCollider>().height = 2.2f;
                        break;
                    case "Magician":
                        _g3D_UI.transform.localPosition = new Vector3(0.014f, 1.9f, -0.15f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 0.85f, -0.1f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.7f;
                        break;
                    case "Archer":
                        _g3D_UI.transform.localPosition = new Vector3(0.125f, 2.03f, -0.28f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.85f, 0f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 2.2f;
                        break;
                }
            }
            // _g3D_UI.transform.rotation = Quaternion.LookRotation(_gGameManager.GetComponent<GameManager>()._gPlayer_One_Camera.transform.position);
            //_g3D_UI.transform.position = _gGameManager.GetComponent<GameManager>()._gPlayer_One_Camera.GetComponent<Camera>().WorldToScreenPoint(_g3D_UI.transform.position);
            //_g3D_UI.transform.LookAt(_gGameManager.GetComponent<GameManager>()._gPlayer_One_Camera.transform);
            //_g3D_UI.transform.rotation = Quaternion.Euler(_g3D_UI.transform.rotation.x, _g3D_UI.transform.rotation.y-180, _g3D_UI.transform.rotation.z );

        }
        else if (_gGameManager.GetComponent<GameManager>()._gMove_Camera.tag == "B")
        {
            if (Chess.Type == "A")
            {
                _g3D_UI.transform.rotation = Quaternion.Euler(60, 0, 0);
                switch (Chess.Job)
                {
                    case "Minion":
                        _g3D_UI.transform.localPosition = new Vector3(0.014f, 1.708f, -0.069f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.8f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.3f;
                        break;
                    case "Warrior":
                        _g3D_UI.transform.localPosition = new Vector3(-0.03f, 1.85f, -0.3f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 0.85f, -0.1f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.7f;
                        break;
                    case "Preist":
                        _g3D_UI.transform.localPosition = new Vector3(-0.15f, 2.39f, -0.5f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.1f, -0.1f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.18f;
                        gameObject.GetComponent<CapsuleCollider>().height = 2.2f;
                        break;
                    case "Magician":
                        _g3D_UI.transform.localPosition = new Vector3(0.014f, 1.9f, -0.15f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 0.85f, -0.1f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.7f;
                        break;
                    case "Archer":
                        _g3D_UI.transform.localPosition = new Vector3(-0.05f, 2.05f, -0.155f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.85f, 0f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 2.2f;
                        break;
                }
            }
            else if (Chess.Type == "B")
            {
                _g3D_UI.transform.rotation = Quaternion.Euler(60, 0, 0);
                switch (Chess.Job)
                {
                    case "Minion":
                        _g3D_UI.transform.localPosition = new Vector3(0.014f, 1.708f, -0.069f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.8f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.3f;
                        break;
                    case "Warrior":
                        _g3D_UI.transform.localPosition = new Vector3(-0.03f, 2.08f, -0.004f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 0.85f, -0.1f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.7f;
                        break;
                    case "Preist":
                        _g3D_UI.transform.localPosition = new Vector3(0.01f, 2.58f, 0.2f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.1f, 0.05f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.18f;
                        gameObject.GetComponent<CapsuleCollider>().height = 2.2f;
                        break;
                    case "Magician":
                        _g3D_UI.transform.localPosition = new Vector3(0.014f, 1.9f, -0.15f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 0.85f, -0.1f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.7f;
                        break;
                    case "Archer":
                        _g3D_UI.transform.localPosition = new Vector3(0.03f, 2.25f, 0.12f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.85f, 0f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 2.2f;
                        break;
                }
            }
            //_g3D_UI.transform.rotation = Quaternion.LookRotation(_gGameManager.GetComponent<GameManager>()._gPlayer_Two_Camera.transform.position);
            // _g3D_UI.transform.rotation = Quaternion.Euler(_g3D_UI.transform.rotation.x, _g3D_UI.transform.rotation.y - 180, _g3D_UI.transform.rotation.z );


        }
    }



    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Magician":
                Destroy(other.gameObject);
                Instantiate(_gEffect[2], gameObject.transform.position, _gEffect[2].transform.rotation);
                char Magi_Damage = other.name[0];
                _gGameManager.GetComponent<GameManager>()._gMove_Camera.transform.parent.GetComponent<CameraShake>().shakeDuration = 0.5f;
                switch (Chess.Job)
                {
                    case "Magician":
                        if (Chess.Now_State == "Defense")
                        {
                            Chess.HP -= ((int)Char.GetNumericValue(Magi_Damage)) / 2;
                        }
                        else
                        {
                            Chess.HP -= ((int)Char.GetNumericValue(Magi_Damage));
                        }
                        break;
                    case "Minion":
                        if (Chess.Now_State == "Defense")
                        {
                            Chess.HP -= ((int)Char.GetNumericValue(Magi_Damage)) / 2;
                        }
                        else
                        {
                            Chess.HP -= ((int)Char.GetNumericValue(Magi_Damage));
                        }
                        break;
                    case "Warrior":
                        if (Chess.Now_State == "Defense")
                        {
                            Chess.HP -= (((int)Char.GetNumericValue(Magi_Damage)) + 2) / 2;
                        }
                        else
                        {
                            Chess.HP -= (((int)Char.GetNumericValue(Magi_Damage)))+2;
                        }
                        break;
                    case "Archer":
                        if (Chess.Now_State == "Defense")
                        {
                            Chess.HP -= (((int)Char.GetNumericValue(Magi_Damage)) - 2)  / 2;
                        }
                        else
                        {
                            Chess.HP -= (((int)Char.GetNumericValue(Magi_Damage))) - 2;
                        }
                        break;
                    case "Preist":
                        if (Chess.Now_State == "Defense")
                        {
                            Chess.HP -= (((int)Char.GetNumericValue(Magi_Damage)) + 2)  / 2;
                        }
                        else
                        {
                            Chess.HP -= (((int)Char.GetNumericValue(Magi_Damage))) + 2;
                        }
                        break;
                }
               

                break;
            case "Archer":
                Destroy(other.gameObject);
                Instantiate(_gEffect[3], gameObject.transform.position, _gEffect[3].transform.rotation);
                char Arrow_Damage = other.name[0];
                _gGameManager.GetComponent<GameManager>()._gMove_Camera.transform.parent.gameObject.GetComponent<CameraShake>().shakeDuration = 0.5f;
                switch (Chess.Job)
                {
                    case "Magician":
                        if (Chess.Now_State == "Defense")
                        {
                            Chess.HP -= (((int)Char.GetNumericValue(Arrow_Damage)) + 2) / 2;
                        }
                        else
                        {
                            Chess.HP -=( ((int)Char.GetNumericValue(Arrow_Damage)))+2;
                        }
                        break;
                    case "Minion":
                        if (Chess.Now_State == "Defense")
                        {
                            Chess.HP -= ((int)Char.GetNumericValue(Arrow_Damage)) / 2;
                        }
                        else
                        {
                            Chess.HP -= ((int)Char.GetNumericValue(Arrow_Damage));
                        }
                        break;
                    case "Warrior":
                        if (Chess.Now_State == "Defense")
                        {
                            Chess.HP -= (((int)Char.GetNumericValue(Arrow_Damage)) - 2) / 2;
                        }
                        else
                        {
                            Chess.HP -= (((int)Char.GetNumericValue(Arrow_Damage))) - 2;
                        }
                        break;
                    case "Archer":
                        if (Chess.Now_State == "Defense")
                        {
                            Chess.HP -= ((int)Char.GetNumericValue(Arrow_Damage)) / 2;
                        }
                        else
                        {
                            Chess.HP -= ((int)Char.GetNumericValue(Arrow_Damage)) ;
                        }
                        break;
                    case "Preist":
                        if (Chess.Now_State == "Defense")
                        {
                            Chess.HP -= (((int)Char.GetNumericValue(Arrow_Damage)) + 2)  / 2;
                        }
                        else
                        {
                            Chess.HP -= (((int)Char.GetNumericValue(Arrow_Damage))) + 2;
                        }
                        break;
                }

                break;
        }
    }






    
}
