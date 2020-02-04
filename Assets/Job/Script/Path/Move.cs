using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{
    [SerializeField]
    private int m_Tmp_Weight;  //暫存權重的
    [SerializeField]
    private int m_ShortRoad_Index; //來存最短路徑的Index


    public List<Vector3> _lShort_Road = new List<Vector3>(); //最短路徑的List
    public Path path;
    public GameObject _gTest;
    float timee;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        timee += Time.deltaTime;
        if(timee>=5)
        {
            OeDown();
            timee = -100;
        }
        

    }

    public void OeDown()
    {

        Vector3 NowPos = _gTest.transform.position; //儲存現在的位置
        for (int i = 0; i < path._iList_Count; i++)
        {
            Debug.Log(122);
            //判斷點擊的位置是否有在可以移動的陣列裡面
            if ((NowPos.x == path._lCan_Move_List[i].x) && (NowPos.z == path._lCan_Move_List[i].z))
            {
                
                m_Tmp_Weight = path._lMove_Weight_List[i];
                _lShort_Road.Insert(m_ShortRoad_Index, path._lCan_Move_List[i]); //把這格存到最短路徑裡面
                m_ShortRoad_Index++; //存取用的Index+1
            }

        }
        while ((NowPos.x != path._lCan_Move_List[0].x || NowPos.z != path._lCan_Move_List[0].z))
        {
            for (int i = 0; i < path._iList_Count; i++)
            {
                //向上比
                if ((NowPos.z + 1 == path._lCan_Move_List[i].z) && (NowPos.x == path._lCan_Move_List[i].x))
                {
                    Cmp_Weight(i);
                }

                //向下比
                if ((NowPos.z - 1 == path._lCan_Move_List[i].z) && (NowPos.x == path._lCan_Move_List[i].x))
                {
                    Cmp_Weight(i);
                }

                //向左比
                if ((NowPos.x - 1 == path._lCan_Move_List[i].x) && (NowPos.z == path._lCan_Move_List[i].z))
                {
                    Cmp_Weight(i);
                }

                //向右比
                if ((NowPos.x + 1 == path._lCan_Move_List[i].x) && (NowPos.z == path._lCan_Move_List[i].z))
                {
                    Cmp_Weight(i);
                }
            }
            NowPos = _lShort_Road[m_ShortRoad_Index];
            m_ShortRoad_Index++;
        }

    }

    /// <summary>
    /// 比較四個方向的權重值
    /// </summary>
    /// <param name="Road_Weight">格子的權重值</param>
    public void Cmp_Weight(int Road_Weight)
    {
        if (path._lMove_Weight_List[Road_Weight] > m_Tmp_Weight)
        {
            m_Tmp_Weight = path._lMove_Weight_List[Road_Weight];
            _lShort_Road.Insert(m_ShortRoad_Index, path._lCan_Move_List[Road_Weight]);
        }
    }
}
