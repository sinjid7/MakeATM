using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public PopupBank popupBank;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenPopupBank()
    {
        popupBank.UpdatePopupUI();
    }


    void Start()
    {
        OpenPopupBank();
    }    

}
