using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitInterface : MonoBehaviour
{
    public Vector2 focusPoint;
    public movement movementScript;
    public resources resourceHandler;
    public List<hitModifier> hitMods = new List<hitModifier>();

    public int actionLevel = 0;
    public action currentAction = null;
    public myTags tags;

    public float knockbackSusceptibility = 1;

    public SpriteRenderer sprite;

    void Start()
    {
        foreach (hitModifier mod in GetComponents<hitModifier>())
        {
            hitMods.Add(mod);
            mod.setUnit(this);
        }
        movementScript.unit = this;
    }

    public bool use(action act)
    {
        return act.use(this);
    }

    public int applyHit(Vector2 hitDirection)
    {
        int res = 0;
        foreach (hitModifier mod in hitMods) res = Mathf.Max(mod.applyHit(hitDirection), res);
        return res;
    }

    public void applyKnockBack(Vector2 hitdirection, float hitForce, uint hitStun)
    {
        movementScript.rigidbodyCast.velocity = (hitdirection + movementScript.getMoveDir()).normalized * hitForce * knockbackSusceptibility;
        movementScript.disableMove(hitStun);
        action stun = new action();
        stun.startup = hitStun;
        stun.interuptPriority = 7; //arbitrary large number
        use(stun);
    }
}
