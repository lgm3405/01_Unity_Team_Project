using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlBase : MonoBehaviour
{
    public List<IEventPlay> mEvents;

    //1ȸ������ �ƴ���
    public bool isPreserve = false;

    //�����ߴ��� ���ߴ���
    public bool isPlay = false;

    //��� �⺻�� 0
    public int mode = 0;

    private void Awake()
    {
        mEvents = new List<IEventPlay>();

        mEvents.AddRange(transform.parent.GetComponentsInChildren<IEventPlay>().ToList());
    }

    protected virtual void PlayEvent()
    {
        foreach (var mEvent in mEvents) 
        {
            mEvent.Play(this);
        }
    }
}
