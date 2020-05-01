using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : ISceneState
{
    public StartState(SceneStateManager StateManager):base(StateManager)
    {
        this.StateName = "StartState";
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
