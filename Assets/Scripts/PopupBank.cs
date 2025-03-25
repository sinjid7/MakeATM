using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    public TextMeshProUGUI p_name; // 유저 이름
    public TextMeshProUGUI p_cash; // 유저 현금
    public TextMeshProUGUI p_balance; // 유저 잔액

    public GameObject depositScreen; // 입금화면
    public GameObject withdrawScreen; // 출금화면

    public TMP_InputField depInputField; // 입금 직접입력
    public Button depInputFieldBtn;
    public TMP_InputField witInputField; // 출금 직접입력
    public Button witInputFieldBtn;

    public GameObject depositBtn; // 입금버튼
    public GameObject withdrawBtn; // 출금버튼
    public GameObject depositBtackBtn; // 입금 뒤로가기버튼
    public GameObject withdrawBackBtn; // 출금 뒤로가기버튼


    private void Start()
    {
        // 직접입력 버튼
        depInputFieldBtn.onClick.AddListener(OnDepInputFieldBtnClicked);
        witInputFieldBtn.onClick.AddListener(OnWitInputFieldBtnClicked);
        // 입,출금 화면 숨김
        depositScreen.SetActive(false);
        withdrawScreen.SetActive(false);
    }

    // 입금버튼 클릭시 호출되는 함수
    public void showDepositScreen()
    {
        depositBtn.SetActive(false);
        withdrawBtn.SetActive(false); // 버튼도 비활성화
        depositScreen.SetActive(true);
        withdrawScreen.SetActive(false);
    }

    // 출금버튼 클릭시 호출되는 함수
    public void showWithdrawScreen()
    {
        depositBtn.SetActive(false);
        withdrawBtn.SetActive(false);// 버튼도 비활성화
        depositScreen.SetActive(false);
        withdrawScreen.SetActive(true);
    }

    public void OnDepInputFieldBtnClicked()
    {
        string inputText = depInputField.text; // 입력된 금액

        if (int.TryParse(inputText, out int depositAmount))
        {
            if (depositAmount > 0) // 금액이 0 이상인지 확인해야 함
            {
                GameManager.Instance.DepositAmount(depositAmount); // 입금 처리

                UpdatePopupUI(); // UI 업데이트
                depInputField.text = ""; // 입력 필드 초기화
            }
            else
            {
                Debug.Log("입금 금액은 0보다 커야 합니다.");
            }
        }
    }

    public void OnWitInputFieldBtnClicked() // 함수 작성 중
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
                Debug.Log("출금 금액은 0보다 커야 합니다.");
            }
        }
    }

    // 금액을 클릭하면 해당 금액만 큼 입금
    public void Deposit(int amount)
    {
        // 금액을 입금
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
        UpdatePopupUI(); // 데이터가 변경된 후 UI를 갱신
    }

    public void UpdatePopupUI()
    {
        // GameManager 인스턴스와 userData가 있는지 확인
        if (GameManager.Instance != null && GameManager.Instance.userData != null)
        {
            p_name.text = " " + GameManager.Instance.userData.userName;
            p_cash.text = string.Format("{0:N0}", GameManager.Instance.userData.cash);
            p_balance.text = string.Format("{0:N0}", GameManager.Instance.userData.balance);
        }
        else
        {
            Debug.LogError("GameManager나 UserData가 초기화되지 않았습니다.");
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
