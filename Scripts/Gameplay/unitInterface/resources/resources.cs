using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resources : MonoBehaviour
{
    public resourceBar health, shields, mana;
    public SigilArray sigils;
    public unitInterface unit;

    public int outOfCombatTime = 300;
    public float shieldRegen = 30;
    public float shieldBreakTime = 10;

    //Private variables
    private int inCombat = 0;
    private float shieldRecharge = 0;

    public bool applyDamage(int shieldDamage, int hpDamage)
    {
        inCombat = outOfCombatTime;
        int origShields = shields.currentVal;
        if (shieldRecharge > 0 || !shields.use(shieldDamage))
        {
            if (shieldRecharge <= 0)
                shieldRecharge = shieldBreakTime;
            if (!health.use(hpDamage) || health.currentVal == 0)
            {
                unitDie();
                return true;
            }
            /*else
            {
                stagger staggerEffect = new stagger();
                staggerEffect.duration = staggerDurration;
                staggerEffect.unit = unit;
                unit.hitMods.Add(staggerEffect);
            }*/
        }
        return false;
    }

    public bool applyShieldDamage(int damage)
    {
        inCombat = outOfCombatTime;
        if (shieldRecharge <= 0 && !shields.use(damage))
        {
            shieldRecharge = shieldBreakTime;
            return true;
        }
        return false;
    }

    public bool applyHpDamage(int damage)
    {
        inCombat = outOfCombatTime;
        if (!health.use(damage) || health.currentVal == 0)
        {
            unitDie();
            return true;
        }
        return false;
    }

    void unitDie()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    void FixedUpdate()
    {
        if (inCombat > 0) inCombat -= 1;
        if (shieldRecharge > 0)
        {
            shieldRecharge -= Time.deltaTime;
            shields.regen(shields.maxVal * Time.deltaTime / shieldBreakTime);
            Color spriteColor = shields.sprite.color;
            if (shieldRecharge > 0)
                spriteColor.a = 0.25f;
            else
                spriteColor.a = 1.0f;
            shields.sprite.color = spriteColor;
        }
        else
            shields.regen(shieldRegen * Time.deltaTime);
    }
}
