using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerSkillData", menuName = "Create New Skill Data/TowerSkillData")]
public class TowerSkillData : ScriptableObject
{
    public enum SkillType
    {
        None,
        Damage,
        CC,
        Buff
    }
    public SkillType skillType;
    public float skillRatio;
}
