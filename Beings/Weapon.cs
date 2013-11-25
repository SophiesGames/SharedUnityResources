using UnityEngine;
using System.Collections;

public abstract class Weapon : GameItem
{
    protected int minDamage = 1;
    protected int maxDamage;

    public abstract int GetDamage();
}
