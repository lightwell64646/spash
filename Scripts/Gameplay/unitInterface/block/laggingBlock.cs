using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laggingBlock : block
{
    public Transform blockTransform;
    public float turnRate = 5;

    // Update is called once per frame
    void FixedUpdate()
    {
        float targetAngle = Vector2.SignedAngle(blockTransform.right, transform.right);

        Vector3 vec = Vector3.zero;
        if (targetAngle > turnRate) vec.z = turnRate;
        else if (targetAngle < -turnRate) vec.z = -turnRate;
        else vec.z = targetAngle;
        blockTransform.Rotate(vec);
        blockDirection = blockTransform.right;
    }
}
