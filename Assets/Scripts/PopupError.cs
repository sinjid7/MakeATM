using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupError : MonoBehaviour
{
    public GameObject popupError;
    public Button closePopupBtn;

    private void Start()
    {
        closePopupBtn.onClick.AddListener(ClosePopupError);
        popupError.SetActive(false);
    }

    public void ShowPopupError()
    {
        popupError.SetActive(true);
    }

    public void ClosePopupError()
    {
        popupError.SetActive(false);
    }
}
