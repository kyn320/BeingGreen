using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class WorldController : MonoBehaviour
{
    public GameObject tilePrefab;
    public Vector3 tileSize;

    public Vector2Int mapSize;

    public TileController[] tileControllers;

    [Button("월드(타일) 생성")]
    public void CreateWorld()
    {
        tileControllers = new TileController[mapSize.x * mapSize.y];

        var startOffset = new Vector2(-mapSize.x * 0.5f * (tileSize.x) + tileSize.x * 0.5f
            , -mapSize.y * 0.5f * (tileSize.z) + tileSize.z * 0.5f);

        var index = 0;

        for (var y = 0; y < mapSize.y; ++y)
        {
            for (var x = 0; x < mapSize.x; ++x)
            {
                var tileGo = Instantiate(tilePrefab, transform);
                var tileController = tileGo.GetComponent<TileController>();
                tileGo.transform.localPosition = new Vector3(startOffset.x + x * tileSize.x
                    ,0
                    ,startOffset.y + y * tileSize.z);
                tileControllers[index] = tileController;
                ++index;
            }
        }
    }

    [Button("랜덤 타일 세팅")]
    public void SetRandomTile()
    {
        var ownerList = new List<int>();
        var maxCount = mapSize.x * mapSize.y;

        for (var i = 0; i < maxCount; ++i)
        {
            ownerList.Add(0);
            ownerList.Add(1);
        }

        for (var i = 0; i < maxCount; ++i)
        {
            var randOwnerDataIndex = Random.Range(0, ownerList.Count);
            var owner = ownerList[randOwnerDataIndex];

            tileControllers[i].Initialize(i, owner);

            ownerList.RemoveAt(randOwnerDataIndex);
        }

    }

}
