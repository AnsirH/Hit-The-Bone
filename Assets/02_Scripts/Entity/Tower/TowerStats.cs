using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : EntityStats
{
    public TowerStats(float attack, float attackSpeed, float range) : base()
    {
        defaultStats.Add("Attack", attack);
        defaultStats.Add("AttackSpeed", attackSpeed);
        defaultStats.Add("Range", range);

        addedStats.Add("Attack", 0);
        addedStats.Add("AttackSpeed", 0);
        addedStats.Add("Range", 0);

        multipledStats.Add("Attack", 1);
        multipledStats.Add("AttackSpeed", 1);
        multipledStats.Add("Range", 1);
    }
}
