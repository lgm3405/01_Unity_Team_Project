using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items4 : Items
{
    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        title = "�ʷղ�";
        effect = "�ߵ�ȿ�� : �� ������ HP �� �ҷ� ȸ���մϴ�";
        explanation[0] = "ȸ������ �ִ� ���� ������ϴ�.";
        explanationX = 1;
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
