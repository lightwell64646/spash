using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accelerateTowards : command
{
    public Vector2 target;

    public bool execute()
    {
        if (unit.movementScript.onWall != 0)
        {
            unit.movementScript.latch();
            return true;
        }
        if (unit.movementScript.onGround != 0) return true;

        unit.movementScript.move(target - (Vector2)unit.transform.position);
        renderOrbit();
        return false;
    }

    private void renderOrbit()
    {

    }
}
