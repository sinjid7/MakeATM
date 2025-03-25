using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // �̱��� �ν��Ͻ�
    public UserData userData;

    public PopupError popupError; // �ܾ� ���� �˾� ����

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
        GameManager.Instance.InitializeUserData("��������ī", 200000, 50000);
    }

    // ���� ������ �ʱ�ȭ �Լ�
    public void InitializeUserData(string name, int initialCash, int initialBalance)
    {
        userData = new UserData(name, initialCash, initialBalance);
    }

    // �Ա� �޼��� - cash���� balance�� �ݾ��� �̵�
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
