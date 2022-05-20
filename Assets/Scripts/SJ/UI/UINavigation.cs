using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasScaler))]
[RequireComponent(typeof(GraphicRaycaster))]
public class UINavigation : MonoBehaviour
{
    private Canvas m_Canvas = null;
    private CanvasScaler m_CanvasScaler = null;

    private Stack<UIBase> m_StackNav = new Stack<UIBase>();
    private List<UIBase> m_ListNav = new List<UIBase>();

    public Canvas Canvas { get => m_Canvas ?? this.GetOrAddComponent<Canvas>(ref m_Canvas); }
    public CanvasScaler CanvasScaler { get => m_CanvasScaler ?? this.GetOrAddComponent<CanvasScaler>(ref m_CanvasScaler); }

    public void SetCanvas(int sortingOrder)
    {
        Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        Canvas.sortingOrder = sortingOrder;
        CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        CanvasScaler.referenceResolution = new Vector2(1920, 1080);
        CanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        CanvasScaler.matchWidthOrHeight = Screen.width < Screen.height ? 0 : 1;
    }

    public void Push(UIBase ui)
    {
        if (this.name.Equals(UINavType.Top))
        {
            if (!m_ListNav.Contains(ui))
            {
                m_ListNav.Add(ui);
            }
        }
        m_StackNav.Push(ui);
        ui.RectT.SetAsLastSibling();
    }

    public UIBase Pop()
    {
        return m_StackNav.Count > 0 ? m_StackNav.Pop() : null;
    }

    public void PopAndCloseAll()
    {

        int j = m_StackNav.Count;
        Debug.Log(j);
        for (int i = 0; i < j; i++)
        {
            UIBase uIBase = m_StackNav.Pop();
            uIBase.Close();
        }
    }
}
