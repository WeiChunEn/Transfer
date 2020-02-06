using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string _sSet_Area_Finish_One;
    public static string _sSet_Area_Finish_Two;
    public static string _sPlayer_One_Finish;
    public static string _sPlayer_Two_Finish;
    public static string _sGame_Start;
    public static string _sGame_End;
    public static string _sScene_Transfer_End;
    public static int _iPlayer1_Transfer_Area_Count;
    public static int _iPlayer2_Transfer_Area_Count;
    GameStateManager m_GameStateManager = new GameStateManager();

    public GameObject _gPlayer1;
    public GameObject _gPlayer2;
    private GameObject m_NowPlayer;
    // Start is called before the first frame update
    void Start()
    {
        _iPlayer1_Transfer_Area_Count = 3;
        _iPlayer2_Transfer_Area_Count = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(_sScene_Transfer_End=="Start")
        {
            m_GameStateManager.Set_GameState(new SetArea_One(m_GameStateManager));
            _sScene_Transfer_End = "End";
            _sSet_Area_Finish_One = "Start";
            //_sGame_Start = "Start";
        }
        m_GameStateManager.GameStateUpdate();

    }

    public void Select_Player(int index)
    {
        
        if (GameManager._sPlayer_One_Finish=="Start")
        {
            gameObject.GetComponent<Path>().Reset_List();
            gameObject.GetComponent<Path>()._gPlayer = _gPlayer1.transform.GetChild(index).gameObject;
            gameObject.GetComponent<Path>().Save_CharacterPos();
            gameObject.GetComponent<Path>().Find_Way();
            m_NowPlayer = _gPlayer1.transform.GetChild(index).gameObject;
        }
        else if(GameManager._sPlayer_Two_Finish=="Start")
        {
            
            gameObject.GetComponent<Path>().Reset_List();
            gameObject.GetComponent<Path>()._gPlayer = _gPlayer2.transform.GetChild(index).gameObject;
            gameObject.GetComponent<Path>().Save_CharacterPos();
            gameObject.GetComponent<Path>().Find_Way();
            m_NowPlayer = _gPlayer2.transform.GetChild(index).gameObject;
        }
    }

    public void Move_Btn_Click()
    {
        m_NowPlayer.GetComponent<Move>().Instant();
    }
}
