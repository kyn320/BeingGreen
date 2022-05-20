using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class BaseUtil
{
    public static void SetListener(this Button btn, MonoBehaviour behaviour, UnityAction action, bool needSound = true)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(action);
        // if (needSound)
        // btn.onClick.AddListener(() => GameManager.Audio.PlayEffect("SD_Button"));
    }

    public static Dictionary<string, T> BindObject<T>(this Dictionary<string, T> dicT, MonoBehaviour mono, string separation) where T : UnityEngine.Object
    {
        foreach (var item in mono.GetComponentsInChildren<T>(true))
            if (item.name.IndexOf(separation) != -1)
                dicT.Add(item.name, item);

        return dicT;
    }

    public static T GetOrAddComponent<T>(this MonoBehaviour mono, ref T t) where T : Component
    {
        t = mono.GetComponent<T>();
        if (t == null)
            t = mono.gameObject.AddComponent<T>();

        return t;
    }
}
