using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointEffector : MonoBehaviour
{
    public RectTransform mousePointArea;
    public GameObject mouseClickVFX;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var go = Instantiate(mouseClickVFX, UIController.Instance.rootCanvas.transform);
            var localPoint = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(mousePointArea, Input.mousePosition, UIController.Instance.CanvaCamra, out localPoint);
            go.GetComponent<RectTransform>().localPosition = localPoint;
        }
    }

}
