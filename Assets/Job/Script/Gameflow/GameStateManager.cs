using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager 
{
    public GameState m_GameState;

    private bool m_RunBegin;//是否載入完成並開始
    public GameStateManager()
    {

    }

    //切換遊戲目前的狀態
    public void Set_GameState(GameState State )
    {
        m_RunBegin = false;
        Debug.Log("SetState:" + State.ToString());
        if (m_GameState != null)
        {
            
            m_GameState.StateEnd();

        }
        m_GameState = State;
        m_GameState.StateBegin();
        m_RunBegin = true;
        Debug.Log(m_GameState);
    }

    public void GameStateUpdate()
    {
        Debug.Log(m_GameState);
        //新的State開始
        //if (m_GameState != null && m_RunBegin == false)
        //{
        //    Debug.Log(m_GameState);
        //    m_GameState.StateBegin();
        //    m_RunBegin = true;

        //}

        //若開始完了就持續做更新
        if (m_GameState != null&& m_RunBegin == true)
        {
            m_GameState.StateUpdate();

        }
    }
}
