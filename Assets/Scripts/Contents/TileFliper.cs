using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFliper : MonoBehaviour
{
    public float forwardOffset = 0.3f;
    public float tileGroundCheckDistance = 0.5f;

    public float tileRange = 1f;

    private float currentActionTime = 0f;
    public float actionTime = 0.5f;
    protected bool isUpdateTime = false;

    public LayerMask layerMask;

    private void Start()
    {
        isUpdateTime = true;
        currentActionTime = actionTime;

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, tileGroundCheckDistance, layerMask);

        if (hit.collider != null)
        {
            var centerPoint = hit.collider.transform.position;
            transform.position = new Vector3(centerPoint.x, transform.position.y, centerPoint.z);
        }
    }

    private void Update()
    {
        if (!isUpdateTime)
            return;

        currentActionTime -= Time.deltaTime;
        if (currentActionTime <= 0)
        {
            isUpdateTime = false;
            Flip();
            Destroy(gameObject);
        }
    }

    public void Flip()
    {
        var tileList = new List<TileController>();

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, tileGroundCheckDistance, layerMask);

        if (hit.collider != null)
        {
            var centerTileObject = hit.collider.gameObject;
            var centerTile = centerTileObject.GetComponent<TileController>();

            var tileObjects = Physics.OverlapSphere(centerTileObject.transform.position, tileRange);
            for (var i = 0; i < tileObjects.Length; ++i)
            {
                var tileObject = tileObjects[i].gameObject;
                var distanceSqr = (tileObject.transform.position - centerTileObject.transform.position).sqrMagnitude;
                if (distanceSqr <= tileRange * tileRange)
                {
                    var tileController = tileObjects[i].GetComponent<TileController>();
                    if (tileController != null)
                        tileList.Add(tileController);
                }
            }
        }

        for (var i = 0; i < tileList.Count; ++i)
        {
            tileList[i].Flip();
        }
    }
}
