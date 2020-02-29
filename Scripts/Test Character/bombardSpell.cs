using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombardSpell : spellDraft
{
    public int fireDelay = 5;
    public Object bolt;
    public Transform top, bottom;

    private int fireCount = 0;
    private bool fireTop;

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
        if (fireCount == 0)
        {
            fireCount = fireDelay;
            GameObject newBolt;
            if (fireTop) newBolt = (GameObject)Instantiate(bolt, top.position, top.rotation);
            else newBolt = (GameObject)Instantiate(bolt, bottom.position, bottom.rotation);
            newBolt.GetComponent<shieldProjectile>().team = team;
            fireTop = !fireTop;
        }
        fireCount -= 1;
        top.Rotate(new Vector3(0, 0, Vector2.SignedAngle((Vector2)top.right, caster.focusPoint - (Vector2)top.position)));
        bottom.Rotate(new Vector3(0, 0, Vector2.SignedAngle((Vector2)bottom.right, caster.focusPoint - (Vector2)bottom.position)));
    }
}
