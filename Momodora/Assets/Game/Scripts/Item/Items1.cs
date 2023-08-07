using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items1 : Items
{
    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        name = "������1";
        title = "��� ����";
        effect = "����ȿ�� : õõ�� HP �� ȸ���մϴ�.";
        explanation[0] = "ũ�ι̴Ͼ� ���濡�� �� ����.";
        explanation[1] = "�罿�� Ÿ�� ����� �� �������";
        explanation[2] = "���ƴٴ� ���ɵ��� �ް� �ٳ���ϴ�.";
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
