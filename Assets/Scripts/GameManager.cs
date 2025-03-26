using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // �̱��� �ν��Ͻ�
    public UserData userData;
    public PopupError popupError; // �ܾ� ���� �˾�
    public PopupBank popupBank;
    public PopupLogin popupLogin; // �α��� â

    // ���̺� Ű
    public const string UserNameKey = "userName";
    public const string CashKey = "cash";
    public const string BalanceKey = "balance";

    private void Awake()
    {
        // �� ���ǹ��� ���� ���� �� �ν��Ͻ��� �ϳ��� �����ϵ��� ��
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �ÿ��� �� ������Ʈ�� �������� �ʵ���
        }
        else
        {
            Destroy(gameObject);  // �̹� �ν��Ͻ��� ������ �ٸ� GameManager ������Ʈ�� �ı�
        }
    }

    void Start()
    {
        ShowLoginPopup();
    }

    // �α��� �˾� �����ֱ�
    public void ShowLoginPopup()
    {
        popupLogin.gameObject.SetActive(true);
        popupBank.gameObject.SetActive(false);
    }

    // // �α��� �˾� ����
    public void HideLoginPopup()
    {
        popupLogin.gameObject.SetActive(false);
        popupBank.gameObject.SetActive(true);

        LoadUserData(); // ���� ������ �ε�
        popupBank.UpdatePopupUI(); // ���� ui������Ʈ
    }

    // �α����� �����ϸ� �˾���ũ�� ������
    public void LoginSuccess()
    {
        HideLoginPopup();
    }

    // ���� ������ �ʱ�ȭ �Լ�
    public void InitializeUserData(string name, int initialCash, int initialBalance)
    {
        userData = new UserData(name, initialCash, initialBalance);
        SaveUserData(); // �ʱ�ȭ �� ������ ����
    }

    // ���������͸� �����ϴ� �Լ�
    public void SaveUserData()
    {
        PlayerPrefs.SetString(UserNameKey, userData.userName);
        PlayerPrefs.SetInt(CashKey, userData.cash);
        PlayerPrefs.SetInt(BalanceKey, userData.balance);
        PlayerPrefs.Save(); // PlayerPrefs�� ����
    }

    //����� ���� �����͸� �ε��ϴ� �Լ�
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
            userData = new UserData("��������ī", 200000, 50000); // ����� �����Ͱ� ������ �⺻������ �ʱ�ȭ
        }

        UpdateUI();
    }

    // �Ա� �޼��� - cash���� balance�� �ݾ��� �̵�
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

    // UI ������Ʈ �Լ�
    public void UpdateUI()
    {
        if (userData != null)
        {
            UIManager.Instance.popupBank.UpdatePopupUI();
        }
    }

}
