using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items2 : Items
{
    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        title = "���� ����";
        effect = "����ȿ�� : ���� �ð��� �þ�� ������ ����մϴ�.";
        explanation[0] = "Ǫ�� �� ���� ������ �߰��� ����";
        explanation[1] = "������ �����߽��ϴ�. ���� ���� ���� �߼��� ��������";
        explanation[2] = "�� ���� �����ϴ�.";
        explanationX = 3;
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
