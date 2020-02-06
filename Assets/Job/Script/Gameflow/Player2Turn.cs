using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player2Turn : GameState
{ 
    public GameObject _gStateName;
    public Player2Turn(GameStateManager StateManager) : base(StateManager)
    {
        this.StateName = "Player2 Turn";
        Debug.Log("Player2 Turn Start");
    }

    public override void StateBegin()
    {
        if (_gStateName == null)
        {
            _gStateName = GameObject.Find("GameState");
        }
        _gStateName.GetComponent<TextMeshProUGUI>().text = StateName;
    }
    public override void StateUpdate()
    {
        if (GameManager._sPlayer_Two_Finish == "End")
        {
            m_GameStateManager.Set_GameState(new Player1Turn(m_GameStateManager));
            GameManager._sPlayer_One_Finish = "Start";
        }
    }
}
