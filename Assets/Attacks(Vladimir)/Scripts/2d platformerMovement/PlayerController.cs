using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : ObjectsPhysic
{
    [SerializeField] private float jumpTakeOffSpeed = 7f;
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] protected bool canDabbleJump = false;
    private SpriteRenderer spriteRenderer;
    protected Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        var move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && grounded)
        {
            
            Debug.Log("jump was pressed on ground");
            Debug.Log($"grounded is {grounded.ToString()}");
            velocity.y = jumpTakeOffSpeed;
            canDabbleJump = true;
        }
        else if(Input.GetButtonDown("Jump") && canDabbleJump)
        {
            Debug.Log("jump was pressed in sky");
            velocity.y = jumpTakeOffSpeed * 2;
            canDabbleJump = false;
        }

        var flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f): (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x)/maxSpeed);
        targetVelocity = move * maxSpeed;
    }
}
