using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class action : MonoBehaviour {
    public uint startup = 5;
    public uint active = 0;
    public uint recovery = 5;
    public uint coolDown = 0;
    public int manaCost = 0;
    public int interuptPriority = 1;

    public uint cd = 0;
    public uint stage = 0;
    protected uint sustained;
    protected unitInterface unit;

    public void begin(unitInterface unitI)
    {
        unit = unitI;
        stage = startup + active + recovery;
        cd = coolDown + startup + active + recovery;
    }

    public bool use(unitInterface unitI)
    {
        sustained = 1;
        if (cd <= 0 && unitI.actionLevel < interuptPriority)
        {
            if (manaCost == 0 || unitI.resourceHandler.mana.use(manaCost))
            {
                begin(unitI);

                if (unitI.currentAction != null) unitI.currentAction.interrupt();
                unitI.currentAction = this;
                unitI.actionLevel = interuptPriority;
                return true;
            }
        }
        return false;
    }

    public void FixedUpdate()
    {
        if (cd > 0)
            cd -= 1;
        if (stage > 0)
        {
            if (stage > recovery+active) startupAct();
            else if (stage == recovery + active) initialAct();
            else if (stage > recovery) act();
            else if (stage == recovery) tearDownAct();
            else recoveryAct();
            if (stage == 1)
            {
                unit.actionLevel = 0;
                unit.currentAction = null;
            }
            stage -= 1;
        }
        sustained = 0;
    }

    public void interrupt() {stage = 0;}

    public virtual void startupAct() { }
    public virtual void initialAct() { }
    public virtual void act() { }
    public virtual void tearDownAct() { }
    public virtual void recoveryAct() { }
}
