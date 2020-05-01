using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    private Button m_ReStart_Btn;
    private Button m_BackMenu_Btn;
    public Button _bHome_Btn;
    public Button _bSurrender_Btn;
    public GameObject _gMusic_On_Btn;
    public GameObject _gMusic_Off_Btn;
    public Button _bTutor_Btn;
    public Button _bSet_Btn;
    public Animator _aSet_Menu;
    private GameObject _gGameManager;
    public GameObject _gTutor;
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager._sScene_Transfer_End = "Start";
        _gGameManager = GameObject.Find("GameManager");
        m_ReStart_Btn = _gGameManager.GetComponent<GameManager>()._bNew_Game_Btn;
        m_BackMenu_Btn = _gGameManager.GetComponent<GameManager>()._bMain_Menu_Btn;
        m_ReStart_Btn.onClick.AddListener(() => Restart_Btn_Click());
        m_BackMenu_Btn.onClick.AddListener(() => Back_MainMenu_Btn_Click());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Restart_Btn_Click()
    {

        Reset_Data();
        SceneManager.LoadSceneAsync("GameLoading");

    }
    public void Back_MainMenu_Btn_Click()
    {
        SceneManager.LoadSceneAsync("MainLoading");

    }

    public void Music_On()
    {
        _gMusic_On_Btn.SetActive(false);
        AudioListener.volume = 1;
        //gameObject.GetComponent<AudioListener>().volum = true;
        _gMusic_Off_Btn.SetActive(true);

    }
    public void Music_Off()
    {
        _gMusic_Off_Btn.SetActive(false);
        AudioListener.volume = 0;
       // gameObject.GetComponent<AudioListener>().enabled = false;
        _gMusic_On_Btn.SetActive(true);


    }

    public void Tutor_Cli()
    {
        _gTutor.SetActive(false);
    }

    public void Surronder()
    {
        if(GameManager._sPlayer_Two_Finish == "Start")
        {
            for(int i = 0; i < _gGameManager.GetComponent<GameManager>()._gPlayer2.transform.childCount;i++)
            {
                _gGameManager.GetComponent<GameManager>()._gPlayer2.transform.GetChild(i).GetComponent<Character>().Chess.HP = 0;
            }
            
        }
        else if(GameManager._sPlayer_One_Finish == "Start")
        {
            for (int i = 0; i < _gGameManager.GetComponent<GameManager>()._gPlayer1.transform.childCount; i++)
            {
                _gGameManager.GetComponent<GameManager>()._gPlayer1.transform.GetChild(i).GetComponent<Character>().Chess.HP = 0;
            }

        }
    }
    public void Set_Btn()
    {
        if (_aSet_Menu.gameObject.tag == "In")
        {
            _aSet_Menu.SetTrigger("Out");
            _aSet_Menu.gameObject.tag = "Out";
        }
        else if(_aSet_Menu.gameObject.tag == "Out")
        {
            _aSet_Menu.SetTrigger("In");
            _aSet_Menu.gameObject.tag = "In";
        }
       
    }
    public void Reset_Data()
    {
        GameManager._iPlayer1_Transfer_Area_Count = 3;
        GameManager._iPlayer2_Transfer_Area_Count = 3;
        GameManager._sScene_Transfer_End = null;
        GameManager._sSet_Area_Finish_One = null;
        GameManager._sSet_Area_Finish_Two = null;
        GameManager._sPlayer_One_Finish = null;
        GameManager._sPlayer_Two_Finish = null;
    }
}
