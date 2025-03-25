using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string userName; // 유저 이름
    public int cash; // 현금
    public int balance; // 통장 잔액

    // 생성자 - 유저 데이터 초기화
    public UserData(string name, int initialCash, int initialBalance)
    {
        userName = name;
        cash = initialCash;
        balance = initialBalance;
    }
}
