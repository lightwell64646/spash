using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : myOrbit
{

    public float groundSpeed = 10;
    public float wallSpeed = 10;
    public float thrusterStrength = 10;
    public float jumpSpeed = 10;
    public float maxSpaceSpeed = 6;
    public float dragForce = 2;
    public float groundAccelRate = 0.1f;
    public float wallAccelRate = 0.02f;
    public float groundturnRate = 0.4f;
    public float SpaceturnRate = 0.2f;
    public float jumpGroundLength = 30;

    public unitInterface unit;

    public bool latched = false;

    private float epsilon = 0.001f;
    private Vector2 moveDir;

    public void Start()
    {
        moveDir.x = 0;
        moveDir.y = 0;
        base.Start();
    }

    Vector2 getWallNormal()
    {
        Vector2 pos2d = transform.position;
        Vector2 closest = wallCollider.ClosestPoint(pos2d);
        return pos2d - closest;
    }

    public void move(Vector2 direction)
    {
        moveDir = direction.normalized;
    }

    public void latch()
    {
        if (onWall != 0)
        {
            latched = true;
        }
    }

    public void drop()
    {
        latched = false;
    }

    override public void FixedUpdate()
    {
        //update position
        if (onWall != 0 && latched)
        {
            Vector2 wallNorm = getWallNormal();
            Vector2 forward = new Vector2(
                -wallNorm.y,
                wallNorm.x);
            float chosenWallDir = Vector2.Dot(moveDir, forward);
            if (chosenWallDir > epsilon || chosenWallDir < -epsilon)
            {
                rigidbodyCast.velocity = (Mathf.Sign(chosenWallDir) * forward.normalized * wallSpeed * wallAccelRate +
                                            rigidbodyCast.velocity * (1 - wallAccelRate));
            }
            else
            {
                rigidbodyCast.velocity = rigidbodyCast.velocity * (1 - wallAccelRate);
            }
        }
        else if (onGround != 0)
        {
            rigidbodyCast.velocity = (moveDir * groundSpeed * groundAccelRate +
                                             rigidbodyCast.velocity * (1 - groundAccelRate));
        }
        else
        {
            rigidbodyCast.AddForce(moveDir * thrusterStrength);
            if (rigidbodyCast.velocity.magnitude > maxSpaceSpeed)
            {
                rigidbodyCast.AddForce(rigidbodyCast.velocity * -dragForce);
            }
        }

        //update rotation
        Vector2 lookVector = unit.focusPoint - (Vector2)transform.position;
        float targetAngle = Vector2.SignedAngle(transform.right, lookVector);

        Vector3 vec = Vector3.zero;
        float turnRate = groundturnRate;
        if (onGround == 0 && onWall == 0) turnRate = SpaceturnRate;
        if (targetAngle > turnRate) vec.z = turnRate; 
        else if (targetAngle < -turnRate) vec.z = -turnRate; 
        else vec.z = targetAngle;
        transform.Rotate(vec);
        rigidbodyCast.angularVelocity = 0;

        if (onWall == 0) latched = false;

        base.FixedUpdate();
    }
}