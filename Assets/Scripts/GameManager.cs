using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // 싱글톤 인스턴스
    public UserData userData;
    public PopupError popupError; // 잔액 부족 팝업
    public PopupBank popupBank;
    public PopupLogin popupLogin; // 로그인 창

    // 세이브 키
    public const string UserNameKey = "userName";
    public const string CashKey = "cash";
    public const string BalanceKey = "balance";

    private void Awake()
    {
        // 이 조건문을 통해 게임 내 인스턴스가 하나만 존재하도록 함
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시에도 이 오브젝트가 삭제되지 않도록
        }
        else
        {
            Destroy(gameObject);  // 이미 인스턴스가 있으면 다른 GameManager 오브젝트를 파괴
        }
    }

    void Start()
    {
        ShowLoginPopup();
    }

    // 로그인 팝업 보여주기
    public void ShowLoginPopup()
    {
        popupLogin.gameObject.SetActive(true);
        popupBank.gameObject.SetActive(false);
    }

    // // 로그인 팝업 숨김
    public void HideLoginPopup()
    {
        popupLogin.gameObject.SetActive(false);
        popupBank.gameObject.SetActive(true);

        LoadUserData(); // 유저 데이터 로드
        popupBank.UpdatePopupUI(); // 은행 ui업데이트
    }

    // 로그인을 성공하면 팝업뱅크를 보여줌
    public void LoginSuccess()
    {
        HideLoginPopup();
    }

    // 유저 데이터 초기화 함수
    public void InitializeUserData(string name, int initialCash, int initialBalance)
    {
        userData = new UserData(name, initialCash, initialBalance);
        SaveUserData(); // 초기화 후 데이터 저장
    }

    // 유저데이터를 저장하는 함수
    public void SaveUserData()
    {
        PlayerPrefs.SetString(UserNameKey, userData.userName);
        PlayerPrefs.SetInt(CashKey, userData.cash);
        PlayerPrefs.SetInt(BalanceKey, userData.balance);
        PlayerPrefs.Save(); // PlayerPrefs에 저장
    }

    //저장된 유저 데이터를 로드하는 함수
    public void LoadUserData()
    {
        if (PlayerPrefs.HasKey(UserNameKey))
        {
            string userName = PlayerPrefs.GetString(UserNameKey);
            int cash = PlayerPrefs.GetInt(CashKey);
            int balance = PlayerPrefs.GetInt(BalanceKey);
            
            userData = new UserData(userName, cash, balance);
        }
        else
        {
            userData = new UserData("신파프리카", 200000, 50000); // 저장된 데이터가 없으면 기본값으로 초기화
        }

        UpdateUI();
    }

    // 입금 메서드 - cash에서 balance로 금액을 이동
    public void DepositAmount(int amount)
    {
        if(userData.cash >= amount)
        {
            userData.cash -= amount;
            userData.balance += amount;
            SaveUserData();
        }
        else
        {
            popupError.ShowPopupError();
        }
    }

    public void WithdrawAmount(int amount)
    {
        if (userData.balance >= amount)
        {
            userData.balance -= amount;
            userData.cash += amount;
            SaveUserData();
        }
        else
        {
            popupError.ShowPopupError();
        }
    }

    // UI 업데이트 함수
    public void UpdateUI()
    {
        if (userData != null)
        {
            UIManager.Instance.popupBank.UpdatePopupUI();
        }
    }

}
