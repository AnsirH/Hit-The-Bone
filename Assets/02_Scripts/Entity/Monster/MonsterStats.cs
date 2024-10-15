using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : EntityStats
{
    public MonsterStats(float hp, float defense, float speed) : base() 
    {
        defaultStats.Add("Hp", hp);
        defaultStats.Add("Defense", defense);
        defaultStats.Add("Speed", speed);

        addedStats.Add("Hp", 0);
        addedStats.Add("Defense", 0);
        addedStats.Add("Speed", 0);

        multipledStats.Add("Hp", 1);
        multipledStats.Add("Defense", 1);
        multipledStats.Add("Speed", 1);
    }
}
