using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager
{
    //������ ����� collector
    private Dictionary<string, Dictionary<string, AudioClip>> audioClips;

    //���� �غ� ����� �ҽ�
    //�̸����� : �����̸�_������̸�
    public List<AudioClip> audioClipList;

     

    //���� ���۽ÿ� ȣ��
    public void Init()
    {
        audioClips = new Dictionary<string, Dictionary<string, AudioClip>>();

        foreach (AudioClip clip in audioClipList)
        {
            string[] splitString = clip.name.Split("_");
            string enemyName = splitString[0];
            string clipName = splitString[1];
            if (audioClips.ContainsKey(enemyName))
            {
                audioClips[enemyName].Add(clipName, clip);
            }
            else
            {
                Dictionary<string, AudioClip> innerDictionary = new Dictionary<string, AudioClip>();
                audioClips.Add(enemyName, innerDictionary);
                audioClips[enemyName].Add(clipName, clip);
            }
        }
    }


    //ȣ���ϴ� audioClip�� �ش�.
    public AudioClip GetAudioClip(string enemyName, string actionName) 
    {
        AudioClip clip = audioClips[enemyName][actionName];
        if (clip == null)
        {
            Debug.Log("����");
        }
        return clip;
    }
}
