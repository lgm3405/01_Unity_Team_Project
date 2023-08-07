using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//enemy���� �ֻ�� Ŭ����

public class EnemyBase : MonoBehaviour
{
    //���ʹ� �⺻ ������Ʈ
    protected Rigidbody2D enemyRigidbody;
    protected BoxCollider2D enemyCollider;
    protected SpriteRenderer enemyRenderer;
    protected Animator enemyAnimator;
    protected AudioSource enemyAudio;

    public EnemyAudioManager enemyAudioManager;

    //���ݰ��� ������Ʈ
    public Transform attackPosition;    //���� ���� ��ġ
    public EnemyAttackData[] attackData;      //����ü, ���ݹ��� collider���� ����

    //�������� ������Ʈ
    public EnemySight sight;

    //�� prefab �ϴ��� sight ��ũ��Ʈ�� �ش� ������ ��Ʈ���Ѵ�. 
    public TestPlayer target = null;

    //���ʹ� �Ӽ�
    public int enemyHp = default;           //ü��
    public float enemySpeed = default;      //�ӵ�
    public Direction direction = Direction.LEFT;   //����
    public int enemyDamageRegist = default; //���� ������ �ѵ�

    public bool isStun = false;     //����

    //�ʱ�ȭ
    public virtual void Init()
    {
        sight = GetComponentInChildren<EnemySight>();
        enemyRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyAnimator = GetComponentInChildren<Animator>();

        enemyAudio = GetComponent<AudioSource>();
        enemyCollider = GetComponent<BoxCollider2D>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    //���� ���ݽ� �ִϸ��̼� ���
    //2�� �̻��� ���� ����� ���� ���� ���� �� �ִ�.
    public virtual void AttackStart()
    {
        //���� ���� ���� ���(�ִϸ��̼�, �Ҹ�)
        //enemyAnimator.SetTrigger("AttackStart");
        //enemyAudio.PlayOneShot(enemyAudioManager.GetAudioClip(gameObject.name, "AttackStart"));
    }

    //���� ����
    //���ϴ� �ڽ� Ŭ�������� �����Ѵ�.
    //�ִϸ��̼� ���߿� ����ȴ�.
    //�浹 ��ó����(���� ����) �ش� ���� prefab���� ó���Ѵ�.
    public virtual void Attack()
    {
        Instantiate(attackData[0].prefab, attackPosition.position, transform.rotation, transform);
    }

    //������ �ݶ��̴� �̺�Ʈ��
    public void Touch(TestPlayer player)
    {
        player.hp -= 1;
        //�÷��̾� ���� 
        //player.Hit();
    }

    //�ش� ���Ͱ� �÷��̾� ���� ������(�÷��̾��� ontrigger�̺�Ʈ)
    //��밡 ȣ���Ѵ�.
    //������ ���� ���ݽÿ� ���Ͽ� �ɸ���.
    public void Hit(bool isStun)
    {
        //�ǰݰ��� ���(�ִϸ��̼�, �Ҹ�)
        //enemyAnimator.SetTrigger("Hit");
        //enemyAudio.PlayOneShot(enemyAudioManager.GetAudioClip(gameObject.name, "Hit"));

        //�÷��̾��� �������� ü���� ����� ���·� �´�.
        if (enemyHp <= 0)
        {
            Dead();
            return;
        }

        //�÷��̾� �ν�
        target = FindObjectOfType<TestPlayer>();

        if (isStun)
        {
            this.isStun = true;
            //�����ð� ����
            //enemyAnimator.SetBool("Stun", true);
            StartCoroutine(StunDelay(1f));
        }
    }

    //���� ������
    //���� Ȯ�强�� ���ؼ� virtual�� ����(������ ȿ���ִ� ����)
    public virtual void Dead()
    {
        //�������� ���(�ִϸ��̼�, �Ҹ�)
        //enemyAnimator.SetTrigger("Dead");
        //enemyAudio.PlayOneShot(enemyAudioManager.GetAudioClip(gameObject.name, "Dead"));

        enemyRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        enemyCollider.enabled = false;
        Destroy(gameObject,.5f);
    }

    //�̵� �Լ�
    //���ϴ� �ڽ� Ŭ�������� �����Ѵ�.
    //target ���� �ÿ� ����
    public virtual void Move()
    {
        float moveDistance = enemySpeed;
        enemyRigidbody.AddForce(new Vector2(moveDistance*(int)direction, 0));
    }

    //ȸ�� �Լ�
    //�⺻ : �翷���� �̵�
    //���� Ȯ�强�� ���ؼ� virtual�� ����(�÷��̾��� ��ġ�� ���������� �ٶ󺸴� ����)
    public virtual void turn()
    {
        if (direction == Direction.LEFT)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            sight.RotateAngleZ(-90);
        }
        else if (direction == Direction.RIGHT)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            sight.RotateAngleZ(90);
        }        
    }

    //touch��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Touch(collision.collider.GetComponent<TestPlayer>());
        }
    }

    //�����ð����� ������ �ɸ���.
    public IEnumerator StunDelay(float time)
    {
        yield return new WaitForSeconds(time);
        isStun = false;
        //enemyAnimator.SetBool("Stun", false);
    }
}


public enum Direction
{
    LEFT = -1,
    RIGHT = 1
}