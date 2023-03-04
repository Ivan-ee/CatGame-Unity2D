using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MeleeAttack : MonoBehaviour
{
    private float AttackTime;
    //[SerializeField] Animator _animator;
    [SerializeField] private LayerMask Enemys;
    [SerializeField] private float playerDamage = 50f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRangeX = 2f;
    [SerializeField] private float attackRangeY = 2f;
    private float startAttack;
    
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        { 
            Attack();
        }
    }    
    private void Attack()
    {
        if (Time.time > AttackTime + attackCooldown)
        {
            var hitEnemys = Physics2D.OverlapBoxAll(attackPos.position,
                new Vector2(attackRangeX, attackRangeY), 0, Enemys);
            foreach (var VARIABLE in hitEnemys)
            {
                VARIABLE.GetComponent<Enemy>().TakeDamage(playerDamage);
            }
            AttackTime = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position,new Vector2(attackRangeX, attackRangeY));
    }
}
