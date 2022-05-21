using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class BaseUtil
{

    public static T GetOrAddComponent<T>(this MonoBehaviour mono, ref T t) where T : Component
    {
        t = mono.GetComponent<T>();
        if (t == null)
            t = mono.gameObject.AddComponent<T>();

        return t;
    }
}
