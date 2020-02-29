using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sigil : int
{
    White = 0, Black = -1,
    Yellow = 1, Red = 4,
    Teal = 2, Green = 5,
    Violet = 3, Blue = 6,
    Any = 7
}

public class SigilArray : MonoBehaviour
{
    public const int SuperChargeOffset = 3;

    unitInterface unit = null;
    public List<Sigil> active = new List<Sigil>();
    public List<Sigil> ready = new List<Sigil>();
    public List<Sigil> burnt = new List<Sigil>();
    public float unburnTime = 3;
    private float refreshTimer = 0;

    int maxActive = 10;
    int maxReady = 5;
    int maxBurnt = 10;

    public void Start()
    {
        if (unit == null)
            unit = GetComponent<unitInterface>();
    }

    public void Quicken(Sigil s)
    {
        if (active.Count < maxActive)
            active.Add(s);
        else
            burn(s);
    }

    public void burn(Sigil s)
    {
        if (burnt.Count < maxBurnt)
            burnt.Add(s);
        else
            overChannel(s);
        if (burnt.Count == 1)
            refreshTimer = unburnTime;
    }

    public void sign(Sigil s)
    {
        if (active.contains(s))
        {
            active.Remove(s);
            ready.Add(s);
            if (ready.Count > maxReady)
            {
                ready.RemoveAt(0);
                Quicken(s);
            }
        }
    }

    public Sigil Tap()
    {
        if (ready.Count != 0)
        {
            Sigil tappedSigil = ready[ready.Count - 1];
            ready.RemoveAt(ready.Count - 1);
            Quicken(tappedSigil);
        }
    }

    public Sigil check(List<Sigil> cost)
    {
        int j;
        Sigil x = Sigil.Any;
        for (j = 0; j < ready.Count; j++) {
            int jcopy = j;
            if (cost[0] == ready[j]) {
                bool works = true;
                int mischannels = 0;
                for (int i = 1; i < cost.Count; i++)
                {
                    if (jcopy > ready.Count)
                    {
                        works = false; // no more sigils left to read abort
                        break;
                    }
                    if (cost[i] == ready[jcopy] + SuperChargeOffset) { } // superchanneled sigils are read as two of their base sigil
                    else if (ready[jcopy] == Sigil.Black)
                    {
                        mischannels += 1; // black sigils are read as any color but cause overchannel if used
                        jcopy += 1;
                        if (cost[i] == Sigil.Any)
                            x = Sigil.Black;
                    }
                    else if (cost[i] == Sigil.Any)
                    {
                        x = ready[jcopy]; // if a cost includes an any tell the caller what paid for the cost
                        jcopy += 1;
                    }
                    else if (cost[i] != ready[jcopy])
                    {
                        works = false; // ignoring the above sigils must match unless the cost is for any sigil
                        break;
                    }
                    else
                    {
                        jcopy += 1; // increment
                    }
                }
                if (works)
                {
                    bind();
                    while (mischannels-- > 0)
                        overChannel();
                    return x;
                }
            }
        }
        return x;
    }

    public void bind()
    {
        foreach(Sigil s in ready)
            burn(s);
        ready.Clear();
    }

    public void overChannel(Sigil s)
    {
        unit.resources.applyHpDamage(3);
    }

    public void FixedUpdate()
    {
        if (refreshTimer > 0)
        {
            refreshTimer -= Time.deltaTime;
            if (refreshTimer <= 0 && burnt.Count != 0)
            {
                Sigil mostRecentBurnt = burnt[burnt.Count - 1];
                burnt.RemoveAt(burnt.Count - 1);
                Quicken(mostRecentBurnt);
                if (burnt.Count != 0)
                    refreshTimer = unburnTime;
            }
        }
    }
}
