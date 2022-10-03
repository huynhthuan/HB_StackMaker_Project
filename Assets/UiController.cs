using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UILayer
{
    LOADING = 0,
    END_LEVEL = 1,
    NO_NEW_LEVEL = 2
}

public class UiController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uiLayerList;
    public UILayer UILayer;

    public void ShowUI(UILayer uiIndex)
    {
        HideAllUI();
        uiLayerList[(int)uiIndex].SetActive(true);
    }

    public void HideUI(UILayer uiIndex)
    {
        uiLayerList[(int)uiIndex].SetActive(false);
    }

    public void HideAllUI()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
