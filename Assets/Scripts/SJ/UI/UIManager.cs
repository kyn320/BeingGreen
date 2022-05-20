using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<UI, UIBase> m_DicUI = new Dictionary<UI, UIBase>();
    private Dictionary<UINavType, UINavigation> m_DicNav = new Dictionary<UINavType, UINavigation>();
    private UINavigation m_CurrentNav = null;
    private UIBase m_CurrentUIBase = null;
    private UI m_CurrentUI = UI.None;
    private LinkedList<UIBase> m_LListUIBase = new LinkedList<UIBase>();

    public Dictionary<UINavType, UINavigation> DicNav { get => m_DicNav; set => m_DicNav = value; }
    public UI CurrentUI { get => m_CurrentUI; set => m_CurrentUI = value; }
    public LinkedList<UIBase> LListUIBase { get => m_LListUIBase; set => m_LListUIBase = value; }

    protected override void Awake()
    {
        Init();
    }

    private void Start()
    {
        Open(UI.Title);
    }

    private void Init()
    {
        InitNavigation();
    }

    private void InitNavigation()
    {

        for (int i = 0; i < System.Enum.GetValues(typeof(UINavType)).Length; i++)
        {

            GameObject go = new GameObject(((UINavType)i).ToString());
            go.transform.SetParent(this.transform, false);
            UINavigation uiNav = go.AddComponent<UINavigation>();
            uiNav.SetCanvas(i);
            DicNav.Add((UINavType)i, uiNav);
        }

        m_CurrentNav = DicNav[UINavType.Normal];
    }

    public void Open(UI uI)
    {

        if (uI.Equals(UI.None)) return;

        if (m_DicUI.ContainsKey(uI))
        {
            PushUINavigation(uI, m_DicUI[uI], true);
        }
        else
        {
            PushUINavigation(uI, InstantiateUIBase(uI), false);
        }
    }

    public void OpenDefaultActivity(UI uI)
    {
        CloseAll();
        Open(uI);
    }

    public void Close(UI uI)
    {

        if (uI.Equals(UI.None)) return;

        UIBase uIBase = null;

        if (m_DicUI.ContainsKey(uI))
        {
            if (m_DicUI[uI].NavType == UINavType.Top)
            {
                uIBase = m_DicUI[uI];
                uIBase?.Close();
            }
            else
            {
                Close(m_DicUI[uI]);
            }
        }
    }

    public void CloseBack()
    {
        Close(LListUIBase.Last.Value);
    }

    public void CloseAll()
    {
        foreach (var a in DicNav)
        {
            if (!a.Key.Equals(UINavType.Top))
            {
                a.Value.PopAndCloseAll();
            }
        }

        LListUIBase.Clear();
    }

    public void HideAll()
    {
        foreach (var a in LListUIBase)
        {
            a.gameObject.SetActive(false);
        }
    }

    public void ShowAll()
    {
        foreach (var a in LListUIBase)
        {
            a.gameObject.SetActive(true);
        }
    }

    private void Close(UIBase uIBase)
    {
        if (LListUIBase.Last.Previous != null)
        {
            LListUIBase.RemoveLast();

            DicNav[uIBase.NavType].Pop();
            uIBase?.Close();
            CurrentUI = LListUIBase.Last.Value.UI;
            m_CurrentUIBase = LListUIBase.Last.Value;

            if (!IsUIOpened(CurrentUI))
            {
                m_CurrentUIBase.gameObject.SetActive(true);
            }
        }
    }

    private UIBase InstantiateUIBase(UI uI)
    {
        UIBase uIBase = Instantiate(ResourceManager.Instance.LoadUI(uI)).GetComponent<UIBase>();
        return uIBase;
    }

    private void PushUINavigation(UI uI, UIBase uIbase, bool isCached)
    {

        if (CurrentUI != uI)
        {

            if (uIbase.NavType != UINavType.Top)
            {
                CurrentUI = uI;
                m_CurrentUIBase = uIbase;
                LListUIBase.AddLast(uIbase);
            }

            uIbase.Open();
            uIbase.name = uI.ToString();
            uIbase.transform.SetParent(DicNav[uIbase.NavType].transform, false);

            if (uIbase.NavType != UINavType.Top)
            {
                DicNav[uIbase.NavType].Push(uIbase);
            }

            if (!isCached) if (!m_DicUI.ContainsKey(uI)) m_DicUI.Add(uI, uIbase);

            uIbase.UI = uI;

        }

    }

    public bool IsUIOpened(UI ui)
    {
        if (m_DicUI.ContainsKey(ui))
        {
            return m_DicUI[ui].gameObject.activeSelf;
        }
        else
        {
            return false;
        }
    }

}
