using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Text))]
public class UIBaseText : MonoBehaviour
{
    [ShowInInspector]
    [ReadOnly]
    private Text text;

    [SerializeField]
    protected string frontAdditionalText = "";
    [SerializeField]
    protected string backAdditionalText = "";

    [Button("¿ÀÅä Ä³½Ì")]
    public void AutoCaching()
    {
        text = GetComponent<Text>();
    }

    private void Awake()
    {
        AutoCaching();
    }

    public void SetText(string context)
    {
        if (text == null)
            AutoCaching();

        text.text = frontAdditionalText + context + backAdditionalText;
    }

}
