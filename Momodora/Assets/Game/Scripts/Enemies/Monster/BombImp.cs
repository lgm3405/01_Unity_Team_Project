using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombImp : EnemyBase
{
    //������ ���¿��� �÷��̾ ���� ����ü�� ������.
    //����ü�� �����Ÿ� ���Ϸδ� �߻���������� �������� �׸���.
    //����ü�� ���������� �����ð����� ����� ������ �����Ѵ�.
    //����� �������� �����ð� �������� "��" ������� ��´�.

    [SerializeField]
    public Coroutine routine = default;

    //���� ������
    private float attackDelay = 5f;
    //���� ���� ������
    private float currDelay = 0;

    //���������� �Ǻ�
    public bool isAttack = false;

    //���� ����Ʈ
    //�ν�����â���� �����Ѵ�.
    private EnemyAttackData attackObject = null;



    // Start is called before the first frame update
    void Awake()
    {
        //base�� init �Լ� ����
        Init();

        //�ʱ� ������ ��
        currDelay = attackDelay * .8f;
        enemyAnimator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾� Ÿ�� ����
        if (target != null)
        {
            //��ƾ ������ x
            if (routine == null)
            {
                //��ƾ ����
                routine = StartCoroutine(MonsterRoutine());
            }

            //�÷��̾� Ÿ�� �����߿� �׻� ���� ������ ����
            if (currDelay < attackDelay)
            {
                currDelay += Time.deltaTime;
            }

            //��� �÷��̾ �ٶ�
            if (transform.position.x - target.transform.position.x > 0 && direction != DirectionHorizen.LEFT)
            {
                direction = DirectionHorizen.LEFT;
                turn();
            }
            else if (transform.position.x - target.transform.position.x < 0 && direction != DirectionHorizen.RIGHT)
            {
                direction = DirectionHorizen.RIGHT;
                turn();
            }

        }
    }

    //���� ��ƾ
    IEnumerator MonsterRoutine()
    {
        //�׻�
        while (true)
        {
            //���������� �ʰ� ���� �����̰� ���� �����̺��� ������� ���� ����
            if (currDelay >= attackDelay && !isAttack)
            {
                AttackStart();
                isAttack = true;
                //���� ������
                currDelay = 0;
                yield return new WaitForEndOfFrame();
                //��ƾ �ʱ�ȭ
            }

            //���� ������ ���
            if (isStun)
            {
                //���� �ȸ���
                yield return new WaitForEndOfFrame();
            }
            else
            {
                //�׿� ����ó��
                yield return new WaitForEndOfFrame();

            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + ((int)direction * Vector3.right * 4), new Vector2(8, 28));
        Debug.DrawRay(transform.position, transform.up * -1, Color.green);

    }

    //�̵� ������
    public override void Move()
    {
        //��ź ������ ����������� 
    }

    //�ִϸ��̼� ����
    public override void AttackStart()
    {
        Debug.Log("_���ݽ���");
        enemyAnimator.SetTrigger("Attack");
    }

    //�ִϸ��̼� �� ����Ʈ �ν�źƮ = ���� ����
    //������ ������ ��� ��ô Ÿ�ֶ̹� �����Ұ�
    public void AttackStartEvent()
    {
        Debug.Log("�ν��Ͻ�����");
        attackObject = Instantiate(attackData[0].gameObject, attackPosition.position, transform.rotation).GetComponent<EnemyAttackData>();

    }

    //�ִϸ��̼� �� ����Ʈ ���� = ����Ʈ �߻�
    //������ ������ ��� �߰� ����Ʈ ����� �߰��� ���� 
    public void AttackEffectEvent()
    {
        //������ ������
    }

    //�ִϸ��̼� �� �ݶ��̴� ON = ���� Ÿ�̹�
    //������ ������ ��� ��� ����(����ü�� ��ü������ ���)
    public void AttackColliderEvent()
    {
        //������ ������
    }


    //�ִϸ��̼� �� ���� ���� = ���� ����
    public void AttackEndEvent()
    {
        StartCoroutine(EndAnimation());
    }

    //���� ���� = ��ƾ ����
    //���� �� ����� �����̸� ���� �ۼ�(wait)
    public void RoutineEndEvent()
    {
        //������ �����������
    }

    //�����̰� �����ڿ� �ٽ� �̵�/���� ��
    IEnumerator EndAnimation()
    {
        yield return null;
        isAttack = false;
    }
}
