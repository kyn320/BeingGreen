using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIPlayerTimer : MonoBehaviour
{
    private int remainCount = 0;
    [SerializeField]
    protected List<GameObject> timeSlotList;

    private float currentTime;
    [SerializeField]
    protected float tickTime;

    private void Start()
    {
        remainCount = timeSlotList.Count;
        currentTime = tickTime;
    }

    public void UpdateDeltaTime(float deltaTime)
    {
        currentTime -= deltaTime;

        if (currentTime <= 0)
        {
            currentTime = tickTime;
            timeSlotList[remainCount - 1].SetActive(false);
            --remainCount;

            if (remainCount <= 0)
            {
                remainCount = timeSlotList.Count;
                ResetTimeSlot();
            }
        }
    }

    public void ResetTimeSlot()
    {
        for (var i = 0; i < timeSlotList.Count; ++i)
        {
            timeSlotList[i].SetActive(true);
        }
    }

}
