using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    private GameObject m_Enmey;
    private GameObject m_Partner;
    public List<Vector3> EnmeyPos_List;    //敵人所在的位置List
    public List<Vector3> PartnerPos_List;   //自己人所在的位置List
    public List<Vector3> Can_Move_List;     //能移動的位置List
    public List<int> Move_Weight_List;      //格子權重的Lisv

    //private int m_Move_Steps;               //能移動的步數
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Find_Way()
    {
        int Walk_Steps = gameObject.GetComponent<Character>()._iWalk_Steps;
        Can_Move_List.Add(gameObject.transform.position);
       // for(int i = 0; i<Walk_Steps;i++)
    }



    
    //存取場上所有腳色的位置
    public void Save_CharacterPos()
    {
        for(int i = 0;i<m_Enmey.transform.childCount;i++)
        {
            EnmeyPos_List.Add(m_Enmey.transform.GetChild(i).position);
        }
        for (int i = 0; i < m_Partner.transform.childCount; i++)
        {
            PartnerPos_List.Add(m_Partner.transform.GetChild(i).position);
        }

    }

   
}
