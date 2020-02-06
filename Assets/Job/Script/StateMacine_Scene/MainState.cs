using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : ISceneState
{
    GameStateManager m_GameStateManager = new GameStateManager();
    public MainState(SceneStateManager StateManager):base(StateManager)
    {
        this.StateName = "MainState";
        m_GameStateManager.Set_GameState(new SetArea_One(m_GameStateManager));
    }

    public override void StateBegin()
    {
        
    }
    public override void StateUpdate()
    {
        m_GameStateManager.GameStateUpdate();
    }
    public override void StateEnd()
    {
        Debug.Log(123);
    }
}
