using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon_Legacy : MonoBehaviour
{
    //���ʹ� �⺻ ������Ʈ
    public Rigidbody2D enemyRigidbody;
    public SpriteRenderer enemyRenderer;
    public BoxCollider2D enemyCollider;
    public Animator enemyAnimator;

    //���ݰ��� ������Ʈ
    public EnemyWeapon_Legacy attackObject;    //���� ������Ʈ(�ٰŸ�/���Ÿ� ������)
    public Transform attackPosition;    //���� ���� ��ġ

    //Ž�� ������Ʈ
    public Transform targetObject;

    //���ʹ� �Ӽ�
    public EnemyDirection_Legacy enemyDirection = default; //����

    public int enemyHp = default;
    public float enemySpeed = default;

    public bool isStun = false;     //����
    public bool isWait = false;     //Ž������
    public bool isDelay = false;    //���� ������

    public virtual void Init()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider2D>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual void Remove()
    {
        Destroy(enemyAnimator);
        Destroy(enemyCollider);
        Destroy(enemyRenderer);
        Destroy(enemyRigidbody);
    }

    public virtual void Attack()
    {
        EnemyWeapon_Legacy weaponObject = Instantiate(attackObject, attackPosition.position, Quaternion.identity);
        weaponObject.useWeapon(enemyDirection); 
    }

    public virtual void Touch() { }
    public virtual void Hit() { }
    public virtual void Dead() { }
    public virtual void Move() 
    {
    }
    public virtual void Defance() { }

    private void Update()
    {
        
    }
}

public enum EnemyDirection_Legacy {
    LEFT = -1, RIGHT = 1
}