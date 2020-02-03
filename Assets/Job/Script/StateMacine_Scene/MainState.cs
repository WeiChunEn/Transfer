using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : ISceneState
{
    public MainState(SceneStateManager StateManager):base(StateManager)
    {
        this.StateName = "MainState";
    }

    public override void StateBegin()
    {
        Debug.Log(123);
    }
    public override void StateUpdate()
    {
        
    }
    public override void StateEnd()
    {
        
    }
}
