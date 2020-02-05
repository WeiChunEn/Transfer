using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    private Color _cTmp_Color; //原本的顏色
    public Vector3 _vDestination; //格子的位置
    public GameObject _gGameManager; //管理器
    public GameObject _gNow_Player; //現在控制的旗子
    public bool _bOn_It;            //是否有在格子上面

    private void Awake()
    {
        _gGameManager = GameObject.Find("GameManager");
    }
    // Start is called before the first frame update
    void Start()
    {
        _cTmp_Color = GetComponent<Renderer>().material.color;
        _vDestination = transform.position;
        _gNow_Player = _gGameManager.GetComponent<Path>()._gPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        //如果在格子上面按左鍵
        if (Input.GetButtonDown("Fire1") && _bOn_It == true)
        {
            _gNow_Player.GetComponent<Move>().Cal_Road();
        }
    }

    private void OnMouseEnter()
    {
        if((_gNow_Player.transform.position.x != _vDestination.x)||(_gNow_Player.transform.position.z != _vDestination.z))
        {
            GetComponent<Renderer>().material.color = Color.black;
            _gNow_Player.GetComponent<Move>()._gGrid = gameObject;
            _bOn_It = true;
        }
      
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = _cTmp_Color;
        _gNow_Player.GetComponent<Move>()._gGrid = null;
        _bOn_It = false;
    }

}
