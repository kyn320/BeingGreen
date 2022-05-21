using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UISceneFadeView : MonoBehaviour
{
    public string TargetScene = "";
    public UI UI;
    public bool IsUIOpenOrClose = true;
    public float FadeTime = 1;

    public void LoadScene(string scene, UI uI, bool isUIOpen, float fadeTime = 1)
    {
        TargetScene = scene;
        UI = uI;
        IsUIOpenOrClose = isUIOpen;
        FadeTime = fadeTime;
        UIManager.Instance.Open(UI.SceneFade);
    }

    public IEnumerator LoadScene()
    {
        yield return SceneManager.LoadSceneAsync(TargetScene);
    }
}
