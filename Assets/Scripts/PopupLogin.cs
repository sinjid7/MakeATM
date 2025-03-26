using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupLogin : MonoBehaviour
{
    public TMP_InputField inputID; // 아이디
    public TMP_InputField inputPassword; // 비번
    public Button loginBtn; // 로그인 버튼
    public Button signUpBtn; // 회원가입 버튼

    private void Start()
    {
        loginBtn.onClick.AddListener(OnLoginButtonClick); // 로그인 버튼 클릭
    }

    // 로그인 버튼을 누르면 로그인이 바로 실행
    public void OnLoginButtonClick()
    {
        GameManager.Instance.LoginSuccess();
    }
}
