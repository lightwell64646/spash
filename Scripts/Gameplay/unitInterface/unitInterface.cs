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

    public int spellFeedingPower = 1;
    
    public float knockbackSusceptibility

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

    public void applyKnockBack(Vector2 hitForce)
    {

    }

    public int feedSpell(int team)
    {
        int amount;
        if (resourceHandler.mana.use(spellFeedingPower))
        {
            if (team == tags.team)
                return spellFeedingPower;
            return -spellFeedingPower;
        }
        return 0;
    }
}
