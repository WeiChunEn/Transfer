using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoadState : ISceneState
{
    public GameLoadState(SceneStateManager StateManager) : base(StateManager)
    {
        this.StateName = "GameLoadState";
    }

    public override void StateBegin()
    {

    }

    public override void StateUpdate()
    {
        //  m_StateManager.SetState(new MainState(m_StateManager), "MainScene");
    }

    public override void StateEnd()
    {

    }
}
