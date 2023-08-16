using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//enemy���� �ֻ�� Ŭ����


//trigger -> colliderü��-���ݷ�
//collider hit()
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
    public PlayerMove target = null;

    //���ʹ� �Ӽ�
    public int enemyHp = default;           //ü��
    public float enemySpeed = default;      //�ӵ�
    public DirectionHorizen direction = DirectionHorizen.LEFT;   //����

    private Coroutine stunCoroutine = null;
    public int enemyStunRegistValue = default; //���� ������ �ѵ�
    public int enemyStunRegistMaxCount = default; //���� ������ Ƚ��
    public int enemyStunRegistCurrCount = default; //���� ������ Ƚ��

    public bool isStun = false;     //����


    public Rigidbody2D platformBody;
    public bool isMovingPlatform = false;

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

    //������ �ݶ��̴� �̺�Ʈ��
    public void Touch(PlayerMove player)
    {
        player.playerHp -= 1;
        //�÷��̾� ���� 
        //player.Hit();
    }

    //�ش� ���Ͱ� �÷��̾� ���� ������(�÷��̾��� ontrigger�̺�Ʈ)
    //��밡 ȣ���Ѵ�.
    //������ ���� ���ݽÿ� ���Ͽ� �ɸ���.
    public void Hit(int damage)
    {
        enemyRigidbody.velocity = Vector3.zero;
        enemyHp -= damage;
        

        if (enemyStunRegistValue <= damage)
        {
            Debug.Log("����ī��Ʈ+1");
            enemyStunRegistCurrCount += 1;
            if (enemyStunRegistMaxCount <= enemyStunRegistCurrCount)
            {
                HitReaction();
                Debug.Log("����");
                enemyStunRegistCurrCount = 0;
                isStun = true;
                enemyAnimator.SetTrigger("Hit");
            }
        }

        //�ǰݰ��� ���(�ִϸ��̼�, �Ҹ�)
        //enemyAudio.PlayOneShot(enemyAudioManager.GetAudioClip(gameObject.name, "Hit"));

        //�÷��̾��� �������� ü���� ����� ���·� �´�.
        if (enemyHp <= 0)
        {
            Dead();
            return;
        }

        //������ ������ �÷��̾� �ν�
        target = FindObjectOfType<PlayerMove>();

        if (isStun)
        {
            if (stunCoroutine != null)
            {
                StopCoroutine(stunCoroutine);
            }
            //�����ð� ����
            enemyAnimator.SetBool("Stun", true);
            stunCoroutine = StartCoroutine(StunDelay(1f));
        }
    }

    //���� hit�� ����
    //�⺻�� �� �ٲ��
    public virtual void HitReaction()
    {
        //�� �ٲ�� ���׼�
    }

    //���� ������
    //���� Ȯ�强�� ���ؼ� virtual�� ����(������ ȿ���ִ� ����)
    public virtual void Dead()
    {
        //�������� ���(�ִϸ��̼�, �Ҹ�)
        enemyAnimator.SetBool("Dead", true);
        //enemyAudio.PlayOneShot(enemyAudioManager.GetAudioClip(gameObject.name, "Dead"));

        Debug.Log(gameObject.name+"����");
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
        if (direction == DirectionHorizen.LEFT)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            sight.RotateAngleZ(-90);
        }
        else if (direction == DirectionHorizen.RIGHT)
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
            Touch(collision.collider.GetComponent<PlayerMove>());
        }
    }

    //�����ð����� ������ �ɸ���.
    public IEnumerator StunDelay(float time)
    {
        yield return new WaitForSeconds(time);
        isStun = false;
        enemyAnimator.SetBool("Stun", false);        
    }    
}


public enum DirectionHorizen
{
    LEFT = -1,
    RIGHT = 1
}