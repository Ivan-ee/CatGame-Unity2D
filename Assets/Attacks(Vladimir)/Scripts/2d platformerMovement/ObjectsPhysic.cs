using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ObjectsPhysic : MonoBehaviour
{
    protected readonly float minMoveDistance = 0.001f;
    protected readonly float shellRadius = 0.01f;
    protected float minGroundNormalY = 0.65f;
    [SerializeField] private float gravityMod = 1;
    
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Vector2 velocity;
    protected Vector2 targetVelocity;
    protected Rigidbody2D rb;
    protected ContactFilter2D contactFilter;
    protected readonly RaycastHit2D[] raycastHitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> raycastHitBufferList = new List<RaycastHit2D>(16);
    
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {
        
    }

    private void FixedUpdate()
    {
        velocity += gravityMod * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;
        grounded = false;
        var deltaPos = velocity * Time.deltaTime;
        var moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        var move = moveAlongGround * deltaPos.x;
        Movement(move, false);
        move = Vector2.up * deltaPos.y;
        Movement(move, true);
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
        if (distance > minMoveDistance)
        {
            int count = rb.Cast(move, contactFilter, raycastHitBuffer, distance + shellRadius);
            raycastHitBufferList.Clear();
            for (int i = 0; i < count; i++) {
                raycastHitBufferList.Add (raycastHitBuffer [i]);
            }

            for (int i = 0; i < raycastHitBufferList.Count; i++) 
            {
                Vector2 currentNormal = raycastHitBufferList [i].normal;
                if (currentNormal.y > minGroundNormalY) 
                {
                    grounded = true;
                    if (yMovement) 
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                var projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity -= projection * currentNormal;
                }

                var modifiedDistance = raycastHitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        rb.position += move;
    }
}
