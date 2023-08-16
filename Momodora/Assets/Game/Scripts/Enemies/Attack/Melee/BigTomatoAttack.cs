using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTomatoAttack : EnemyAttackData
{

    public override void UseEffect()
    {
        animator.SetTrigger("Attack");
    }
    public override void UseCollider()
    {
        isActive = true;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive && collision.tag == "Player")
        {
            isActive = false;
            collision.GetComponent<PlayerMove>().playerHp -= damage;
            //�÷��̾� ���� 
            //player.Hit();
        }

    }

}
