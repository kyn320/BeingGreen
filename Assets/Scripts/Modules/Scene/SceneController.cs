using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class SceneController : Singleton<SceneController>
{
    public void LoadScene(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single, UnityAction endAction = null)
    {
        SceneManager.LoadScene(sceneName, loadMode);
        endAction?.Invoke();
    }

    public async Task LoadSceneAsync(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single, bool allowActive = true ,UnityAction<float> loadAction = null, UnityAction endAction = null)
    {
        var asyncOper = SceneManager.LoadSceneAsync(sceneName,loadMode);
        asyncOper.allowSceneActivation = allowActive;

        do {
            loadAction?.Invoke(asyncOper.progress);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }while (asyncOper.isDone);

        endAction?.Invoke();
    }


}
