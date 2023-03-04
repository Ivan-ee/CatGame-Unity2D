using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    private enum TypeOfView{ SIDE, TOP }
    
    [Header("Moving")] 
    [SerializeField] private TypeOfView _typeOfView = TypeOfView.SIDE;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _extraJumpValue;

    [Header("Ground check")] 
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;
    
    private Vector2 _moveInput;
    private int _extraJump;
    private bool _isGrounded;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _extraJump = _extraJumpValue;
    }

    private void Update()
    {
        
        switch (_typeOfView)
        {
            case TypeOfView.SIDE:
                
                if (_isGrounded==true)
                {
                    _extraJump = _extraJumpValue;
                }

                if (Input.GetKeyDown(KeyCode.Space) && _extraJump > 0)
                {
                    _rigidbody.velocity = Vector2.up * _jumpForce;
                    _extraJump--;
                }
                else if (Input.GetKeyDown(KeyCode.Space) && _extraJump == 0 && _isGrounded == true)
                {
                    _rigidbody.velocity = Vector2.up * _jumpForce;
                }
                break;
            
            case TypeOfView.TOP:
                // прыжки
                break;
                
        }
    }

    private void FixedUpdate()
    {
        _moveInput.x = Input.GetAxis("Horizontal");
        _moveInput.y = Input.GetAxis("Vertical");

        Rotation(_moveInput.x);
        
        switch (_typeOfView)
        {
            case TypeOfView.SIDE:

                _rigidbody.gravityScale = 1;
                
                _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
        
                
                _rigidbody.velocity = new Vector2(_moveInput.x * _speed, _rigidbody.velocity.y);
                break;
            
            case TypeOfView.TOP:
                
                _rigidbody.gravityScale = 0;
                
                _rigidbody.MovePosition(_rigidbody.position + _moveInput * (_speed * Time.fixedDeltaTime));
                break;
        }
    }

    void Rotation(float direction)
    {
        Quaternion rot = transform.rotation;
        
        if (direction < 0)
        {
            rot.y = 0;
        }
        if (direction > 0)
        {
            rot.y = 180;
        }
        
        transform.rotation = rot;
    }
} 
