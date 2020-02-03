using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//場景的管理者
//場景State都是在這邊做轉換的
public class SceneStateManager
{
    private ISceneState m_State;//State
    private bool m_RunBegin;//是否載入完成並開始
    private AsyncOperation m_asyncOperation; //載入場景的東東
    public SceneStateManager()
    {

    }

    //轉換場景的State
    public void SetState(ISceneState State,string LoadSceneName)
    {
        m_RunBegin = false;

        Debug.Log("SetState:" + State.ToString());

        //載入場景
        LoadScene(LoadSceneName); 
        
        //告知前一個State結束
        if(m_State !=null)
        {
            m_State.StateEnd();

        }

        //設定新的State
        m_State = State;
    }

    //呼叫載入場景
    public void LoadScene(string LoadSceneName)
    {
        m_asyncOperation = SceneManager.LoadSceneAsync(LoadSceneName);
    }


    //State的Update更新
    public void StateUpdate()
    {
        //如果場景還在載入就不去做以下的事情
        if(!m_asyncOperation.isDone)
        {
            return;
        }

        //新的State開始
        if(m_State !=null&&m_RunBegin==false)
        {
            m_State.StateBegin();
            m_RunBegin = true;

        }

        //若開始完了就持續做更新
        if(m_State!=null)
        {
            m_State.StateUpdate();

        }
    }
}
