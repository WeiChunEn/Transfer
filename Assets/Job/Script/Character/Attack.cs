using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject _gAttack_Grid; //生成的戰鬥格子
    public GameObject _gGrid_Group; //放格子的位置
    public Path path;        //路徑的Class

    public GameObject[] _gEffect = new GameObject[5]; //攻擊的特效
    public GameObject[] _gBallet = new GameObject[4];   //遠程攻擊的物品
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

            //for (int j = 0; j < path._gPlane.transform.childCount; j++)
            //{
            //if ((path._gPlane.transform.GetChild(j).transform.position.x == path._lCan_Attack_List[i].x) && (path._gPlane.transform.GetChild(j).transform.position.z == path._lCan_Attack_List[i].z))
            //{
            if (gameObject.transform.position.x != path._lCan_Attack_List[i].x || gameObject.transform.position.z != path._lCan_Attack_List[i].z)
            {
                Instantiate(_gAttack_Grid, new Vector3(path._lCan_Attack_List[i].x, 0.01f, path._lCan_Attack_List[i].z), _gAttack_Grid.transform.rotation, _gGrid_Group.transform);
            }
            //}

            // }

        }
        //for (int i = 0; i < path._lCan_Attack_List.Count; i++)
        //{

        //    Instantiate(_gAttack_Grid, new Vector3(path._lCan_Attack_List[i].x, 0.01f, path._lCan_Attack_List[i].z), _gAttack_Grid.transform.rotation, _gGrid_Group.transform);
        //}
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
        Debug.Log(1232);
        _iJob_Num = gameObject.GetComponent<Character>()._iNow_Class_Count;
        switch(gameObject.GetComponent<Character>().Chess.Job)
        {
            case "Minion":
                Enmey.GetComponent<Character>().Chess.HP -= gameObject.GetComponent<Character>().Chess.Attack;
                Instantiate(_gEffect[_iJob_Num], Enmey.transform.position, _gEffect[_iJob_Num].transform.rotation);
                break;
            case "Magician":
               
                GameObject Tmp_obj;
                Tmp_obj = _gBallet[_iJob_Num];
                Tmp_obj.name = gameObject.GetComponent<Character>().Chess.Attack.ToString();
                Tmp_obj.tag = gameObject.GetComponent<Character>().Chess.Job;
                Instantiate(Tmp_obj, new Vector3(Enmey.transform.position.x, Enmey.transform.position.y+3.0f, Enmey.transform.position.z), Tmp_obj.transform.rotation);
               
                break;
            case "Warrior":
               
                path._lCan_Attack_Enmey.Remove(Enmey);
                Enmey.GetComponent<Character>().Chess.HP -= gameObject.GetComponent<Character>().Chess.Attack;
                Instantiate(_gEffect[_iJob_Num], Enmey.transform.position, _gEffect[_iJob_Num].transform.rotation);
                if (path._lCan_Attack_Enmey.Count == 0)
                {
                    Destory_AttackGrid();
                    path.gameObject.GetComponent<GameManager>().Set_Character_Btn();
                    path.gameObject.GetComponent<GameManager>().In_And_Out();
                }
                break;
        }
        
        
        
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
           
            Partner.GetComponent<Character>().Set_Job_Data("Minion");
            Partner.GetComponent<Character>().Chess.HP += gameObject.GetComponent<Character>().Chess.Attack;
            Instantiate(_gEffect[_iJob_Num], Partner.transform.position, _gEffect[_iJob_Num].transform.rotation);
        }
        
    }

    
}
