using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//

//���ʹ� ���⿡ ���� �� ��ġ���� ���� ���ؼ� Ŀ���� �ν�����â ����
[CustomEditor(typeof(EnemyBase), true)]
public class TestInspector : Editor
{
    EnemyBase enemyObject;

    void OnEnable()
    {
        enemyObject = target as EnemyBase;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (enemyObject.direction == Direction.LEFT)
        {
            enemyObject.turn();
        }
        else if (enemyObject.direction == Direction.RIGHT)
        {
            enemyObject.turn();
        }
    }
}