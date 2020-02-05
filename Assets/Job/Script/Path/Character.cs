using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string _sType;
    public string _sJob;
    public int _iWalk_Steps;
    public int _iAttack_Distance;
    public class Character_Data
    {
        protected string m_Type;
        protected string m_Job;
        protected int m_Walk_Steps;
        protected int m_Attack_Distance;

    }

    public class Set_Character : Character_Data
    {
        public Set_Character(string Type, string Job, int Walk_Steps, int Attack_Distance)
        {
            this.m_Type = Type;
            this.m_Job = Job;
            this.m_Walk_Steps = Walk_Steps;
            this.m_Attack_Distance = Attack_Distance;
        }
        public string Type => m_Type;
        public string Job => m_Job;
        public int Walk_Steps => m_Walk_Steps;
        public int Attack_Distance => m_Attack_Distance;

    }



    public Set_Character Chess;

    public void Awake()
    {
        _sType = gameObject.tag;
        _sJob = "Minion";
        _iWalk_Steps = 5;
        _iAttack_Distance = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        Chess = new Set_Character(_sType, _sJob, _iWalk_Steps, _iAttack_Distance);

    }

    // Update is called once per frame
    void Update()
    {

    }


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
}
