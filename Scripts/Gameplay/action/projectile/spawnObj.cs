using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObj : action
{
    public Transform spawner;
    public Object obj;

    protected GameObject newObj;

    public override void act()
    {
        newObj = (GameObject)Instantiate(obj, spawner.position, spawner.rotation);
    }
}
