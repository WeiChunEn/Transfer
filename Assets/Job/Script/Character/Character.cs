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


    public GameObject _gGameManager; //管理器
    public Slider _sHead_HP;        //頭上的血條Slider
    public TextMeshProUGUI _tHead_HP; //頭上的血條%
    public GameObject _g3D_UI;
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
            }

        }
        public int MP { get => m_MP; set => m_MP = value; }
        public int Attack { get => this.m_Attack; set => m_Attack = value; }

        public string Now_State { get => m_Now_State; set => m_Now_State = value; }
        public bool Have_Moved { get => m_Have_Moved; set => m_Have_Moved = value; }
        public bool Have_Attacked { get => m_Have_Attacked; set => m_Have_Attacked = value; }

    }



    public Set_Character Chess;

    public void Awake()
    {
        _sType = gameObject.tag;
        _sJob = "Minion";
        _iWalk_Steps = 2;
        _iAttack_Distance = 1;
        _iHP = 10;
        _iMP = 5;
        _iAttack = 6;
        _sNow_State = "Idle";
        _sHP_Slider.maxValue = _iHP;
        _tHP.text = (_iHP / 10.0f * 100).ToString();
        _sHead_HP.maxValue = _sHP_Slider.maxValue;
        _tHead_HP.text = _tHP.text;
    }
    // Start is called before the first frame update
    void Start()
    {
        Chess = new Set_Character(_sType, _sJob, _iWalk_Steps, _iAttack_Distance, _iHP, _iMP, _iAttack, _sNow_State);

    }

    // Update is called once per frame
    void Update()
    {

        Set_Debug();
        Look_At_Camera();
        if (Chess.HP <= 0)
        {
            _gPlayer_UI.GetComponent<Button>().interactable = false;
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector3(100, 100, 100);
            Chess.Now_State = "Death";
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

                break;
            case "Archor":

                break;

            case "Magician":
                break;
            case "Minion":
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
        
        _tHP.text = (_iHP / 10.0 * 100).ToString();
        _sHead_HP.value = _sHP_Slider.value;
        _tHead_HP.text = _tHP.text;
    }

    public void Look_At_Camera()
    {
        //Quaternion Tmp_Rota = _g3D_UI.transform.rotation;
        if (_gGameManager.GetComponent<GameManager>()._gPlayer_One_Camera.activeSelf == true)
        {

            if (Chess.Type == "A")
            {
                _g3D_UI.transform.rotation = Quaternion.Euler(60, 180, 0);
            }
            else if (Chess.Type == "B")
            {
                _g3D_UI.transform.rotation = Quaternion.Euler(60, 180, 0);
            }
            // _g3D_UI.transform.rotation = Quaternion.LookRotation(_gGameManager.GetComponent<GameManager>()._gPlayer_One_Camera.transform.position);
            //_g3D_UI.transform.position = _gGameManager.GetComponent<GameManager>()._gPlayer_One_Camera.GetComponent<Camera>().WorldToScreenPoint(_g3D_UI.transform.position);
            //_g3D_UI.transform.LookAt(_gGameManager.GetComponent<GameManager>()._gPlayer_One_Camera.transform);
            //_g3D_UI.transform.rotation = Quaternion.Euler(_g3D_UI.transform.rotation.x, _g3D_UI.transform.rotation.y-180, _g3D_UI.transform.rotation.z );

        }
        else if (_gGameManager.GetComponent<GameManager>()._gPlayer_Two_Camera.activeSelf == true)
        {
            if (Chess.Type == "A")
            {
                _g3D_UI.transform.rotation = Quaternion.Euler(60, 0, 0);
            }
            else if (Chess.Type == "B")
            {
                _g3D_UI.transform.rotation = Quaternion.Euler(60, 0, 0);
            }
            //_g3D_UI.transform.rotation = Quaternion.LookRotation(_gGameManager.GetComponent<GameManager>()._gPlayer_Two_Camera.transform.position);
            // _g3D_UI.transform.rotation = Quaternion.Euler(_g3D_UI.transform.rotation.x, _g3D_UI.transform.rotation.y - 180, _g3D_UI.transform.rotation.z );


        }
    }
}
