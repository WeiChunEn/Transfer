using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool _bSet_Area_Finish_One;
    public static bool _bSet_Area_Finish_Two;
    public static bool _bPlayer_One_Finish;
    public static bool _bPlayer_Two_Finish;
    public static bool _bGame_Start;
    public static bool _bGame_End;
    public static bool _bScene_Transfer_End;
    GameStateManager m_GameStateManager = new GameStateManager();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_bScene_Transfer_End==true)
        {
            m_GameStateManager.Set_GameState(new SetArea_One(m_GameStateManager));
            _bScene_Transfer_End = false;
        }
        m_GameStateManager.GameStateUpdate();

    }
}
