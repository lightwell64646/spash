using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldProjectile : projectile
{
    public int damage = 10;
    public int team;
    public Object hitSprite;
    public float blockFactor = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        myTags tags = other.GetComponent<myTags>();
        if (tags != null)
        {
            if (tags.isWall)
            {
                Destroy(gameObject);
            }
            if (tags.team != team && tags.team != 0)
            {
                unitInterface otherInterface = other.GetComponent<unitInterface>();
                Vector2 hitDirection = transform.position - otherInterface.transform.position;
                int blocked = otherInterface.applyHit(hitDirection);
                if (blocked == 1) otherInterface.resourceHandler.applyShieldDamage((int)(damage*blockFactor));
                else if (blocked == 0) otherInterface.resourceHandler.applyShieldDamage(damage);
                Instantiate(hitSprite, other.transform.position, other.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
