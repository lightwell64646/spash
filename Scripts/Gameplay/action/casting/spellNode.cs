using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellNode : MonoBehaviour
{
    public unitInterface controller = null;
    public bool active = true;

    public void aquire(unitInterface unit)
    {
        if (controller == null && active)
            controller = unit;
    }

    public void deactivate()
    {
        active = false;
        controller = null;
    }

    public void abandon(unitInterface unit)
    {
        if (controller == unit)
            controller = null;
    }

    public void FixedUpdate()
    {
        //pretty streams
    }
}
