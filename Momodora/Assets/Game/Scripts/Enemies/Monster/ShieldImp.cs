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
    private float attackDelay = 5f;
    //���� ���� ������
    private float currDelay = 0;
    //�ൿ ������
    private float wait = 1f;
    //��� ������
    private float defenceTime = .5F;
    //�̵� ������
    private float moveTime = .5F;

    //���������� �Ǻ�
    public bool isAttack = false;
    //���������
    public bool isDefence = false;
    //�̵�������
    public bool isMove = false;

    Vector2 firstPoint;

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
        firstPoint = transform.position;
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
                    if (!isStun)
                    {
                        //��ƾ ����
                        isDefence = true;
                        routine = StartCoroutine(MonsterRoutine());
                    }
                }
            }

            //�÷��̾� Ÿ�� �����߿� �׻� ���� ������ ����
            if (currDelay < attackDelay)
            {
                currDelay += Time.deltaTime;
            }

            if (!isStun)
            {
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
                    Move();
                }
            }


            if (isStun && currDelay > attackDelay)
            {
                currDelay = attackDelay * .6f;
            }
        }
    }

    //���� ��ƾ
    IEnumerator MonsterRoutine()
    {
        //�׻�
        while (true)
        {

            //���� ������ ���
            if (isStun)
            {
                //���� �ȸ���
                yield return new WaitForEndOfFrame();
            }
            else
            {
                //���������� �ʰ� ���� �����̰� ���� �����̺��� ������� ���� ����
                if (currDelay >= attackDelay && !isAttack)
                {
                    Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position + ((int)direction * Vector3.right * 4), new Vector2(8, 28), 0);

                    //hit�迭�� ��� ����
                    foreach (Collider2D hit in hits)
                    {
                        //player �����
                        if (hit.tag == "Player" && hit.transform.position.y - transform.position.y <= 2)
                        {
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
                        yield return new WaitForEndOfFrame();
                        //��ƾ �ʱ�ȭ
                        continue;
                    }


                }

                //���� ���� �ƴҰ��
                if (!isAttack)
                {
                    //�̵�
                    isMove = true;
                    yield return new WaitForSeconds(moveTime);
                    enemyRigidbody.velocity = new Vector2(0, enemyRigidbody.velocity.y);
                    isMove = false;

                    //���Ÿ��
                    yield return new WaitForSeconds(defenceTime);

                }

                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + ((int)direction * Vector3.right * 4), new Vector2(8, 28));

    }

    //�̵� ������
    public override void Move()
    {
        if(Mathf.Abs(transform.position.x - firstPoint.x) >= 2)
        {
            enemyRigidbody.velocity = new Vector2(firstPoint.x - transform.position.x, enemyRigidbody.velocity.y);
        }
        else
        {
            if (isMovingPlatform)
            {
                enemyRigidbody.velocity = new Vector2(enemySpeed * (int)direction + platformBody.velocity.x, enemyRigidbody.velocity.y);
            }
            //�⺻ �÷��������� ������
            else
            {
                enemyRigidbody.velocity = new Vector2(enemySpeed * (int)direction, enemyRigidbody.velocity.y);
            }

        }
    }

    //�ִϸ��̼� ����
    public override void AttackStart()
    {
        enemyAnimator.SetTrigger("Attack");
    }

    //�ִϸ��̼� �� ����Ʈ �ν�źƮ = ���� ����
    //������ ������ ��� ��ô Ÿ�ֶ̹� �����Ұ�
    public void AttackStartEvent()
    {
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
        yield return new WaitForSeconds(wait);
        isAttack = false;
        isDefence = true;
    }
}
