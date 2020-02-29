using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporary : expire
{
    override protected void onExpire()
    {
        Destroy(gameObject);
    }
}
