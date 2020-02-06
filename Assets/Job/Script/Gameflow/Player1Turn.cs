using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player1Turn : GameState
{
    public GameObject _gStateName;
    public Player1Turn(GameStateManager StateManager) : base(StateManager)
    {
        this.StateName = "Player1 Turn";
        Debug.Log("Player1 Turn Start");
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
        if (GameManager._sPlayer_One_Finish == "End")
        {
            m_GameStateManager.Set_GameState(new Player2Turn(m_GameStateManager));
            GameManager._sPlayer_Two_Finish = "Start";
        }
    }

}
