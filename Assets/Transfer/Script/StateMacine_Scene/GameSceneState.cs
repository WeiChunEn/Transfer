using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneState :ISceneState
{
    private Button m_ReStart_Btn;
    private Button m_BackMenu_Btn;
    private GameObject _gGameManager;
    public GameSceneState(SceneStateManager StateManager):base(StateManager)
    {
        
        this.StateName = "GameScene";
        GameManager._sScene_Transfer_End = "Start";
    }
    public override void StateBegin()
    {
        _gGameManager = GameObject.Find("GameManager");
        m_ReStart_Btn = _gGameManager.GetComponent<GameManager>()._bNew_Game_Btn;
        m_BackMenu_Btn = _gGameManager.GetComponent<GameManager>()._bMain_Menu_Btn;
        m_ReStart_Btn.onClick.AddListener(() => Restart_Btn_Click());
        m_BackMenu_Btn.onClick.AddListener(() => Back_MainMenu_Btn_Click());
    }

    public void Restart_Btn_Click()
    {

        Reset_Data();
        m_StateManager.SetState(new GameSceneState(m_StateManager), "GameScene");
        
    }
    public void Back_MainMenu_Btn_Click()
    {
        m_StateManager.SetState(new MainState(m_StateManager), "MainScene");
    }

    public void Reset_Data()
    {
        GameManager._iPlayer1_Transfer_Area_Count = 3;
        GameManager._iPlayer2_Transfer_Area_Count = 3;
        GameManager._sScene_Transfer_End = null;
        GameManager._sSet_Area_Finish_One = null;
        GameManager._sSet_Area_Finish_Two = null;
        GameManager._sPlayer_One_Finish = null;
        GameManager._sPlayer_Two_Finish = null;
    }
}
