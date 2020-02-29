using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeAction : command
{
    public Vector2 focus;
    public int focusEpsilon;
    public action act;

    private bool used = false;

    public bool startup()
    {
        unit.focusPoint = focus;
        return true;
    }

    public bool execute()
    {
        if (Vector2.Angle(focus - (Vector2)unit.transform.position, unit.transform.right) < focusEpsilon)
        {
            used |= unit.use(act);
        }
        if (used && act.stage == act.recovery) return true;
        return false;
    }
}
