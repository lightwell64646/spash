using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : action
{
    hitBox hiter;

    public virtual void startupAct() { }
    public virtual void initialAct() { }
    public virtual void act() { }
    public virtual void tearDownAct() { }
    public virtual void recoveryAct() { }
}
