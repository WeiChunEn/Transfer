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
    public int _iMP;
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
    public int _iNow_Class_Count;
    public GameObject[] _gClass = new GameObject[4]; //其他職業的模型
    public Sprite[] _gClass_Card = new Sprite[4]; //其他職業的卡牌圖

    public GameObject _gTransfer_Effect;
    public Material _mDeath_Mat;

    public GameObject[] _gEffect = new GameObject[5]; //被攻擊以及攻擊的特效
    public class Character_Data
    {
        protected string m_Type;
        protected string m_Job;
        protected int m_Walk_Steps;
        protected int m_Attack_Distance;
        protected int m_HP;
        protected int m_MP;
        protected int m_Attack;
        protected string m_Now_State;
        protected bool m_Have_Moved;
        protected bool m_Have_Attacked;
    }

    public class Set_Character : Character_Data
    {


        public Set_Character(string Type, string Job, int Walk_Steps, int Attack_Distance, int HP, int MP, int Attack, string Now_State)
        {
            this.m_Type = Type;
            this.m_Job = Job;
            this.m_Walk_Steps = Walk_Steps;
            this.m_Attack_Distance = Attack_Distance;
            this.m_HP = HP;
            this.m_MP = MP;
            this.m_Attack = Attack;
            this.m_Now_State = Now_State;
        }
        public string Type => m_Type;
        public string Job { get => m_Job; set => m_Job = value; }
        public int Walk_Steps { get => m_Walk_Steps; set => m_Walk_Steps = value; }
        public int Attack_Distance { get => m_Attack_Distance; set => m_Attack_Distance = value; }
        public int HP
        {
            get { return this.m_HP; }
            set
            {
                m_HP = value;

                if (m_HP < 0)
                { m_HP = 0; }
                else if(m_HP>=20)
                {
                    m_HP = 20;
                }
            }

        }
        public int MP { get => m_MP; set => m_MP = value; }
        public int Attack { get => this.m_Attack; set => m_Attack = value; }

        public string Now_State { get => m_Now_State; set => m_Now_State = value; }
        public bool Have_Moved { get => m_Have_Moved; set => m_Have_Moved = value; }
        public bool Have_Attacked { get => m_Have_Attacked; set => m_Have_Attacked = value; }

    }



    public Set_Character Chess;

    public virtual void Awake()
    {
        if(_gPlayer_UI.GetComponent<Image>().sprite.name=="Preist_B"|| _gPlayer_UI.GetComponent<Image>().sprite.name == "Preist_A")
        {
            _sType = gameObject.tag;
            _sJob = "Preist";
            _iWalk_Steps = 2;
            _iAttack_Distance = 1;
            _iHP = 15;
            _iMP = 5;
            _iAttack = 5;
            _sNow_State = "Idle";
            _sHP_Slider.maxValue = _iHP;
            _tHP.text = ((_iHP / _sHP_Slider.maxValue) *100).ToString();
            
            _sHead_HP.maxValue = _sHP_Slider.maxValue;
            _tHead_HP.text = _tHP.text;
            _iNow_Class_Count = 4;
            _mDeath_Mat = gameObject.transform.GetChild(_iNow_Class_Count).transform.GetChild(1).GetComponent<Renderer>().material;
            //_gClass[_iNow_Class_Count].SetActive(true)

        }
        else
        {
            _sType = gameObject.tag;
            _sJob = "Minion";
            _iWalk_Steps = 2;
            _iAttack_Distance = 1;
            _iHP = 20;
            _iMP = 5;
            _iAttack = 2;
            _sNow_State = "Idle";
            _sHP_Slider.maxValue = _iHP;
            _tHP.text = ((_iHP / _sHP_Slider.maxValue) * 100).ToString();
            _sHead_HP.maxValue = _sHP_Slider.maxValue;
            _tHead_HP.text = _tHP.text;
            _iNow_Class_Count = 0;
            _gClass[_iNow_Class_Count].SetActive(true);
            _gPlayer_UI.GetComponent<Image>().sprite = _gClass_Card[_iNow_Class_Count];
            _mDeath_Mat = gameObject.transform.GetChild(_iNow_Class_Count).transform.GetChild(1).GetComponent<Renderer>().material;
        }
        
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        Chess = new Set_Character(_sType, _sJob, _iWalk_Steps, _iAttack_Distance, _iHP, _iMP, _iAttack, _sNow_State);

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
        if (Chess.HP <= 0)
        {

            _Death_Time += Time.deltaTime * 0.5f;
            _mDeath_Mat.SetFloat("_DissolveCutoff", _Death_Time);
           
            if (_Death_Time >= 1.0)
            {
                gameObject.SetActive(false);
                gameObject.transform.position = new Vector3(100, 100, 100);
                Chess.Now_State = "Death";
            }
            
            _gPlayer_UI.GetComponent<Button>().interactable = false;

            
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
                _iNow_Class_Count = 1;
                _gClass[_iNow_Class_Count].SetActive(true);
                _gPlayer_UI.GetComponent<Image>().sprite = _gClass_Card[_iNow_Class_Count];
                Chess.Attack = 5;
                Chess.Attack_Distance = 3;
                Chess.Walk_Steps = 3;
                _tHead_Name.text = "W" + _tHead_Name.name;
                _tName.text = "W" + _tName.name;
                break;
            case "Magician":
                Chess.Job = "Magician";
                _gClass[_iNow_Class_Count].SetActive(false);
                _iNow_Class_Count = 2;
                _gClass[_iNow_Class_Count].SetActive(true);
                _gPlayer_UI.GetComponent<Image>().sprite = _gClass_Card[_iNow_Class_Count];
                Chess.Attack = 8;
                Chess.Attack_Distance = 2;
                Chess.Walk_Steps = 1;
                _tHead_Name.text = "M" + _tHead_Name.name;
                _tName.text = "M" + _tName.name;
                _mDeath_Mat = gameObject.transform.GetChild(_iNow_Class_Count).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material;
                break;
            case "Archor":
                _gClass[_iNow_Class_Count].SetActive(false);
                _iNow_Class_Count = 3;
                _gClass[_iNow_Class_Count].SetActive(true);
                _gPlayer_UI.GetComponent<Image>().sprite = _gClass_Card[_iNow_Class_Count];
                Chess.Attack = 6;
                Chess.Attack_Distance = 3;
                Chess.Walk_Steps = 2;
                _tHead_Name.text = "A" + _tHead_Name.name;
                _tName.text = "A" + _tName.name;
                break;

           
               
            case "Minion":
                Chess.Job = "Minion";
                _gClass[_iNow_Class_Count].SetActive(false);
                _iNow_Class_Count = 0;
                _gClass[_iNow_Class_Count].SetActive(true);
                _gPlayer_UI.GetComponent<Image>().sprite = _gClass_Card[_iNow_Class_Count];
                Chess.Attack = 3;
                Chess.Attack_Distance = 1;
                Chess.Walk_Steps = 2;
                _tHead_Name.text = "S" + _tHead_Name.name;
                _tName.text = "S" + _tName.name;
                _mDeath_Mat = gameObject.transform.GetChild(_iNow_Class_Count).transform.GetChild(1).GetComponent<Renderer>().material;
                break;

        }
    }


    public void Set_Debug()
    {


        _sJob = Chess.Job;
        _iHP = Chess.HP;
        _sHP_Slider.value = _iHP;
        _sNow_State = Chess.Now_State;
        _iAttack = Chess.Attack;
        _iAttack_Distance = Chess.Attack_Distance;
        _bHave_Attacked = Chess.Have_Attacked;
        _bHave_Moved = Chess.Have_Moved;
        _iWalk_Steps = Chess.Walk_Steps;

        _tHP.text = Math.Round(_iHP / _sHP_Slider.maxValue * 100).ToString();
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
                        _g3D_UI.transform.localPosition = new Vector3(0.1006802f, 1.464f, 0.051f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.937f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 0.658f;
                        break;
                    case "Warrior":
                        _g3D_UI.transform.localPosition = new Vector3(-0.06f, 2.0f, 0.185f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 1.22f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.25f;
                        break;
                    case "Priest":
                        _g3D_UI.transform.localPosition = new Vector3(0.049f, 1.92f, -0.071f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.2f, -0.05f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.18f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.2f;
                        break;
                    case "Magician":
                        _g3D_UI.transform.localPosition = new Vector3(0.1f, 2.3f, -0.005f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.5f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.4f;
                        break;
                }
            }
            else if (Chess.Type == "B")
            {
                _g3D_UI.transform.rotation = Quaternion.Euler(60, 180, 0);
                switch (Chess.Job)
                {
                    case "Minion":
                        _g3D_UI.transform.localPosition = new Vector3(0.1006802f, 1.464f, 0.051f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.937f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 0.658f;
                        break;
                    case "Warrior":
                        _g3D_UI.transform.localPosition = new Vector3(-0.06f, 2.0f, 0.185f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 1.22f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.25f;
                        break;
                    case "Preist":
                        _g3D_UI.transform.localPosition = new Vector3(0.049f, 1.92f, -0.071f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.2f, -0.05f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.18f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.2f;
                        break;
                    case "Magician":
                        _g3D_UI.transform.localPosition = new Vector3(-0.022f, 2.3f, -0.005f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.5f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.4f;
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
                        _g3D_UI.transform.localPosition = new Vector3(0.1006802f, 1.464f, 0.051f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.937f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 0.658f;
                        break;
                    case "Warrior":
                        _g3D_UI.transform.localPosition = new Vector3(-0.06f, 2.0f, 0.185f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 1.22f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.25f;
                        break;
                    case "Preist":
                        _g3D_UI.transform.localPosition = new Vector3(0.049f, 1.92f, -0.071f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.2f, -0.05f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.18f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.2f;
                        break;
                    case "Magician":
                        _g3D_UI.transform.localPosition = new Vector3(0.1f, 2.3f, -0.005f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.5f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.4f;
                        break;
                }
            }
            else if (Chess.Type == "B")
            {
                _g3D_UI.transform.rotation = Quaternion.Euler(60, 0, 0);
                switch (Chess.Job)
                {
                    case "Minion":
                        _g3D_UI.transform.localPosition = new Vector3(0.1006802f, 1.464f, 0.051f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 0.937f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 0.658f;
                        break;
                    case "Warrior":
                        _g3D_UI.transform.localPosition = new Vector3(-0.06f, 2.0f, 0.185f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.001f, 1.22f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.25f;
                        break;
                    case "Preist":
                        _g3D_UI.transform.localPosition = new Vector3(0.049f, 1.92f, -0.071f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.2f, -0.05f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.18f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.2f;
                        break;
                    case "Magician":
                        _g3D_UI.transform.localPosition = new Vector3(-0.022f, 2.3f, -0.005f);
                        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.01f, 1.5f, 0.02f);
                        gameObject.GetComponent<CapsuleCollider>().radius = 0.304f;
                        gameObject.GetComponent<CapsuleCollider>().height = 1.4f;
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
                char Damage = other.name[0];
                _gGameManager.GetComponent<GameManager>()._gMove_Camera.transform.GetChild(0).GetComponent<CameraShake>().shakeDuration = 0.5f;
                Chess.HP -= (int)Char.GetNumericValue(Damage);
                
                break;
        }
    }
}
