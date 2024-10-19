using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Stats
{
    public struct BuffData
    {
        EntityBase sender;
        EntityBase receiver;
        string statType;
        bool isPercentBuff;
        float value;

        public EntityBase Sender => sender;
        public EntityBase Receiver => receiver;
        public string StatType => statType;
        public bool IsPercentBuff => isPercentBuff;
        public float Value => value;

        public void SetBuffData(EntityBase sender, EntityBase receiver, string statType, bool isPercentBuff, float value)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.statType = statType;
            this.isPercentBuff = isPercentBuff;
            this.value = value;
        }
    }

    public struct DotDamageData
    {
        EntityBase sender;
        EntityBase receiver;
        GameObject medium;
        float ratio;

        public EntityBase Sender => sender;
        public EntityBase Receiver => receiver;
        public GameObject Medium => medium;
        public float Ratio => ratio; 

        public void SetData(EntityBase sender, EntityBase receiver, GameObject medium, float ratio)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.medium = medium;
            this.ratio = ratio;
        }
    }

    public class EntityStats
    {
        // �⺻ ����
        protected Dictionary<string, float> defaultStats = new Dictionary<string, float>();
        // ���� ����
        protected Dictionary<string, float> addedStats = new Dictionary<string, float>();
        // �ۼ�Ʈ ���� ����
        protected Dictionary<string, float> multipledStats = new Dictionary<string, float>();

        // ���� ������ ����Ʈ
        protected SortedDictionary<float, BuffData> priorityEndBuff;

        // ���� ������ ������ ����Ʈ
        protected SortedDictionary<float, DotDamageData> priorityEndDotDamage;

        // �����ڷ� ������ ������ ���� ���� �׳� ���� ������ �߰��� �� �ִ� �Լ��� ����� �߰��� ��
        public EntityStats()
        {
            priorityEndBuff = new SortedDictionary<float, BuffData>();
            priorityEndDotDamage = new SortedDictionary<float, DotDamageData>();
        }

        /// <summary>
        /// ���� ����
        /// </summary>
        /// <param name="duration">���� �ð�</param>
        /// <param name="sender">������</param>
        /// <param name="receiver">�����</param>
        /// <param name="statType">���� ����</param>
        /// <param name="isDotBuff">��Ʈ�� �������� Ȯ��</param>
        /// <param name="isPercentBuff">�ۼ�Ʈ �������� Ȯ��</param>
        /// <param name="value">���� ��</param>
        public void AddBuff(float duration, EntityBase sender, EntityBase receiver, string statType, bool isPercentBuff, float value) 
        {
            if (isPercentBuff)
            {
                MultipleStat(statType, value);
            }
            else
            {
                AddStat(statType, value);
            }

            if (duration <= 0)
            {
                return;
            }
            BuffData buff = new BuffData();
            buff.SetBuffData(sender, receiver, statType, isPercentBuff, value);
            priorityEndBuff.Add(Time.time + duration, buff);
        }

        private void EndBuff(BuffData buffData)
        {
            if (buffData.IsPercentBuff)
            {
                MultipleStat(buffData.StatType, 1 / buffData.Value);
            }
            else
            {
                AddStat(buffData.StatType, -buffData.Value);
            }
        }

        public void AddDotDamage(float duration, EntityBase sender, EntityBase receiver, GameObject medium, float ratio)
        {
            if (duration <= 0)
            {
                return;
            }
            foreach(DotDamageData dotDamageData in priorityEndDotDamage.Values)
            {
                if (dotDamageData.Sender == sender && dotDamageData.Medium == medium)
                {
                    return;
                }
            }

            DotDamageData dotDamage = new DotDamageData();
            dotDamage.SetData(sender, receiver, medium, ratio);
            priorityEndDotDamage.Add(Time.time + duration, dotDamage);
        }

        private void EndDotDamage(DotDamageData dotDamageData)
        {

        }


        private void AddStat(string stat, float value)
        {
            addedStats[stat] += value;
        }

        private void MultipleStat(string stat, float value)
        {
            multipledStats[stat] *= value;
        }

        /// <summary>
        /// ����� ������ Ȯ���ϰ� ���ӽð��� ���� ������ �����Ѵ�.
        /// </summary>
        public void CheckBuffDuration()
        {
            if (priorityEndBuff.Count > 0)
            {
                foreach (KeyValuePair<float, BuffData> entity in priorityEndBuff)
                {
                    if (entity.Key >= Time.time)
                    {
                        EndBuff(entity.Value);
                        priorityEndBuff.Remove(entity.Key);
                    }
                }
            }

            if (priorityEndDotDamage.Count > 0)
            {
                foreach (KeyValuePair<float, DotDamageData> entity in priorityEndDotDamage)
                {
                    if (entity.Key >= Time.time)
                    {
                        EndDotDamage(entity.Value);
                        priorityEndBuff.Remove(entity.Key);
                    }
                }
            }
        }

        public float GetTotalDotDamage()
        {
            float totalDotDamage = 0f;
            if (priorityEndDotDamage.Count > 0)
            {
                foreach(DotDamageData dotDamageData in priorityEndDotDamage.Values)
                {
                    totalDotDamage += dotDamageData.Sender.Stat.GetStat("Attack") * dotDamageData.Ratio;
                }
            }

            return totalDotDamage;
        }

        public float GetStat(string stat)
        {
            float defaultStat = defaultStats[stat];
            float addedStat = addedStats[stat];
            float multipledStat = multipledStats[stat];

            return defaultStat * multipledStat + addedStat;
        }
    }
}