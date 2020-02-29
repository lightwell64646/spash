using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manaBombard : casting
{
    public int spellDuration = 300;

    // Start is called before the first frame update
    public override void initialAct()
    {
        base.act();
        newObj.transform.parent = spawner;
    }
}
