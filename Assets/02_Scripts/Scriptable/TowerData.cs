using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName ="Create New Entity/TowerData")]
public class TowerData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public float Attack;
    public float AttackSpeed;
    public float Range;
}
