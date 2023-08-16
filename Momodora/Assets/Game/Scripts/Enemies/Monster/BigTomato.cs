using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTomato : EnemyBase
{
    //��� �÷��̾�� �����ϴٰ�
    //���� ���� + ���� Ÿ�̹��� ��� �����Ѵ�.
    [SerializeField]
    public Coroutine routine = default;

    private float moveDelay = 1f;
    private float attackDelay = 1.5f;
    //������ ���� �ð� ���
    private float wait = 1f;
    private float currDelay = 0;

    public bool isAttack = false;

    private EnemyAttackData attackObject = null;



    // Start is called before the first frame update
    void Awake()
    {
        Init();
        currDelay = attackDelay - 1f;
        enemySpeed = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (routine == null)
            {
                if (!isAttack)
                {
                    if (!isStun)
                    {
                        routine = StartCoroutine(MonsterRoutine());
                    }
                }
            }

            if (currDelay < attackDelay)
            {
                currDelay += Time.deltaTime;
            }

            if (isStun && currDelay > attackDelay)
            {
                currDelay = attackDelay * .6f;
            }
        }
    }

    IEnumerator MonsterRoutine()
    {
        while (true)
        {
            if (isStun)
            {
                yield return new WaitForEndOfFrame();
            }
            else
            {

                if (transform.position.x - target.transform.position.x > 0)
                {
                    direction = DirectionHorizen.LEFT;
                    turn();
                }
                else
                {
                    direction = DirectionHorizen.RIGHT;
                    turn();
                }

                if (currDelay >= attackDelay && !isAttack)
                {
                    Collider2D[] hits = Physics2D.OverlapCircleAll(attackPosition.position, 2);

                    //hit�迭�� ��� ����
                    foreach (Collider2D hit in hits)
                    {
                        Debug.Log("!");
                        if (hit.tag == "Player")
                        {
                            Debug.Log("!");
                            enemyRigidbody.velocity = new Vector2(0, enemyRigidbody.velocity.y);
                            AttackStart();
                            isAttack = true;
                            currDelay = 0;
                            break;
                        }
                    }
                    if (isAttack)
                    {
                        break;
                    }
                    yield return new WaitForEndOfFrame();
                }
                else 
                {

                    Move();
                    enemyAnimator.SetTrigger("Move");
                    yield return new WaitForSeconds(moveDelay);
                    enemyRigidbody.velocity = new Vector2(0, enemyRigidbody.velocity.y);
                }
                    

            }
            
        }

        routine = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, 2);

    }

    public override void Move()
    {
        if (isMovingPlatform)
        {
            enemyRigidbody.velocity = new Vector2((enemySpeed * (int)direction)+platformBody.velocity.x, enemyRigidbody.velocity.y);
        }
        else
        {
            enemyRigidbody.velocity = new Vector2(enemySpeed * (int)direction, enemyRigidbody.velocity.y);

        }
    }

    //�ִϸ��̼� ����
    public override void AttackStart()
    {
        enemyAnimator.SetTrigger("Attack");
    }

    //�ִϸ��̼� �� ���� ����
    public void AttackStartEvent()
    {
        attackObject = Instantiate(attackData[0].gameObject, attackPosition.position, transform.rotation, transform).GetComponent<EnemyAttackData>();
        
    }

    //�ִϸ��̼� �� �ݶ��̴� ����
    public void AttackColliderEvent()
    {
        
        attackObject.UseCollider();
    }

    //�ִϸ��̼� �� ����Ʈ ����
    public void AttackEffectEvent()
    {
    
        attackObject.UseEffect();
    }

    //�ִϸ��̼� �� ���� ����
    public void AttackEndEvent()
    {
       
        Destroy(attackObject.gameObject);
    }

    public void RoutineEndEvent()
    {
        if (isAttack && routine == null)
        {
            routine = StartCoroutine(EndAnimation());
        
        }
    }

    IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(wait);
        isAttack = false;
        routine = null;
    }
}
