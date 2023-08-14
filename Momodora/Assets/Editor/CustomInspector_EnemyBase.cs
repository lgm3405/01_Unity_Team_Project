using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//

//���ʹ� ���⿡ ���� �� ��ġ���� ���� ���ؼ� Ŀ���� �ν�����â ����
[CustomEditor(typeof(EnemyBase), true)]
public class CustomInspector_EnemyBase : Editor
{
    EnemyBase enemyObject;

    void OnEnable()
    {
        enemyObject = target as EnemyBase;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

#if UNITY_EDITOR
        if (enemyObject.direction == DirectionHorizen.LEFT)
        {
            enemyObject.turn();
        }
        else if (enemyObject.direction == DirectionHorizen.RIGHT)
        {
            enemyObject.turn();
        }
#endif
    }
}
