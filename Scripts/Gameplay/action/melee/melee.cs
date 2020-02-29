using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour {

    public int shieldDamage = 50;
    public int hpDamage = 1;
    public int recharge = 30;
    public int actionPriority = 1;
    public Object slashSprite;
    public unitInterface unit;
    public SpriteRenderer hitIndicator;

    public int r = 0;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (r > 0)  r -= 1;
        if (unit.actionLevel>=actionPriority) hitIndicator.enabled = false;
        else hitIndicator.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (r <= 0 && unit.actionLevel < actionPriority)
        {
            unitInterface otherInterface = other.GetComponent<unitInterface>();
            if (otherInterface != null && otherInterface.tags.team != unit.tags.team)
            {
                int blocked = otherInterface.applyHit(transform.position - other.transform.position);

                if (blocked == 1) otherInterface.resourceHandler.applyDamage(shieldDamage, hpDamage);
                else if (blocked == 0) otherInterface.resourceHandler.applyHpDamage(hpDamage);

                Instantiate(slashSprite, other.transform.position, other.transform.rotation);
                r = recharge;
            }
        }
    }
}
