using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISceneState 
{
    public string StateName { get; set; } = "ISceneState";

    protected SceneStateManager m_StateManager = null;

    //Constructer建構子
    public ISceneState(SceneStateManager StateManager)
    {
        m_StateManager = StateManager;
    }

    //State開始
    public virtual void StateBegin()
    { }

    //State更新
    public virtual void StateUpdate()
    { }

    //State結束
    public virtual void StateEnd()
    { }

    //覆寫ToString方便Debug
    public override string ToString()
    {
        return string.Format("[I_SceneState: StateName = {0}]", StateName);
    }
}
