using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBase : MonoBehaviour
{
    public IEventPlay mEvent;

    //1ȸ������ �ƴ���
    public bool isPreserve = false;

    //�����ߴ��� ���ߴ���
    public bool isPlay = false;

    protected virtual void PlayEvent()
    {
        mEvent.Play();
    }
}
