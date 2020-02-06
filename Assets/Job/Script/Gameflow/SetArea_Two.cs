using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetArea_Two : GameState
{
    public GameObject _gStateName;
    public SetArea_Two(GameStateManager StateManager) : base(StateManager)
    {
        this.StateName = "Player2 Set Transfer Area";

    }

    public override void StateBegin()
    {
        if (_gStateName == null)
        {
            _gStateName = GameObject.Find("GameState");
        }
        _gStateName.GetComponent<TextMeshProUGUI>().text = StateName;
    }
}
