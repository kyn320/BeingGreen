using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Context : Singleton<Context>
{
    private UIView m_UIView;
    public UIView UIView { get => m_UIView ?? this.GetOrAddComponent<UIView>(ref m_UIView); }
}
