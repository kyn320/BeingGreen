using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayHelpData", fileName = "PlayHelpData")]
public class PlayHelpData : ScriptableObject
{
    public Sprite screenShot;
    [TextArea]
    public string description;
}
