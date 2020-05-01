using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public GameObject _eSet_Transfer_Area_Sound;
    public GameObject _eCard_Hight;
    public GameObject _eButton_Hight;
    public GameObject _eCursor_Hight;
    public AudioClip _aBattle_Bgm;
    public AudioClip _aVictory_Bgm;
    public UnityEvent _eTransfer_Sound;
    public UnityEvent _eRecall_Sound;
    public UnityEvent _eDissolve;
    public UnityEvent _eMagi_Chant;
    public UnityEvent _eArcher_Shoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Set_Card_Hight(Button button)
    {
        if(button.interactable==true)
        {
            Instantiate(_eCard_Hight);
        }
        
    }
    public void Set_Button_Hight(Button button)
    {
        if (button.interactable == true)
        {
            Instantiate(_eButton_Hight);
        }
    }

    public void Set_Cursor_Hight()
    {
        Instantiate(_eCursor_Hight);
    }

}
