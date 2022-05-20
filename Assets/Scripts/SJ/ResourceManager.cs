using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.U2D;
using UnityEngine.Events;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] private List<GameObject> m_ListUI = new List<GameObject>();
    private Dictionary<string, GameObject> m_DicUI = new Dictionary<string, GameObject>();

    public List<GameObject> ListUI { get => m_ListUI; set => m_ListUI = value; }
    public Dictionary<string, GameObject> DicUI { get => m_DicUI; set => m_DicUI = value; }

    protected override void Awake()
    {
        Init();
    }

    public ResourceManager Init()
    {
        m_ListUI.ForEach((a) => DicUI.Add(a.name, a));
        return ResourceManager.Instance;
    }

    public GameObject LoadUI(UI ui)
    {
        string uiName = $"P_UI{ui}";

        if (DicUI.ContainsKey(uiName))
        {
            return DicUI[uiName];
        }
        else
        {
            Debug.LogError($"{uiName} is null.");
            return null;
        }
    }

}