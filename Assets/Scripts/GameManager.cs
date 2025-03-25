using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // 싱글톤 인스턴스
    public UserData userData;

    public PopupError popupError; // 잔액 부족 팝업 참조

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
        GameManager.Instance.InitializeUserData("신파프리카", 200000, 50000);
    }

    // 유저 데이터 초기화 함수
    public void InitializeUserData(string name, int initialCash, int initialBalance)
    {
        userData = new UserData(name, initialCash, initialBalance);
    }

    // 입금 메서드 - cash에서 balance로 금액을 이동
    public void DepositAmount(int amount)
    {
        if(userData.cash >= amount)
        {
            userData.cash -= amount;
            userData.balance += amount;
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
        }
        else
        {
            popupError.ShowPopupError();
        }
    }

}
