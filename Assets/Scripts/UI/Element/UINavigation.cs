using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINavigation : MonoBehaviour
{
    private List<UIBaseView> m_ListNav = new List<UIBaseView>();

    public void Push(UIBaseView uiView)
    {
        m_ListNav.Add(uiView);
    }

    public UIBaseView Pop()
    {
        if (m_ListNav.Count > 0)
        {
            var top = m_ListNav.Count - 1;
            var uiView = m_ListNav[top];
            m_ListNav.RemoveAt(top);
            return uiView;
        }
        else
        {
            return null;
        }
    }

    public void PopAndCloseAll()
    {
        while (m_ListNav.Count > 0)
        {
            UIBaseView uIBase = Pop();
            uIBase.Close();
        }
    }
}
