using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evocation : action
{
    List<Sigil> sigilRequirements;
    Sigil checkResult;

    public bool checkSigils(unitInterface unit)
    {
        checkResult = unit.resources.sigils.check(sigilRequirements);
        return checkResult != Sigil.Any;
    }
}
