using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject _gAttack_Grid; //生成的戰鬥格子
    public GameObject _gGrid_Group; //放格子的位置
    public Path path;        //路徑的Class

    public GameObject _gEffect;
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
        for (int i = 1; i < path._lCan_Attack_List.Count; i++)
        {

            for (int j = 0; j < path._gPlane.transform.childCount; j++)
            {
                if ((path._gPlane.transform.GetChild(j).transform.position.x == path._lCan_Attack_List[i].x) && (path._gPlane.transform.GetChild(j).transform.position.z == path._lCan_Attack_List[i].z))
                {

                    Instantiate(_gAttack_Grid, new Vector3(path._lCan_Attack_List[i].x, 0.01f, path._lCan_Attack_List[i].z), _gAttack_Grid.transform.rotation, _gGrid_Group.transform);
                }

            }

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
        if(gameObject.GetComponent<Character>().Chess.Job!="Preist")
        {
            Enmey.GetComponent<Character>().Chess.HP -= gameObject.GetComponent<Character>().Chess.Attack;
            Instantiate(_gEffect, Enmey.transform.position, _gEffect.transform.rotation);
        }
        
        
    }

    public void Recall_Partner(GameObject Partner)
    {
        if (gameObject.GetComponent<Character>().Chess.Job == "Preist")
        {
            Partner.GetComponent<Character>().Set_Job_Data("Minion");
            Partner.GetComponent<Character>().Chess.HP += gameObject.GetComponent<Character>().Chess.Attack;
            Instantiate(_gEffect, Partner.transform.position, _gEffect.transform.rotation);
        }
        
    }

    
}
