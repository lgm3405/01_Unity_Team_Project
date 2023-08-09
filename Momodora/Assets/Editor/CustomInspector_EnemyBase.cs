using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

        if (enemyObject.direction == DirectionHorizen.LEFT)
        {
            enemyObject.turn();
        }
        else if (enemyObject.direction == DirectionHorizen.RIGHT)
        {
            enemyObject.turn();
        }
    }
}
