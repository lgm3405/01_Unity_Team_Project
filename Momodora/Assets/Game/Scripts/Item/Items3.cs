using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items3 : Items
{
    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        title = "�ƽ�Ʈ�� ����";
        effect = "����ȿ�� : ���� ����߸��� �δϰ� �� �谡 �˴ϴ�.";
        explanation[0] = "�����Ǿ� ���� ����.";
        explanation[1] = "����� ũ�� �÷��ݴϴ�";
        explanationX = 2;
    }

    public override void Print()
    {
        Debug.LogFormat(title);
        Debug.LogFormat(effect);
        for (int i = 0; i < explanationX; i++)
        {
            Debug.LogFormat(explanation[i]);
        }
    }
}
