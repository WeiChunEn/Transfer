using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetArea_One :GameState
{
    public GameObject _gStateName;
    private GameObject _gGameManager;
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
        Debug.Log(_gStateName.GetComponent<TextMeshProUGUI>().text);
        _gGameManager = GameObject.Find("GameManager");
    }

    public override void StateUpdate()
    {
        if(_gStateName==null)
        {
            _gStateName = GameObject.Find("GameState");
            
        }
        _gGameManager = GameObject.Find("GameManager");
        _gStateName.GetComponent<TextMeshProUGUI>().text = StateName;
        Debug.Log("SetAreaUpdate");
        if(GameManager._sSet_Area_Finish_One == "Start")
        {
           
           if( GameManager._iPlayer1_Transfer_Area_Count == 0)
            {
                GameManager._sSet_Area_Finish_One = "End";
                _gGameManager.GetComponent<GameManager>().Camera_Move_Anim();
                //_gGameManager.GetComponent<GameManager>()._gPlayer_One_Camera.SetActive(false);
                //_gGameManager.GetComponent<GameManager>()._gPlayer_Two_Camera.SetActive(true);

            }

        }
        else if (GameManager._sSet_Area_Finish_One=="End")
        {
            Debug.Log("SetAreaOneFinish");
            m_GameStateManager.Set_GameState(new SetArea_Two(m_GameStateManager));
            GameManager._sSet_Area_Finish_Two = "Start";
        }
    }

    public override void StateEnd()
    {
        Debug.Log("SetAreaOneEnd");
        

    }

}
