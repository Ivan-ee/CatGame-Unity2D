using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeedX = 10f;
    [SerializeField] private float moveSpeedY = 10f;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private bool _canMove = true;

    [Range(0, 1f)] [SerializeField] private float movementSmooth = 0.5f;
    private Vector2 _velocity = Vector2.zero;
    
    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(float vMove, float hMove, bool jump)
    {
        if (_canMove)
        {
            var targetVelocity = new Vector2(moveSpeedY * hMove, moveSpeedX * vMove);
            _rigidbody2D.velocity = 
                Vector2.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, movementSmooth);
            var isFlippedX = _spriteRenderer.flipX ? (targetVelocity.x >= 0) : (targetVelocity.x < 0);
            if (isFlippedX)
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}
