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

    private class DotEffect
    {
        public float duration;    // 남은 DOT 지속 시간
        public float interval;    // 데미지가 적용되는 간격
        public float damage;      // 1회 데미지
        public float timeSinceLastTick;  // 마지막 데미지 틱 이후 경과한 시간
    }

    private Dictionary<int, DotEffect> activeDotEffects = new Dictionary<int, DotEffect>();

    public void CheckDotEffects(Monster owner)
    {
        List<int> towersToRemove = new List<int>();

        foreach (var kvp in activeDotEffects)
        {
            var towerId = kvp.Key;
            var effect = kvp.Value;

            effect.duration -= Time.deltaTime;
            effect.timeSinceLastTick += Time.deltaTime;

            if (effect.timeSinceLastTick >= effect.interval)
            {
                Debug.Log("도트 데미지 들어감 " + -effect.damage);
                AddBuff(0, null, null, "Hp", false, -effect.damage);

                if (GetStat("Hp") <= 0)
                {
                    owner.Die();
                }

                effect.timeSinceLastTick = 0f;
            }

            if (effect.duration <= 0)
            {
                towersToRemove.Add(towerId);
            }
        }

        // 만료된 도트 효과 제거
        foreach (var towerId in towersToRemove)
        {
            activeDotEffects.Remove(towerId);
        }
    }

    public void ApplyDot(int towerId, float damage, float duration, float interval)
    {
        if (activeDotEffects.ContainsKey(towerId))
        {
            // 같은 타워가 다시 공격한 경우 지속 시간을 초기화
            activeDotEffects[towerId].duration = duration;
        }
        else
        {
            // 새로 도트 데미지 적용
            activeDotEffects.Add(towerId, new DotEffect
            {
                duration = duration,
                interval = interval,
                damage = damage,
                timeSinceLastTick = 0f
            });
        }
    }
}
