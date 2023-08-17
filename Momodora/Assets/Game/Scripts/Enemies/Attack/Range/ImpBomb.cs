using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpBomb : EnemyAttackData
{
    Rigidbody2D bulletRigidbody;

    public GameObject poison;
    public bool isBoom = false;

    public float speed = 10;
    int direction;

    public override void UseEffect()
    {
        animator.SetTrigger("Attack");
    }
    public override void UseCollider()
    {
        isActive = true;
    }

    public override void Init()
    {
        base.Init();
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.right.x > 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        GetParabolaAtHeight(new Vector3(2.5f,1.5f), direction, bulletRigidbody);
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        //�΋Hĥ ��� ���ǿ��� ����
        if ((collision.tag == "Player" || collision.tag == "Wall" || collision.tag == "Floor") && !isBoom)
        {
            isBoom = true;
            CameraMove.ShakingCamera(Camera.main.GetComponent<CameraMove>());
            PlayerMove player = collision.GetComponentInParent<PlayerMove>();
            if (player != null)
            {
                //player.hp -= damage;
                player.Hit(damage, -direction);
            }
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            Instantiate(poison, transform.position, Quaternion.identity);
            Destroy(gameObject);

            //������� ���� �߰��ϴ°� �߰�
            //�÷��̾� ���� 
        }


    }


    //�׷���Ƽ ���� �������� ������
    private void GetParabolaAtHeight(Vector2 maxHeightDisplacement, int direction, Rigidbody2D rigid)
    {

        // m*k*g*h = m*v^2/2 (��, k == gravityScale) <= ������ ������ ���� ��Ģ ����
        float v_y = Mathf.Sqrt(2 * rigid.gravityScale * -Physics2D.gravity.y * maxHeightDisplacement.y);
        // ������ � ��Ģ ����
        float v_x = direction * maxHeightDisplacement.x * v_y / (2 * maxHeightDisplacement.y);

        Vector2 force = rigid.mass * (new Vector2(v_x, v_y) - rigid.velocity);
        rigid.velocity = force;
    }
}
