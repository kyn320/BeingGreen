using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITargetFollower : MonoBehaviour
{
    public Transform target;

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMax = Vector3.zero;
        rectTransform.anchorMin = Vector3.zero;
        rectTransform.pivot = Vector2.one * 0.5f;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            var camera = Camera.main; //UIController.Instance.CanvaCamra;
            var targetPos = RectTransformUtility.WorldToScreenPoint(camera, target.position);

            rectTransform.anchoredPosition = targetPos;
        }
    }


}
