using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    public Dictionary<string, MapEvent> eventCheck = new Dictionary<string, MapEvent>();
    public int eventCheck2 = default;

    public int test = default;

    /* �̺�Ʈ üũ ���
    0 : Stage1Map3 - ��������
    1 : Stage1Map4 - �ʷղ�
    2 : Stage1Map7 - �ھƿ����� ���ٸ�
    3 : Stage1Map11 - ��������
    4 : Stage1Map12 - �� ����������
    5 : Stage1Map15 - ������ ȹ��
    6 : Stage1Map16 - �� ��
    7 : Stage1Map23 - ��������
    8 : ���� �� - ���� ����
    9 : �ذ� NPC ��ȭ ����

    */

    void Awake()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    eventCheck[i] = 0;
        //    eventCheck2 = 0;
        //    eventCheck[1] = 1;

        //    test = 5;
        //}
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    test=MapCheck("Stage1Map4");
        //    Debug.Log(test);
        //}
    }

    //public int MapCheck(string mapName)
    //{
        //int check=2;

        //if (mapName == "Stage1Map3")
        //{
        //    if (eventCheck[0] == 0)
        //    {
        //        // �������� �ʱ����
        //    }
        //    else
        //    {
        //        // �������� ��������
        //    }

        //    check = eventCheck[0];
        //}
        //else if (mapName == "Stage1Map4")
        //{
        //    if (eventCheck[1] == 0)
        //    {
        //        // �ʷղ� �ʱ����
        //    }
        //    else
        //    {
        //        // �ʷղ� ȹ�� ����
        //    }

        //    check = eventCheck[1];
        //}
        //else if (mapName == "Stage1Map7")
        //{
        //    if (eventCheck[2] == 0)
        //    {
        //        // �ھƿ����� ���ٸ� �ʱ� ����
        //    }
        //    else
        //    {
        //        // �ھƿ����� ���ٸ� ���۵� ����
        //    }

        //    check = eventCheck[2];
        //}
        //else if (mapName == "Stage1Map11")
        //{
        //    if (eventCheck[3] == 0)
        //    {
        //        // �������� �ʱ����
        //    }
        //    else
        //    {
        //        // �������� ��������
        //    }

        //    check = eventCheck[3];
        //}
        //else if (mapName == "Stage1Map12")
        //{
        //    if (eventCheck[4] == 0)
        //    {
        //        // ���������� �ʱ����
        //    }
        //    else
        //    {
        //        // ���������� ������ ����
        //    }

        //    check = eventCheck[4];
        //}
        //else if (mapName == "Stage1Map15")
        //{
        //    if (eventCheck[5] == 0)
        //    {
        //        // ������ ��ȹ�� ����
        //    }
        //    else
        //    {
        //        // ������ ȹ�� ����
        //    }

        //    check = eventCheck[5];
        //}
        //else if (mapName == "Stage1Map16")
        //{
        //    if (eventCheck[6] == 0)
        //    {
        //        // �� �� �ʱ� ����
        //    }
        //    else
        //    {
        //        // �� �� ���� ����
        //    }

        //    check = eventCheck[6];
        //}
        //else if (mapName == "Stage1Map23")
        //{
        //    if (eventCheck[7] == 0)
        //    {
        //        // �������� �ʱ����
        //    }
        //    else
        //    {
        //        // �������� ��������
        //    }

        //    check = eventCheck[7];
        //}
        //else if (mapName == " ")   // ���� ��
        //{
        //    if (eventCheck[8] == 0)
        //    {
        //        // ���� �ʱ����
        //    }
        //    else
        //    {
        //        // ���� ��������
        //    }

        //    check = eventCheck[8];
        //}
        //else if (mapName == " ")   // �ذ� NPC ��
        //{
        //    if (eventCheck[9] == 0)
        //    {
        //        // �ذ� NPC �ʱ� ����
        //    }
        //    else
        //    {
        //        // �ذ� NPC ��ȭ ���� ����
        //    }

        //    check = eventCheck[9];
        //}
        //else
        //{
        //    check = 2;
        //}

        //return check;
    //}
}
