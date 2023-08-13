using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldImp : EnemyBase
{
    //�÷��̾ �ν��Ѱ�, ��ġ�� ��ġ���� ũ�� ��������� ���������� �յڷ� �����ϰ� �����δ�.
    //�̵�->���->���� ���� ��ƾ
    //Ʃ�丮�� ������ �ٶ󺸴� ��ġ�� �����Ǵ� �ɼ��� �ʿ�
    //�����̴� ���� �÷��̾ �ٶ󺸰� ���Ѵ�.    
    //���и� ����ְų� �����߿��� ���� �����ʴ´�.
    //���� ���� ���� : �ڽź��� 2ĭ ����(�Ʒ��κ��� �������), ī�޶� ��������

    [SerializeField]
    public Coroutine routine = default;

    //���� ������
    private float attackDelay = .6f;
    //���� ���� ������
    private float currDelay = 0;
    //���� ���� ������
    private float wait = 1f;
    //���� ���� ������
    private float defenceTime = 0;

    //���������� �Ǻ�
    public bool isAttack = false;
    //���������
    public bool isDefence = false;
    //�̵�������
    public bool isMove = false;

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
                //���� ������ x
                if (!isAttack)
                {
                    //��ƾ ����
                    isDefence = true;
                    routine = StartCoroutine(MonsterRoutine());
                }
            }

            //�÷��̾� Ÿ�� �����߿� �׻� ���� ������ ����
            if (currDelay < attackDelay)
            {
                currDelay += Time.deltaTime;
            }

            //�̵� �����ϰ��
            if (isMove)
            {
                //��ǥ ���� ȸ��
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
                Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position+((int)direction*Vector3.right*4), new Vector2(8, 1), 0);

                //hit�迭�� ��� ����
                foreach (Collider2D hit in hits)
                {
                    //player �����
                    if (hit.tag == "Player")
                    {
                        Debug.Log("�Ϲݰ���");
                        enemyRigidbody.velocity = new Vector2(0, enemyRigidbody.velocity.y);
                        isAttack = true;
                        isMove = false;
                        isDefence = false;
                        AttackStart();
                        break;
                    }
                }

                //���� �������� ���
                if (isAttack)
                {
                    //���� ������
                    currDelay = 0;
                    //��ƾ ����
                    break;
                }              


            }

            //���� ���� �ƴҰ��
            if (!isStun)
            {
                //�̵�
                Move();
                yield return new WaitForSeconds(Time.deltaTime);

            }
            //���� ������ ���
            else
            {
                //���� �ȸ���
                yield return new WaitForEndOfFrame();
            }
            if (isMove && Mathf.Approximately(enemyRigidbody.velocity.x, 0))
            {
                isMove = false;
                enemyRigidbody.velocity = Vector2.zero;
                yield return new WaitForSeconds(defenceTime);
            }
        }

        //��ƾ ����
        routine = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + ((int)direction * Vector3.right * 4), new Vector2(8, 1));

    }

    //�̵� ������
    public override void Move()
    {
        //���� �÷��������� ������
        //���� �÷����� ����ŭ ���ؼ� �����δ�.
        if (isMovingPlatform)
        {
            enemyRigidbody.velocity = new Vector2(enemySpeed*(int)direction + platformBody.velocity.x, enemyRigidbody.velocity.y);
        }
        //�⺻ �÷��������� ������
        else
        {
            enemyRigidbody.velocity = new Vector2(enemySpeed * (int)direction, enemyRigidbody.velocity.y);
        }
        isMove = true;
    }

    //�ִϸ��̼� ����
    public override void AttackStart()
    {
        Debug.Log("���ݽ���");
        enemyAnimator.SetTrigger("Attack");
    }

    //�ִϸ��̼� �� ����Ʈ �ν�źƮ = ���� ����
    //������ ������ ��� ��ô Ÿ�ֶ̹� �����Ұ�
    public void AttackStartEvent()
    {
        Debug.Log("�ν��Ͻ�����");
        attackObject = Instantiate(attackData[0].gameObject, attackPosition.position, transform.rotation, transform).GetComponent<EnemyAttackData>();

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
    //������ ������ ��� ��� ����(����ü�� ��ü������ ���)
    public void AttackEndEvent()
    {
        //������ ������
    }

    //���� ���� = ��ƾ ����
    //���� �� ����� �����̸� ���� �ۼ�(wait)
    public void RoutineEndEvent()
    {
        //�������̰� ��ƾ�� ���ٸ�
        if (isAttack && routine == null)
        {
            //������ �̺�Ʈ ����
            routine = StartCoroutine(EndAnimation());
        }
    }

    //�����̰� �����ڿ� �ٽ� �̵�/���� ��
    //idle ���¿��� ����
    IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(wait);
        isAttack = false;
        routine = null;
    }
}
