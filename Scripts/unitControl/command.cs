using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class command
{
    public unitInterface unit;

    public void addCommand(unitInterface unitI)
    {
        unit = unitI;
    }

    public bool startup()
    {
        return true;
    }

    public bool execute()
    {
        return true;
    }
}
