using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//enemy���� �ֻ�� Ŭ����

public class EnemyBase : MonoBehaviour
{
    //���ʹ� �⺻ ������Ʈ
    private Rigidbody2D enemyRigidbody;
    private BoxCollider2D enemyCollider;
    private SpriteRenderer enemyRenderer;
    private Animator enemyAnimator;

    //���ݰ��� ������Ʈ
    public Transform attackPosition;    //���� ���� ��ġ
    public Transform attackPrefab;      //����ü, ���ݹ��� collider���� ����

    //Ž�� ������Ʈ
    //PlayerFinder - �ֻ�� Ŭ����
    //PlayerFinder
    //public PlayerFinder finderAI;

    //���ʹ� �Ӽ�
    public int enemyHp = default;
    public float enemySpeed = default;

    public bool isStun = false;     //����
    public bool isWait = false;     //Ž������
    public bool isDelay = false;    //���� ������

    //�ʱ�ȭ
    public virtual void Init()
    {
        enemyRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyAnimator = GetComponentInChildren<Animator>();

        enemyCollider = GetComponent<BoxCollider2D>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    //���� ���ݽ� �ִϸ��̼� ���
    public void AttackStart()
    {
    }

    //���� ����
    //���ϴ� �ڽ� Ŭ�������� �����Ѵ�.
    //�ִϸ��̼� ���߿� ����ȴ�.
    //���Ÿ� -> ������Ʈ ����
    //�ٰŸ�1 -> �� �ִϸ��̼� ���� �浹���� value ����
    //�ٰŸ�2 -> ���� ���� ���� 
    public virtual void Attack()
    {
    }

    //������ �ݶ��̴� �̺�Ʈ��
    public void Touch(TestPlayer player)
    {
    }

    //�ش� ���Ͱ� �÷��̾� ���� ������(�÷��̾��� ontrigger�̺�Ʈ)
    //��밡 ȣ���Ѵ�.
    public void Hit() 
    { 
    }

    //���� ������
    public void Dead() 
    { 
    }

    //�̵� �Լ�
    //���ϴ� �ڽ� Ŭ�������� �����Ѵ�.
    //ai���� ���� �ÿ� ����
    public virtual void Move(TestPlayer player)
    {
    }
}
