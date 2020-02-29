using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellDraft : myOrbit
{
    public List<spellNode> nodes;
    public int team;
    public Evocation evocation;
    public int maxMana;

    public unitInterface controller { get; private set; }
    public int controlFactor { get; private set; }
    public int currentMana;

    public void Start()
    {
        foreach (spellNode node in GetComponents<spellNode>())
        {
            nodes.Add(node);
            node.setUnit(this);
        }
        base.Start();
    }

    public void Command(unitInterface commander)
    {
        if (commander == controller)
        {
            Move(commander.focusPoint);
        }
    }

    protected void Move(Vector2 target)
    {

    }

    public void Activate(unitInterface activator)
    {
        if (activator == controller && currentMana >= evocation.manaCost)
        {
            if (evocation.checkSigils(activator))
            {
                currentMana -= evocation.manaCost;
                evocation.begin(activator);
            }
        }
    }

    public void Expire()
    {
        Destroy(gameObject);
    }

    public void Independent()
    {

    }

    public void FixedUpdate()
    {
        if (currentMana <= 0)
            Expire();
        Dictionary<unitInterface, int> map = new Dictionary<unitInterface, int>();
        int controlledNodes = 0;
        unitInterface strongestcontroller = null;
        foreach (spellNode node in nodes)
        {
            if (node.controller != null){
                currentMana += controller.feedSpell(team) % maxMana;
                controlledNodes += 1;
                if (map.ContainsKey(node.controller))
                    map[node.controller] += 1;
                else
                    map.Add(node.controller, 1);
                if (map[node.controller] > controlledNodes/2 ||
                        (map[node.controller] == controlledNodes / 2 && node.controller == controller))
                    strongestcontroller = node.controller;
            }
        }
        controller = strongestcontroller;
        controlFactor = map[controller];
        
        if (controller == null)
        {
            Independent();
        }
        base.FixedUpdate();
    }
}
