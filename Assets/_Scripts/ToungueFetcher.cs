using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ToungueFetcher : MonoBehaviour
{
    private float tongueTime;
    [SerializeField] private float speed = 20f;
    [SerializeField] private LayerMask Enemys;
    [SerializeField] private float tongueCooldown = 1f;
    [SerializeField] private Transform tonguePos;
    [SerializeField] private float tongueRangeX = 2f;
    [SerializeField] private float tongueRangeY = 2f;
    private float startAttack;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TongueGrow();
        }
    }

    private void TongueGrow()
    {
        if (Time.time > tongueTime + tongueCooldown)
        {
            var hitEnemy = Physics2D.OverlapBox(tonguePos.position,
                new Vector2(tongueRangeX, tongueRangeY), 0, Enemys);
            if(hitEnemy != null)
                hitEnemy.GetComponent<Rigidbody2D>().AddForce(-hitEnemy.GetComponent<Transform>().up * speed,
                    ForceMode2D.Impulse);
            tongueTime = Time.time;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(tonguePos.position,new Vector2(tongueRangeX, tongueRangeY));
    }
}
