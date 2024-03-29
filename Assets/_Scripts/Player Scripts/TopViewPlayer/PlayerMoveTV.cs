using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTV : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float _speed;

    private Vector2 _moveInput;
    private Rigidbody2D _rigidbody;

    private KeyCode[] _keys = new KeyCode[] {KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D};
    private int[] _codes = new[] {0, 180, 90, -90 };
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _rigidbody.gravityScale = 0;
    }
    private void FixedUpdate()
    {
        _moveInput.x = Input.GetAxis("Horizontal");
        _moveInput.y = Input.GetAxis("Vertical");

        Rotation();
        
        _rigidbody.MovePosition(_rigidbody.position + _moveInput * (_speed * Time.fixedDeltaTime));
    }

    void Rotation()
    {
        Quaternion rot = transform.rotation;
        
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKey(_keys[i]))
            {
                rot = Quaternion.Euler(0,0,_codes[i]);
            }
        }

        transform.rotation = rot;

    }
}
