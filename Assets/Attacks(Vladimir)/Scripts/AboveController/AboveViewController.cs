using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboveViewController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10;
    [SerializeField] private Camera _camera;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }
    

    private void Move()
    {
        var deltaXPos = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var deltaYPos = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        _rigidbody.velocity = new(deltaXPos, deltaYPos);
    }
    private void Rotation()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
