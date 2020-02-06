using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainState : ISceneState
{
    private Button m_StartBtn;
    private Button m_ExitBtn;
    
    public MainState(SceneStateManager StateManager):base(StateManager)
    {
        this.StateName = "MainState";
       
        
    }

    public override void StateBegin()
    {
        m_StartBtn = GameObject.Find("StartBtn").GetComponent<Button>();
        m_ExitBtn = GameObject.Find("ExitBtn").GetComponent<Button>();
        m_StartBtn.onClick.AddListener(() => Start_Btn_Click());
        m_ExitBtn.onClick.AddListener(() => Exit_Btn_Click());
    }
    public override void StateUpdate()
    {
       
    }
    public override void StateEnd()
    {
        
    }

    public void Start_Btn_Click()
    {
        m_StateManager.SetState(new GameSceneState(m_StateManager), "GameScene");
    }
    public void Exit_Btn_Click()
    {
        Application.Quit();
    }
}
