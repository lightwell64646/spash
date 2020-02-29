using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : action
{
    public float wallJumpStrength = 23;
    public float spaceJumpStrength = 15;
    public int maxJumps = 2;

    protected int jumpsRemaining = 0;

    public override void initialAct()
    {
        if (jumpsRemaining > 0)
        {
            jumpsRemaining -= 1;
            Vector2 jumpVect = unit.focusPoint - (Vector2)transform.position;
            if (unit != null && (unit.movementScript.onWall != 0))
                jumpVect = jumpVect.normalized * wallJumpStrength;
            else
                jumpVect = jumpVect.normalized * spaceJumpStrength;
            unit.movementScript.rigidbodyCast.velocity = jumpVect;
        }
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
        if (unit != null && (unit.movementScript.onGround != 0 || unit.movementScript.onWall != 0))
            jumpsRemaining = maxJumps;
    }
}

