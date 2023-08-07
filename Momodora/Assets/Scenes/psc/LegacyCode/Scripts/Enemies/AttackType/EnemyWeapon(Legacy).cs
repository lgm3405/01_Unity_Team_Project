using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon_Legacy : MonoBehaviour
{
    //���� Ÿ��
    public WeaponType_Legacy weaponType;    
    //���ݷ�
    public int weaponDamage;
    //����
    public float weaponSpeed;

    public virtual void useWeapon(EnemyDirection_Legacy direction) { }
}

public enum WeaponType_Legacy
{
    RANGE,  //���Ÿ� ����
    MELEE,  //�ٰŸ� ����(���� �ִϸ��̼� ����)
    NOTHING //�ٰŸ� ����(���� �ִϸ��̼� ����)
} 