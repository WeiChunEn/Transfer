using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    private AsyncOperation async = null;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadScene()
    {
        yield return new  WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "MainLoading")
        {
            async = SceneManager.LoadSceneAsync("MainScene");
        }
        else if (SceneManager.GetActiveScene().name == "GameLoading")
        {
            async = SceneManager.LoadSceneAsync("GameScene");
        }

        async.allowSceneActivation = false;
        while (!async.isDone)
        {

            if(async.progress==0.9f)
            {
                yield return new WaitForSeconds(1.0f);
                async.allowSceneActivation = true;
            }
            
            yield return new WaitForEndOfFrame();
        }

       
    }


}

