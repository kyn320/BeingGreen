using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class WorldController : MonoBehaviour
{
    public GameObject tilePrefab;
    public Vector3 tileSize;

    public Vector2Int mapSize;

    public TileController[] tileControllers;

    public int[] ownerTileCount;
    public UnityEvent<int, int> updateOwnerTileCountEvent;

    [SerializeField]
    private BoxCollider[] outBoxColliders;

    [Button("월드(타일) 생성")]
    public void CreateWorld()
    {
        outBoxColliders[0].center = new Vector3(mapSize.x * 0.5f + 0.5f, 0, 0);
        outBoxColliders[0].size = new Vector3(1, 10f, mapSize.y);

        outBoxColliders[1].center = new Vector3(-mapSize.x * 0.5f - 0.5f, 0, 0);
        outBoxColliders[1].size = new Vector3(1, 10f, -mapSize.y);

        outBoxColliders[2].center = new Vector3(0, 0, mapSize.y * 0.5f + 0.5f);
        outBoxColliders[2].size = new Vector3(mapSize.x, 10f, 1f);

        outBoxColliders[3].center = new Vector3(0, 0, -mapSize.y * 0.5f - 0.5f);
        outBoxColliders[3].size = new Vector3(-mapSize.x, 10f, 1f);


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
                    , 0
                    , startOffset.y + y * tileSize.z);
                tileControllers[index] = tileController;
                tileController.flipEvent.AddListener(UpdateTileOwner);
                ++index;
            }
        }
    }

    [Button("랜덤 타일 세팅")]
    public void SetRandomTile()
    {
        var ownerList = new List<int>();
        var maxCount = mapSize.x * mapSize.y;

        for (var i = 0; i < maxCount / 2; ++i)
        {
            ownerList.Add(0);
            ownerList.Add(1);
        }

        for (var i = 0; i < maxCount; ++i)
        {
            var randOwnerDataIndex = Random.Range(0, ownerList.Count);
            var owner = ownerList[randOwnerDataIndex];

            ++ownerTileCount[owner];
            tileControllers[i].Initialize(i, owner);

            ownerList.RemoveAt(randOwnerDataIndex);
        }
    }

    private void UpdateTileOwner(int index, int owner)
    {
        ++ownerTileCount[owner];
        --ownerTileCount[(owner + 1) % 2];
        updateOwnerTileCountEvent.Invoke(index, owner);
    }

    public int[] GetOwnerTileCount()
    {
        return ownerTileCount;
    }

}
