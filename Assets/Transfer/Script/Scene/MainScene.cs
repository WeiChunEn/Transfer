using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public Button _b_Main_StartBtn;
    public Button _b_Main_ExitBtn;
    public Button _b_Main_TutBtn;
    private void Awake()
    {
        
    }

        // Start is called before the first frame update
        void Start()
    {
        _b_Main_StartBtn.onClick.AddListener(() => Start_Btn_Click());
        _b_Main_ExitBtn.onClick.AddListener(() => Exit_Btn_Click());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start_Btn_Click()
    {
        SceneManager.LoadSceneAsync("GameLoading");

    }
    public void Exit_Btn_Click()
    {
        Application.Quit();
    }
}
