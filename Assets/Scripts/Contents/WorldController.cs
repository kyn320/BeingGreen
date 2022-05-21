using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class WorldController : MonoBehaviour
{
    [FoldoutGroup("Tile")]
    public GameObject tilePrefab;
    [FoldoutGroup("Tile")]
    public Vector3 tileSize;

    [FoldoutGroup("Tile")]
    public Vector2Int mapSize;

    [FoldoutGroup("Tile")]
    public TileController[] tileControllers;

    [FoldoutGroup("Tile")]
    public int[] ownerTileCount;
    [FoldoutGroup("Tile")]
    public UnityEvent<int, int> updateOwnerTileCountEvent;
    [FoldoutGroup("Tile")]
    public UnityEvent<int, int, int> updateTotalTileCountEvent;
    [FoldoutGroup("Tile")]
    public UnityEvent<int, int, int> updateTotalBingoCountEvent;
    [FoldoutGroup("Tile")]
    public UnityEvent updateBingoEvent;

    [FoldoutGroup("Wall")]
    [SerializeField]
    private BoxCollider[] outBoxColliders;

    public Transform[] playerSpawnPoint;

    [Button("월드(타일) 생성")]
    public void CreateWorld()
    {
        CreateWall();
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

        playerSpawnPoint[0] = tileControllers[0].transform;
        playerSpawnPoint[1] = tileControllers[mapSize.x * mapSize.y - 1].transform;
    }

    public void CreateWall()
    {
        outBoxColliders[0].center = new Vector3(mapSize.x * 0.5f + 0.5f, 0, 0);
        outBoxColliders[0].size = new Vector3(1, 10f, mapSize.y);

        outBoxColliders[1].center = new Vector3(-mapSize.x * 0.5f - 0.5f, 0, 0);
        outBoxColliders[1].size = new Vector3(1, 10f, -mapSize.y);

        outBoxColliders[2].center = new Vector3(0, 0, mapSize.y * 0.5f + 0.5f);
        outBoxColliders[2].size = new Vector3(mapSize.x, 10f, 1f);

        outBoxColliders[3].center = new Vector3(0, 0, -mapSize.y * 0.5f - 0.5f);
        outBoxColliders[3].size = new Vector3(-mapSize.x, 10f, 1f);
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

        updateTotalTileCountEvent?.Invoke(ownerTileCount[0], ownerTileCount[1], mapSize.x * mapSize.y);
    }

    private void UpdateTileOwner(int index, int owner)
    {
        ++ownerTileCount[owner];
        --ownerTileCount[(owner + 1) % 2];

        updateOwnerTileCountEvent?.Invoke(index, owner);
        updateTotalTileCountEvent?.Invoke(ownerTileCount[0], ownerTileCount[1], mapSize.x * mapSize.y);
        CheckBingoByIndex(index, owner);
    }

    public void FlipAllTile(int targetOwner)
    {
        for (var i = 0; i < tileControllers.Length; ++i)
        {
            if (targetOwner != tileControllers[i].GetOwner())
            {
                tileControllers[i].Flip();
            }
        }
    }

    public void FillAllSize()
    {
        for (var i = 0; i < tileControllers.Length; ++i)
        {
            tileControllers[i].FillScale();
        }
    }


    public void CheckBingoByIndex(int index, int owner)
    {
        var col = GetColNum(index);
        var startX = col * mapSize.x;
        var endX = col * mapSize.x + mapSize.x;

        //index Count가 mapSize.x와 같으면 빙고 처리
        var indexXList = new List<int>();
        for (var x = startX; x < endX; ++x)
        {
            if (tileControllers[x].GetOwner() == owner)
            {
                indexXList.Add(x);
            }
        }

        if (indexXList.Count == mapSize.x)
        {
            for (var i = 0; i < indexXList.Count; ++i)
            {
                tileControllers[indexXList[i]].SetBingo(true);
            }
            updateBingoEvent?.Invoke();
        }

        var startY = index - col * mapSize.x;
        var endY = index + (mapSize.y - col) * mapSize.x;
        //index Count가 mapSize.y와 같으면 빙고 처리
        var indexYList = new List<int>();
        for (var y = startY; y < endY; y += mapSize.x)
        {
            if (tileControllers[y].GetOwner() == owner)
            {
                indexYList.Add(y);
            }
        }

        if (indexYList.Count == mapSize.y)
        {
            for (var i = 0; i < indexYList.Count; ++i)
            {
                tileControllers[indexYList[i]].SetBingo(true);
            }
            updateBingoEvent?.Invoke();
        }

        var p1BingoCount = 0;
        var p2BingoCount = 0;

        for (var i = 0; i < tileControllers.Length; ++i)
        {
            var tileController = tileControllers[i];

            if (tileController.isBingo)
            {
                if (tileController.GetOwner() == 0)
                {
                    ++p1BingoCount;
                }
                else
                {
                    ++p2BingoCount;
                }
            }
        }

        updateTotalBingoCountEvent?.Invoke(p1BingoCount, p2BingoCount, tileControllers.Length);
    }

    public Vector3 GetSpawnPoint(int owner)
    {
        return playerSpawnPoint[owner].position;
    }

    public int GetColNum(int index)
    {
        for (var i = 0; i < mapSize.y; ++i)
        {
            if (index < (i + 1) * mapSize.x)
                return i;
        }
        return -1;
    }

    public int[] GetOwnerTileCount()
    {
        return ownerTileCount;
    }

}
