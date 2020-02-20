using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player2Turn : GameState
{ 
    public GameObject _gStateName;
    private GameObject _gGameManager;
    public Player2Turn(GameStateManager StateManager) : base(StateManager)
    {
        this.StateName = "";
        Debug.Log("Player2 Turn Start");
    }

    public override void StateBegin()
    {
        if (_gStateName == null)
        {
            _gStateName = GameObject.Find("GameState");
        }
        _gStateName.GetComponent<TextMeshProUGUI>().text = StateName;
        _gGameManager = GameObject.Find("GameManager");
        
    }
    public override void StateUpdate()
    {
        if (GameManager._sPlayer_Two_Finish == "End")
        {
            m_GameStateManager.Set_GameState(new Player1Turn(m_GameStateManager));
            GameManager._sPlayer_One_Finish = "Start";
            _gGameManager.GetComponent<GameManager>().Set_Now_Team();
        }
    }
}
