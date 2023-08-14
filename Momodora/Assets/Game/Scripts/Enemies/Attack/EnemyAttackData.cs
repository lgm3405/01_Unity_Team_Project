using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackData : MonoBehaviour
{
    //Ÿ��
    public EnemyAttackType type;
    
    //�Ѿ�, ����Ʈ ��
    public SpriteRenderer attackSprite;
    public Animator animator;

    public bool isActive = false;

    //���ݷ�
    public int damage;

    public void Awake()
    {
        Init();
        Destroy(gameObject, 10f);
    }

    public virtual void Init()
    {
        attackSprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    public virtual void UseCollider()
    {
        isActive = true;
    }

    public virtual void UseEffect()
    {

        animator.SetTrigger("Attack");


    }

}


public enum EnemyAttackType
{
    RANGE,
    MELEE,
    NOTHING
}

public interface IAttackControl
{
    public void UseItem();
}