using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterData", fileName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite standIllust;
    public Sprite icon;
    public GameObject renderPrefab;
}
