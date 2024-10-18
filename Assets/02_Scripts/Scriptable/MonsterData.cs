using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Create New Entity/MonsterData")]
public class MonsterData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public float Hp;
    public float Defense;
    public float Speed;
}
