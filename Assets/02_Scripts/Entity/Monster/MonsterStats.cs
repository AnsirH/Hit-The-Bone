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
        public float duration;    // ���� DOT ���� �ð�
        public float interval;    // �������� ����Ǵ� ����
        public float damage;      // 1ȸ ������
        public float timeSinceLastTick;  // ������ ������ ƽ ���� ����� �ð�
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
                Debug.Log("��Ʈ ������ �� " + -effect.damage);
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

        // ����� ��Ʈ ȿ�� ����
        foreach (var towerId in towersToRemove)
        {
            activeDotEffects.Remove(towerId);
        }
    }

    public void ApplyDot(int towerId, float damage, float duration, float interval)
    {
        if (activeDotEffects.ContainsKey(towerId))
        {
            // ���� Ÿ���� �ٽ� ������ ��� ���� �ð��� �ʱ�ȭ
            activeDotEffects[towerId].duration = duration;
        }
        else
        {
            // ���� ��Ʈ ������ ����
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
