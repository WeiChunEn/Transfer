using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player1Turn : GameState
{
    public GameObject _gStateName;
    private GameObject _gGameManager;
    public Player1Turn(GameStateManager StateManager) : base(StateManager)
    {
        this.StateName = "Player1 Turn Start";
        Debug.Log("Player1 Turn Start");
    }

    public override void StateBegin()
    {
        if (_gStateName == null)
        {
            _gStateName = GameObject.Find("GameState");
        }
       
        _gGameManager = GameObject.Find("GameManager");
        _gGameManager.GetComponent<GameManager>()._gNow_State_UI[1].SetActive(false);


    }
    public override void StateUpdate()
    {
       
        if (GameManager._sPlayer_One_Finish == "End")
        {
            m_GameStateManager.Set_GameState(new Player2Turn(m_GameStateManager));
            GameManager._sPlayer_Two_Finish = "Start";
            _gGameManager.GetComponent<GameManager>().Set_Now_Team();
        }
    }

}
