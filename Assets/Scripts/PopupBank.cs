using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    public TextMeshProUGUI p_name; // ���� �̸�
    public TextMeshProUGUI p_cash; // ���� ����
    public TextMeshProUGUI p_balance; // ���� �ܾ�

    public GameObject depositScreen; // �Ա�ȭ��
    public GameObject withdrawScreen; // ���ȭ��

    public TMP_InputField depInputField; // �Ա� �����Է�
    public Button depInputFieldBtn;
    public TMP_InputField witInputField; // ��� �����Է�
    public Button witInputFieldBtn;

    public GameObject depositBtn; // �Աݹ�ư
    public GameObject withdrawBtn; // ��ݹ�ư
    public GameObject depositBtackBtn; // �Ա� �ڷΰ����ư
    public GameObject withdrawBackBtn; // ��� �ڷΰ����ư


    private void Start()
    {
        // �����Է� ��ư
        depInputFieldBtn.onClick.AddListener(OnDepInputFieldBtnClicked);
        witInputFieldBtn.onClick.AddListener(OnWitInputFieldBtnClicked);
        // ��,��� ȭ�� ����
        depositScreen.SetActive(false);
        withdrawScreen.SetActive(false);
    }

    // �Աݹ�ư Ŭ���� ȣ��Ǵ� �Լ�
    public void showDepositScreen()
    {
        depositBtn.SetActive(false);
        withdrawBtn.SetActive(false); // ��ư�� ��Ȱ��ȭ
        depositScreen.SetActive(true);
        withdrawScreen.SetActive(false);
    }

    // ��ݹ�ư Ŭ���� ȣ��Ǵ� �Լ�
    public void showWithdrawScreen()
    {
        depositBtn.SetActive(false);
        withdrawBtn.SetActive(false);// ��ư�� ��Ȱ��ȭ
        depositScreen.SetActive(false);
        withdrawScreen.SetActive(true);
    }

    public void OnDepInputFieldBtnClicked()
    {
        string inputText = depInputField.text; // �Էµ� �ݾ�

        if (int.TryParse(inputText, out int depositAmount))
        {
            if (depositAmount > 0) // �ݾ��� 0 �̻����� Ȯ���ؾ� ��
            {
                GameManager.Instance.DepositAmount(depositAmount); // �Ա� ó��

                UpdatePopupUI(); // UI ������Ʈ
                depInputField.text = ""; // �Է� �ʵ� �ʱ�ȭ
            }
            else
            {
                Debug.Log("�Ա� �ݾ��� 0���� Ŀ�� �մϴ�.");
            }
        }
    }

    public void OnWitInputFieldBtnClicked() // �Լ� �ۼ� ��
    {
        string inputText = witInputField.text;
        if (int.TryParse(inputText, out int withdrawAmount))
        {
            if (withdrawAmount > 0)
            {
                GameManager.Instance.WithdrawAmount(withdrawAmount);

                UpdatePopupUI();
                witInputField.text = "";
            }
            else
            {
                Debug.Log("��� �ݾ��� 0���� Ŀ�� �մϴ�.");
            }
        }
    }

    // �ݾ��� Ŭ���ϸ� �ش� �ݾ׸� ŭ �Ա�
    public void Deposit(int amount)
    {
        // �ݾ��� �Ա�
        if (GameManager.Instance != null && GameManager.Instance.userData != null)
        {
            GameManager.Instance.DepositAmount(amount);
            UpdatePopupUI();
        }

    }

    public void Withdraw(int amount)
    {
        if (GameManager.Instance != null && GameManager.Instance.userData != null)
        {
            GameManager.Instance.WithdrawAmount(amount);
            UpdatePopupUI();
        }
    }

    public void Refresh()
    {
        UpdatePopupUI(); // �����Ͱ� ����� �� UI�� ����
    }

    public void UpdatePopupUI()
    {
        // GameManager �ν��Ͻ��� userData�� �ִ��� Ȯ��
        if (GameManager.Instance != null && GameManager.Instance.userData != null)
        {
            p_name.text = " " + GameManager.Instance.userData.userName;
            p_cash.text = string.Format("{0:N0}", GameManager.Instance.userData.cash);
            p_balance.text = string.Format("{0:N0}", GameManager.Instance.userData.balance);
        }
        else
        {
            Debug.LogError("GameManager�� UserData�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        }
    }

    public void ClosePopup()
    {
        depositBtn.SetActive(true);
        withdrawBtn.SetActive(true);

        depositScreen.SetActive(false);
        withdrawScreen.SetActive(false);
    }
}
