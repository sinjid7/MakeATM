using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string userName; // ���� �̸�
    public int cash; // ����
    public int balance; // ���� �ܾ�

    // ������ - ���� ������ �ʱ�ȭ
    public UserData(string name, int initialCash, int initialBalance)
    {
        userName = name;
        cash = initialCash;
        balance = initialBalance;
    }
}
