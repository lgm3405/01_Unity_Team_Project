using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D playerRigidbody = default;
    private SpriteRenderer playerRenderer = null;
    private Animator animator = default;

    private float moveForce = default;     // ĳ���Ͱ� ������ �� ��ġ
    private float rollForce = default;
    private float xInput = default;     // ���� ������ �Է°�
    private float zInput = default;     // ���� ������ �Է°�
    private float jInput = default;
    private float xSpeed = default;     // ���� ������ ������
    private float zSpeed = default;     // ���� ������ ������
    private float jSpeed = default;
    private float rSpeed = default;
    private float jumpForce = default;

    private int jumpCount = default;

    private bool jumping = false;
    private bool flipX = false;
    private bool isRolled = false;
    private bool rollingSlow = false;
    private bool isGrounded = false;
    private bool isCrouched = false;
    private bool isLadder = false;

    private void Awake()
    {
             // { ���� �� ����
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        moveForce = 8f;
        rollForce = 16f;
        xInput = 0f;
        zInput = 0f;
        jInput = 0f;
        xSpeed = 100f;
        zSpeed = 0f;
        jSpeed = 0f;
        rSpeed = 0f;
        jumpForce = 700f;

        jumpCount = 0;
             // } ���� �� ����
    }     // End Awake()

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");     // ���� �Է°� ����
        //zInput = Input.GetAxis("Vertical");     // ���� �Է°� ����

        if (isRolled == true && rollingSlow == false)
        {
            if (flipX == false)
            {
                rSpeed += rollForce;     // ���� �Է��� �����Ѹ�ŭ ���� ����
                Vector3 newVelocity2 = new Vector3(rSpeed, 0f, 0f);
                //transform.Translate(Vector3.right * rSpeed);
                playerRigidbody.velocity = newVelocity2;
            }
            else
            {
                rSpeed += rollForce;     // ���� �Է��� �����Ѹ�ŭ ���� ����
                Vector3 newVelocity2 = new Vector3(rSpeed, 0f, 0f);
                //transform.Translate(Vector3.right * -rSpeed);
                playerRigidbody.velocity = newVelocity2;
            }
        }
        else
        {
            xSpeed = xInput * moveForce;     // ���� �Է��� �����Ѹ�ŭ ���� ����
            zSpeed = zInput * moveForce;     // ���� �Է��� �����Ѹ�ŭ ���� ����
            Vector3 newVelocity = new Vector3(xSpeed, 0f, 0f);     // ����, ���� �Է°���ŭ �÷��̾� �̵� ��ǥ ����
            /*transform.Translate(Vector3.right * xSpeed);*/     // Ȯ�ε� ��ġ�� ��ǥ��ŭ �÷��̾� �̵�
            //Debug.LogFormat("�̵� ������ : {0}", xSpeed);
            playerRigidbody.velocity = newVelocity;
            Debug.LogFormat("�̵� ���� : {0}", xSpeed);
        }

        if (xSpeed > 0f)
        {
            if (flipX == true)
            {
                flipX = false;
                playerRenderer.flipX = false;
            }
        }

        if (xSpeed < 0f)
        {
            if (flipX == false)
            {
                flipX = true;
                playerRenderer.flipX = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && jumpCount < 2)
        {
            jumpCount += 1;
            jumping = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            jumping = false;
            jSpeed = 0f;
        }

        if (jumping == true)
        {
            jSpeed += jumpForce * Time.deltaTime;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jSpeed));

            if (jSpeed >= 30f)
            {
                jSpeed = 30f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && isRolled == false)
        {
            isRolled = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && isCrouched == false)
        {
            isCrouched = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && isCrouched == true)
        {
            isCrouched = false;
        }

        animator.SetBool("Ground", isGrounded);
        animator.SetBool("Roll", isRolled);
        animator.SetBool("Crouch", isCrouched);

        // ��ٸ����� ������ �� ����
        //playerRigidbody.constraints = RigidbodyConstraints2D.None;
        //playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
        
    public void PlayerRollingSlow()
    {
        if (isRolled == true)
        {
            rollingSlow = true;
            playerRigidbody.velocity *= 1f;
            StartCoroutine(SlowEnd());
        }
    }

    public void PlayerRollingEnd()
    {
        if (isRolled == true)
        {
            isRolled = false;
            rSpeed = 0f;
        }
    }

    IEnumerator SlowEnd()
    {
        yield return new WaitForSeconds(0.2f);

        rollingSlow = false;
    }

    //������ �������ش�
    //if(playerRigidbody.velocity.y > 500)
    //{
    //    playerRigidbody.velocity = new Vector2(0, 500);
    //}

    //    ����������ٺ��� velocity�� ������ �����ش�.
    //    else if (Input.GetMouseButtonDown(0) && 0 < playerRigid.velocity.y)
    //    {
    //        playerRigid.velocity = playerRigid.velocity * 1f;
    //    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Floor") && 0.7f < collision.contacts[0].normal.y)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Ladder") && Input.GetKey(KeyCode.UpArrow))
        {
            if (isLadder == false)
            {
                isLadder = true;
                playerRigidbody.velocity = Vector2.zero;
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                playerRigidbody.gravityScale = 0f;
                Debug.Log("��ٸ��� ��Ҵ�");
            }
        }
    }

    
}
