using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowShot : spawnObj
{
    public int maxDrawDuration = 60;
    public int initialDamage = 30;
    public int maxDamage = 80;
    public int initialSpeed = 20;
    public int maxSpeed = 30;

    private int draw = 0;
    // Start is called before the first frame update
    public override void initialAct()
    {
        if (sustained != 0 && draw <= maxDrawDuration)
        {
            draw += 1;
            stage += 1;
            cd += 1;
        }
        else
        {
            GameObject arrow = (GameObject)Instantiate(obj, spawner.position,
                spawner.rotation);
            damageProjectile proj = arrow.GetComponent<damageProjectile>();
            proj.shieldDamage = (initialDamage +
                (maxDamage - initialDamage) * draw / maxDrawDuration);
            proj.hpDamage = 1;
            proj.launchSpeed = (initialSpeed +
                (maxSpeed - initialSpeed) * draw / maxDrawDuration);
            proj.team = transform.GetComponent<myTags>().team;
            draw = 0;
            Debug.Log("Loose");
        }
    }
}
