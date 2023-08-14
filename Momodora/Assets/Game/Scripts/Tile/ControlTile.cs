using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTile : MonoBehaviour
{
    List<EventTile> eventTiles;

    //�̺�Ʈ �Ŵ����� �߰��� bool ����
    //�̺�Ʈ ����
    bool endEvent = false;
    
    //�̺�Ʈ ����
    bool playEvent = false;

    //ȭ��� Ÿ��
    public bool enableArrow = true;
    //����
    public bool enablefloor = true;
    //����
    public bool enableAccess = false;
    //������
    public string accessItemName = null;

    private void Update()
    {
        if (playEvent && !endEvent)
        {
            foreach(var eventTile in eventTiles)
            {
                eventTile.PlayEvent();
                endEvent = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enableArrow && collision.collider.tag == "Arrow")
        {
            if (!playEvent)
            {
                playEvent = true;
            }
        }

        if(enablefloor && collision.collider.tag == "Player")
        {
            foreach (var contact in collision.contacts)
            {
                if(contact.point.normalized.y >= .8f)
                {
                    playEvent = true;
                }
            }
        }

        if (enableAccess && collision.collider.tag == "Player")
        {
            if (accessItemName != null)
            {
                playEvent = true;
                /*�߰��� item�̸� �� �ʿ�*/
            }
        }
    }
}
