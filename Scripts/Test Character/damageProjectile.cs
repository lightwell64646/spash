using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageProjectile : projectile
{
    public int shieldDamage = 100;
    public int hpDamage = 1;
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
                if (blocked == 1) otherInterface.resourceHandler.applyDamage((int)(shieldDamage*blockFactor), hpDamage);
                else if (blocked == 0) otherInterface.resourceHandler.applyDamage(shieldDamage, hpDamage);

                Instantiate(hitSprite, other.transform.position, other.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
