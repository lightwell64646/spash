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
        base.FixedUpdate();
        unit.resourceHandler.shields.regen((float)unit.resourceHandler.shields.maxVal / staggerDurration);
        Color spriteColor = unit.sprite.color;
        if (duration != 0) spriteColor.a = 0.5f;
        else spriteColor.a = 1f;
        unit.sprite.color = spriteColor;
    }

    public void setUnit(unitInterface unitI)
    {
        unit = unitI;
    }

    public int applyHit(Vector2 hitDirection)
    {
        return 2;
    }

    protected void onExpire()
    {
        unit.hitMods.Remove(this);
    }
}
