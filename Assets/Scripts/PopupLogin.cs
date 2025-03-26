using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupLogin : MonoBehaviour
{
    public TMP_InputField inputID; // ���̵�
    public TMP_InputField inputPassword; // ���
    public Button loginBtn; // �α��� ��ư
    public Button signUpBtn; // ȸ������ ��ư

    private void Start()
    {
        loginBtn.onClick.AddListener(OnLoginButtonClick); // �α��� ��ư Ŭ��
    }

    // �α��� ��ư�� ������ �α����� �ٷ� ����
    public void OnLoginButtonClick()
    {
        GameManager.Instance.LoginSuccess();
    }
}
