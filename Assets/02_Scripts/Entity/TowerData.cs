using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName ="Create New Entity/TowerData")]
public class TowerData : ScriptableObject
{
    public Sprite icon;
    public float attack;
    public float attackSpeed;
    public float range;
}
