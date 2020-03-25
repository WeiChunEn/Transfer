using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Music : MonoBehaviour
{
    public GameObject _eSet_Transfer_Area_Sound;
    public GameObject _eCard_Hight;
    public GameObject _eButton_Hight;
    public AudioClip _aBattle_Bgm;
    public AudioClip _aVictory_Bgm;
    public UnityEvent _eTransfer_Sound;
    public UnityEvent _eRecall_Sound;
    public UnityEvent _eDissolve;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Set_Card_Hight()
    {
        Instantiate(_eCard_Hight);
    }
    public void Set_Button_Hight()
    {
        Instantiate(_eButton_Hight);
    }
}
