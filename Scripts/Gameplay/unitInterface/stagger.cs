using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stagger : expire, hitModifier
{
    public unitInterface unit;
    float staggerDurration;

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public int applyHit(Vector2 hitDirection)
    {
        return 2;
    }

    protected void onExpire()
    {
        unit.hitMods.Remove(this);
    }

    public void setUnit(unitInterface unitI)
    {
        unit = unitI;
    }
}
