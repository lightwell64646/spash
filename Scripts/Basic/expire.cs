using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expire : MonoBehaviour
{
    public uint duration = 5;

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (duration == 0) onExpire();
        duration -= 1;
    }

    virtual protected void onExpire()
    {

    }
}
