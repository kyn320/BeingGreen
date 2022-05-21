using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TileBingoData", fileName = "TileBingoData")]
public class TileBingoData : ScriptableObject
{
    public List<GameObject> grassBingoTileList;
    public List<GameObject> sandBingoTileList;

    public GameObject GetRandomTilePrefab(int owner)
    {
        List<GameObject> tilePrefabList = null;

        switch (owner)
        {
            case 0:
                tilePrefabList = grassBingoTileList;
                break;
            case 1:
                tilePrefabList = sandBingoTileList;
                break;
        }

        return tilePrefabList[Random.Range(0, tilePrefabList.Count)];
    }

}
