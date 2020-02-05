using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    private Color _cTmp_Color;
    public Vector3 _vDestination;
    public GameObject _gGameManager;
    public GameObject _gNow_Player;
    public bool _bOn_It;

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
        if (Input.GetButtonDown("Fire1") && _bOn_It == true)
        {
            _gNow_Player.GetComponent<Move>().Cal_Road();
        }
    }

    private void OnMouseEnter()
    {

        GetComponent<Renderer>().material.color = Color.black;
        _gNow_Player.GetComponent<Move>()._gGrid = gameObject;
        _bOn_It = true;
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = _cTmp_Color;
        _gNow_Player.GetComponent<Move>()._gGrid = null;
        _bOn_It = false;
    }

}
