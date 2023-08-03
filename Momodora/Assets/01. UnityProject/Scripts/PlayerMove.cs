using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D playerRigidbody = default;

    private float moveForce = default;     // ĳ���Ͱ� ������ �� ��ġ
    private float xInput = default;     // ���� ������ �Է°�
    private float zInput = default;     // ���� ������ �Է°�
    private float xSpeed = default;     // ���� ������ ������
    private float zSpeed = default;     // ���� ������ ������
    private float jInput = default;
    private float jumpForce = default;
    private int jumpCount = default;

    private void Awake()
    {
             // { ���� �� ����
        playerRigidbody = GetComponent<Rigidbody2D>();

        moveForce = 8f;
        xInput = 0f;
        zInput = 0f;
        xSpeed = 0f;
        zSpeed = 0f;
        jInput = 0f;
        jumpForce = 200f;
        jumpCount = 0;
             // } ���� �� ����
    }     // End Awake()

    void Update()
    {
        jInput = Input.GetAxis("Jump");
        xInput = Input.GetAxis("Horizontal");     // ���� �Է°� ����
        zInput = Input.GetAxis("Vertical");     // ���� �Է°� ����
        xSpeed = xInput * moveForce * Time.deltaTime;     // ���� �Է��� �����Ѹ�ŭ ���� ����
        zSpeed = zInput * moveForce * Time.deltaTime;     // ���� �Է��� �����Ѹ�ŭ ���� ����
        if (zSpeed < 0f)     // �Ʒ� ���� �Է°� Ȯ��
        {
            Debug.Log("���� ����");
        }
        Vector3 newvelocity = new Vector3(xSpeed, 0f, zSpeed);     // ����, ���� �Է°���ŭ �÷��̾� �̵� ��ǥ ����
        transform.Translate(Vector3.right * xSpeed);     // Ȯ�ε� ��ġ�� ��ǥ��ŭ �÷��̾� �̵�
        if (jInput > 0f)
        {
            jInput = 0f;
            PlayerJump();
        }
    }     // End Update()

    private void PlayerJump()
    {
        if (jumpCount >= 2) { return; }

        jumpCount += 1;
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(new Vector2(0, jumpForce));
        //else if (Input.GetMouseButtonDown(0) && 0 < playerRigid.velocity.y)
        //{
        //    playerRigid.velocity = playerRigid.velocity * 1f;
        //}
    }     // End PlayerJump()

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (0.7f < collision.contacts[0].normal.y)
        {
            //isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //isGrounded = false;
    }
}
