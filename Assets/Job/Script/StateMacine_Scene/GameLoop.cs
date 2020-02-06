using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{

    SceneStateManager m_SceneStateManager = new SceneStateManager();
    

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_SceneStateManager.SetState(new MainState(m_SceneStateManager), "MainScene");
        
    }

    // Update is called once per frame
    void Update()
    {
        m_SceneStateManager.StateUpdate();
       
    }
}
