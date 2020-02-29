using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : temporary
{
    private Rigidbody2D thisRigid;
    public float launchSpeed = 300;

    // Start is called before the first frame update
    void Start()
    {
        thisRigid = GetComponent<Rigidbody2D>();
        thisRigid.velocity = transform.right * launchSpeed;
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
        transform.Rotate(new Vector3(0, 0, Vector2.SignedAngle(transform.right, thisRigid.velocity)));
    }
}
