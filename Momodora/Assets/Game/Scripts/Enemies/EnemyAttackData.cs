using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackData : MonoBehaviour
{
    //Ÿ��
    public EnemyAttackType type;
    
    //�Ѿ�, ����Ʈ ��
    public GameObject prefab;

    //���ݷ�
    public int damage;
}

public enum EnemyAttackType
{
    RANGE,
    MELEE,
    NOTHING
}
