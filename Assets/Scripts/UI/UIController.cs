using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIController : Singleton<UIController>
{
    public Camera CanvaCamra { get { return rootCanvas?.worldCamera; } }
    public Canvas rootCanvas;
    public Transform viewGroup;

    //[SerializeField]
    //protected RectTransform popupBackgorund;
    public Transform popupGroup;

    public List<UIBaseView> viewList;
    public int popupViewCount = 0;

    public UIBaseView OpenView(UIBaseView view)
    {
        viewList.Add(view);

        view.Init(null);
        view.Open();

        return view;
    }

    public UIBaseView OpenView(UIBaseView view, UIData uiData)
    {
        viewList.Add(view);

        view.Init(uiData);
        view.Open();

        return view;
    }

    public UIBasePopup OpenPopup(string popupName)
    {
        var popupPrefab = Resources.Load<GameObject>($"UI/UI{popupName}Popup");
        var popupObject = Instantiate(popupPrefab, popupGroup);
        var view = popupObject.GetComponent<UIBaseView>();
        ++popupViewCount;
        OpenView(view);
        //popupBackgorund.gameObject.SetActive(popupViewCount > 0);


        return view as UIBasePopup;
    }

    public UIBasePopup OpenPopup(UIPopupData popupData)
    {
        var popupPrefab = Resources.Load<GameObject>(popupData.prefabPath);
        var popupObject = Instantiate(popupPrefab, popupGroup);
        var view = popupObject.GetComponent<UIBaseView>();
        ++popupViewCount;
        OpenView(view, popupData);
        //popupBackgorund.gameObject.SetActive(popupViewCount > 0);

        return view as UIBasePopup;
    }

    public void CloseView(UIBaseView view)
    {
        view.Close();
        viewList.Remove(view);
    }

    public void ClosePopup(string popupName)
    {
        var view = viewList.Find(item => item.gameObject.name.Equals(popupName));
        --popupViewCount;
        CloseView(view);
        //popupBackgorund.gameObject.SetActive(popupViewCount > 0);
    }

    public void ClosePopup(UIBasePopup view)
    {
        --popupViewCount;
        CloseView(view);
        //popupBackgorund.gameObject.SetActive(popupViewCount > 0);
    }

    public void ClosePopup(UIPopupData popupData)
    {
        var view = viewList.Find(item => item.viewName.Equals(popupData.viewName));
        --popupViewCount;
        CloseView(view);
        //popupBackgorund.gameObject.SetActive(popupViewCount > 0);
    }

}
