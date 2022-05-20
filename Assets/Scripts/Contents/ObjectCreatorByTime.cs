using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreatorByTime : MonoBehaviour
{
    public GameObject createPrefab;

    private float currentCreateTime = 0;
    public float createTime = 5f;

    private void Start()
    {
        currentCreateTime = createTime;
        GameRuleController.Instance.updatePlayDelaTimeEvent.AddListener(UpdateTime);
    }

    public void UpdateTime(float deltaTime)
    {
        currentCreateTime -= deltaTime;
        if (currentCreateTime <= 0)
        {
            currentCreateTime = createTime;
            Create();
        }
    }

    public void Create()
    {
        var go = Instantiate(createPrefab);
        go.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }
}
