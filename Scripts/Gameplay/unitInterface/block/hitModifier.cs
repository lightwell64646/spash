using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface hitModifier
{
    int applyHit(Vector2 hitDirection);
    void setUnit(unitInterface unit);
}
