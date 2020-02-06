using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneState :ISceneState
{
    public GameSceneState(SceneStateManager StateManager):base(StateManager)
    {
        this.StateName = "GameScene";
        GameManager._sScene_Transfer_End = "Start";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
