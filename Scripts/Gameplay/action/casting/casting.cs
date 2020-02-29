using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class casting : spawnObj
{

    public override void act()
    {
        base.act();
        newObj.GetComponent<spellDraft>().team = unit.tags.team;
        newObj.GetComponent<spellDraft>().currentMana = manaCost;
    }
}
