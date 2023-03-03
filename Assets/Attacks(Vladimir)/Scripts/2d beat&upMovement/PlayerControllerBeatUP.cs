using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBeatUP : CharacterMovement
{
    private float vMove;
    private float hMove;
    private bool canJump = true;
    void Update()
    {
        vMove = Input.GetAxis("Vertical");
        hMove = Input.GetAxis("Horizontal");
        Move(vMove,hMove,canJump);
    }
}
