using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    //����� �ݶ��̴�
    private CircleCollider2D circleCollider = default;

    //�þ� �Ÿ�
    //circle �ݶ��̴��� �������� ����
    public float radius = default;

    //0~360���� �Է� ����
    [Range(0f, 360f)]
    //���ϴ� �þ� ����
    public float sightAngleRange = 90f;
    private float sightAngleHalfRange = 0f;

    //����þ߸� �������� �߰������� ȸ�������� ������ �����ϴ� ����
    //����up (0,1), y�� ����� ������ ���ػ�� �ð����/�ݽð�������� ����Ǵ� ����
    //�� 0 ~ 360�� -180 ~ 180���� �� ����.
    [Range(-180f, 180f)]
    public float sightRotateToZ = -90f;

    //����� ����
    public bool onDebug = false;


    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = radius;

        sightAngleHalfRange = sightAngleRange * 0.5f;
    }


    //ȸ���Ҷ� �ַ� ���.
    public void RotateAngleZ(float angle)
    {
        sightRotateToZ = angle;
    }

    // �Է��� -180~180�� ���� Up Vector ���� Local Direction���� ��ȯ
    // y���� ��� ����(0,1) �̴� ���ۼ��� -> y���� ������⿡�� �Է��� sightRotateToZ��ŭ ȸ�����Ѽ� ���ۼ��� �׸���.
    private Vector3 GetAngleToUpVector(float sightRotateToZ)
    {
        //angleInDegree : ���ϴ� ����
        //transform.eulerAngles.z ���� ȸ������ ����

        //���� ȸ�� �ν� -> ������ ȸ��(�ð����)�� ����� ǥ��
        //eulerAngles.z -> ������ ȸ��(�ð����)�� ������ ǥ�� 

        // ex) z�� -10��(������ǥ 10��) + ���ϴ� ���� 10�� -> 20������ ����
        float radian = (sightRotateToZ - transform.eulerAngles.z) * Mathf.Deg2Rad;

        //x(�غ�),y(����),r(�밢��),t(����)�� �̷��� �ﰢ������ y=r*sin(t), x=r*cos(t) ������
        //�ش� �Լ��� y���� �غ����� �ؼ� ������ �׸��� ������ �̸� �ٲ㼭 vector �� �������.
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }


    //�÷��̾ ������ ���� ��� ����
    //���� �Ϸ��� �ش� ������Ʈ�� disable ��Ų��
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 myPosition = transform.position;
            Vector2 targetPosition = other.transform.position;

            //��(����) ��ġ�� �������� ���� ����(�÷��̾�)�� ����
            Vector2 dir = (targetPosition - myPosition).normalized;
            //��(����)�� �ٶ󺸴� ���ۼ�
            Vector2 lookDir = GetAngleToUpVector(this.sightRotateToZ);

            //���ۼ��� ������ ��������� ������ ����
            float angle = Vector3.Angle(lookDir, dir);

            //���ۼ����� ���� ������ +/- �������� 2���� ������ �ɼ� �����Ƿ�
            //������ ���� ���� ���Ѵ�.
            if (angle <= sightAngleHalfRange)
            {
                //Ÿ�� ���� �Ϸ�
                //�ѹ� Ÿ���� �����Ǹ� ���� ��������� �Ѿƿ´�.
                EnemyBase tmp = GetComponentInParent<EnemyBase>();
                tmp.target = other.GetComponent<PlayerMove>();

                //Ÿ���� �����Ǹ� �ش� ����� �� �ʿ䰡 ���⶧���� ��Ȱ��ȭ ��Ų��.
                gameObject.SetActive(false);
                //Debug.Log("�νĿϷ�");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (onDebug)
        {
            sightAngleHalfRange = sightAngleRange * 0.5f;

            Vector3 originPos = transform.position;

            Gizmos.DrawWireSphere(originPos, radius);

            Vector3 horizontalRightDir = GetAngleToUpVector(-sightAngleHalfRange + this.sightRotateToZ);
            Vector3 horizontalLeftDir = GetAngleToUpVector(sightAngleHalfRange + this.sightRotateToZ);
            Vector3 lookDir = GetAngleToUpVector(this.sightRotateToZ);

            Debug.DrawRay(originPos, horizontalLeftDir * radius, Color.cyan);
            Debug.DrawRay(originPos, lookDir * radius, Color.green);
            Debug.DrawRay(originPos, horizontalRightDir * radius, Color.cyan);
        }
    }
}
