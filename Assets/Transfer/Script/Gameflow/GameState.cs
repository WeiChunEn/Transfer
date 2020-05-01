using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState 
{
    public string StateName { get; set; } = "GameFlow";

    protected GameStateManager m_GameStateManager = null;

    //Constructer建構子
    public GameState(GameStateManager GameStateManager)
    {
        m_GameStateManager = GameStateManager;
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
        return string.Format("[GameState: StateName = {0}]", StateName);
    }
}
