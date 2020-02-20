using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetArea_Two : GameState
{
    public GameObject _gStateName;
    private GameObject _gGameManager;
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
        _gGameManager = GameObject.Find("GameManager");
        //_gGameManager.GetComponent<GameManager>()._gNow_State_UI[0].SetActive(false);
        //_gGameManager.GetComponent<GameManager>()._gNow_State_UI[1].SetActive(true);
    }
    public override void StateUpdate()
    {
        
        if (GameManager._sSet_Area_Finish_Two == "Start")
        {
            if(GameManager._iPlayer2_Transfer_Area_Count == 0)
            {
                GameManager._sSet_Area_Finish_Two = "End";
                _gGameManager.GetComponent<GameManager>().Camera_Move_Anim();
                
                //_gGameManager.GetComponent<GameManager>()._gPlayer_Two_Camera.SetActive(false);
                //_gGameManager.GetComponent<GameManager>()._gPlayer_One_Camera.SetActive(true);
            }
           
        }
        else if(GameManager._sSet_Area_Finish_Two == "End")
        {
            m_GameStateManager.Set_GameState(new Player1Turn(m_GameStateManager));
            GameManager._sPlayer_One_Finish = "Start";
            _gGameManager.GetComponent<GameManager>().Set_Now_Team();
        }
    }
}
