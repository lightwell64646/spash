using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour, hitModifier
{
    public float blockAngle = 60;
    public int blockStrength = 1;
    public int actionPriority = 1;
    public SpriteRenderer blockIndicator;
    public unitInterface unit;
    public Vector3 blockDirection;

    public void setUnit(unitInterface unitI)
    {
        unit = unitI;
    }

    public int applyHit(Vector2 hitDirection)
    {
        if (Vector2.Angle(blockDirection, hitDirection) < blockAngle &&
            unit.actionLevel < actionPriority)
        {
            return blockStrength;
        }
        return 0;
    }

    void FixedUpdate()
    {
        if (unit.actionLevel < actionPriority) blockIndicator.enabled = true;
        else blockIndicator.enabled = false;
    }
}
