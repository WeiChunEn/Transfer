using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetArea_One :GameState
{
    public GameObject _gStateName;
   
    public SetArea_One(GameStateManager StateManager):base(StateManager)
    {
        this.StateName = "Player1 Set Transfer Area";

    }

    public override void StateBegin()
    {
        
        if(_gStateName==null)
        {
            _gStateName = GameObject.Find("GameState");
        }
        _gStateName.GetComponent<TextMeshProUGUI>().text = StateName;
    }

    public override void StateUpdate()
    {
        Debug.Log("SetAreaUpdate");
        if (GameManager._bSet_Area_Finish_One==true)
        {
            Debug.Log("SetAreaOneFinish");
            m_GameStateManager.Set_GameState(new SetArea_Two(m_GameStateManager));
            GameManager._bSet_Area_Finish_One = false;
        }
    }

    public override void StateEnd()
    {
        Debug.Log("SetAreaOneEnd");
    }

}
