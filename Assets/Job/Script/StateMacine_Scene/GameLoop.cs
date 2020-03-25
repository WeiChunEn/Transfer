using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public Texture2D _tCursor_tex;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    SceneStateManager m_SceneStateManager = new SceneStateManager();
    

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

        m_SceneStateManager.SetState(new MainState(m_SceneStateManager), "MainScene");
        Cursor.SetCursor(_tCursor_tex, hotSpot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        m_SceneStateManager.StateUpdate();
       
    }
}
