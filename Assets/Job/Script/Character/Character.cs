using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string _sType;
    public string _sJob;
    public int _iWalk_Steps;
    public int _iAttack_Distance;
    public int _iHP;
    public int _iMP;
    public int _iAttack;
    public class Character_Data
    {
        protected string m_Type;
        protected string m_Job;
        protected int m_Walk_Steps;
        protected int m_Attack_Distance;
        protected int m_HP;
        protected int m_MP;
        protected int m_Attack;
    }

    public class Set_Character : Character_Data
    {
        private int _hP;
        private int _mP;
        private int _attack;
        private int _attack_Distance;
        private int _walk_Steps;
        private string _job;

        public Set_Character(string Type, string Job, int Walk_Steps, int Attack_Distance, int HP, int MP, int Attack)
        {
            this.m_Type = Type;
            this.m_Job = Job;
            this.m_Walk_Steps = Walk_Steps;
            this.m_Attack_Distance = Attack_Distance;
            this.m_HP = HP;
            this.m_MP = MP;
            this.m_Attack = Attack;
        }
        public string Type => m_Type;
        public string Job { get => _job; set => _job = value; }
        public int Walk_Steps { get => _walk_Steps; set => _walk_Steps = value; }
        public int Attack_Distance { get => _attack_Distance; set => _attack_Distance = value; }
        public int HP { get => this.m_HP; set => m_HP = value; }
        public int MP { get => _mP; set => _mP = value; }
        public int Attack { get => this.m_Attack; set => m_Attack = value; }

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
        _iAttack = 3;
    }
    // Start is called before the first frame update
    void Start()
    {
        Chess = new Set_Character(_sType, _sJob, _iWalk_Steps, _iAttack_Distance,_iHP,_iMP,_iAttack);
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Chess.HP);
        Set_Debug();
        if (Chess.HP<0)
        {
            Destroy(gameObject);
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
        
        
       
        _iHP = Chess.HP;
    }
}
