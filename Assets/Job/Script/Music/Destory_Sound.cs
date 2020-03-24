using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destory_Sound : MonoBehaviour
{
    [SerializeField]
    private float _fStart_Time = 0.0f;
    private float _fDestory_Time = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _fStart_Time += Time.deltaTime;
        if(_fStart_Time>=_fDestory_Time)
        {
            Destroy(gameObject);
        }
    }
}
