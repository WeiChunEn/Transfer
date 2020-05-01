using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using UnityEngine.UI;


//場景的管理者
//場景State都是在這邊做轉換的


public class SceneStateManager:MonoBehaviour
{


    private ISceneState m_State;//State
    private bool m_RunBegin;//是否載入完成並開始
     static AsyncOperation m_asyncOperation; //載入場景的東東
    private string m_LoadSceneName;
    private Thread newThread;
    public  int Index = 0;
    public GameObject _gLoading;
    public Sprite _sLoading;
    public Sprite[] _sMainToGame;

    public SceneStateManager()
    {
       
    }
    //轉換場景的State
    public void SetState(ISceneState State, string LoadSceneName)
    {
        m_RunBegin = false;
        m_LoadSceneName = LoadSceneName;
        Debug.Log("SetState:" + State.ToString());
        if(LoadSceneName == "GameScene")
        {
            _gLoading = GameObject.Find("Canvas").transform.Find("Loading").gameObject;
            _sLoading = _gLoading.GetComponent<Image>().sprite;
        }
       
        newThread = new Thread(Index_Plus);
        //載入場景
        StartCoroutine("LoadScene");
        // StartCoroutine(LoadScene());
        //LoadScene();
        //newThread = new Thread(LoadScene);
        //newThread.Start();

        //告知前一個State結束
        if (m_State != null)
        {
            m_State.StateEnd();

        }

        //設定新的State
        m_State = State;
    }


   IEnumerator LoadScene()
    {
       
        m_asyncOperation = SceneManager.LoadSceneAsync(m_LoadSceneName);
        m_asyncOperation.allowSceneActivation = false;
        if(m_LoadSceneName == "GameScene")
        {
            newThread.Start();
        }
        
        // m_asyncOperation.allowSceneActivation = true;
        while (!m_asyncOperation.isDone)
        {

           
                if (m_asyncOperation.progress == 0.9f)
            {
                m_asyncOperation.allowSceneActivation = true;
                
            }

            yield return null;
        }
    }

    public void Index_Plus()
    {
        while(true)
        {
            //_sLoading = _sMainToGame[Index];
            Index++;
           
           // _gLoading.GetComponent<Image>().sprite = _gLoading.GetComponent<LoadAnime>()._sMainToGame[Index];
            if (Index>=59)
            {
                Index = 0;
            }
        }
        
    }
    //public void LoadScene()
    //{
    //    m_asyncOperation = SceneManager.LoadSceneAsync(m_LoadSceneName);
    //    m_asyncOperation.allowSceneActivation = false;
    //    //while (!m_asyncOperation.isDone)
    //    //{

    //    //    if (m_asyncOperation.progress >= 0.9)
    //    //    {
    //    //        m_asyncOperation.allowSceneActivation = true;
    //    //    }


    //    //}
    //}
    //State的Update更新
    public void Update()
    {
        StateUpdate();
    }

    public void StateUpdate()
    {
       
        //如果場景還在載入就不去做以下的事情
        if (!m_asyncOperation.isDone)
        {
           
            return;


        }
        
        
        //新的State開始
        if (m_State != null && m_RunBegin == false)
        {
            m_State.StateBegin();
            m_RunBegin = true;

        }

        //若開始完了就持續做更新
        if (m_State != null)
        {
            m_State.StateUpdate();

        }
    }
}
