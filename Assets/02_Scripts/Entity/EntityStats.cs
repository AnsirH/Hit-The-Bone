using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    public struct BuffData
    {
        public string sender;
        public string receiver;
        public string stat;
        public bool isPercentBuff;
        public float value;
        
        public void SetBuffData(string sender, string receiver, string stat, bool isPercentBuff, float value)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.stat = stat;
            this.isPercentBuff = isPercentBuff;
            this.value = value;
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

        // ���� ����Ʈ
        protected SortedDictionary<float, BuffData> priorityEndBuff;

        // �����ڷ� ������ ������ ���� ���� �׳� ���� ������ �߰��� �� �ִ� �Լ��� ����� �߰��� ��
        public EntityStats()
        {
            priorityEndBuff = new SortedDictionary<float, BuffData>();
        }

        /// <summary>
        /// ���� ����
        /// </summary>
        /// <param name="duration">���� �ð�</param>
        /// <param name="sender">������</param>
        /// <param name="receiver">�����</param>
        /// <param name="stat">���� ����</param>
        /// <param name="isPercentBuff">�ۼ�Ʈ �������� Ȯ��</param>
        /// <param name="value">���� ��</param>
        public void AddBuff(float duration, string sender, string receiver, string stat, bool isPercentBuff, float value) 
        {
            if (isPercentBuff)
            {
                MultipleStat(stat, value);
            }
            else
            {
                AddStat(stat, value);
            }

            if (duration <= 0)
            {
                return;
            }
            BuffData buff = new BuffData();
            buff.SetBuffData(sender, receiver, stat, isPercentBuff, value);
            priorityEndBuff.Add(Time.time + duration, buff);
        }


        private void AddStat(string stat, float value)
        {
            addedStats[stat] += value;
        }

        private void MultipleStat(string stat, float value)
        {
            multipledStats[stat] *= value;
        }

        private void EndBuff(BuffData buffData)
        {
            if (buffData.isPercentBuff)
            {
                MultipleStat(buffData.stat, 1 / buffData.value);
            }
            else
            {
                AddStat(buffData.stat, -buffData.value);
            }
        }

        /// <summary>
        /// ����� ������ Ȯ���ϰ� ���ӽð��� ���� ������ �����Ѵ�.
        /// </summary>
        public void CheckBuffDuration()
        {
            if (priorityEndBuff.Count == 0)
            {
                return;
            }

            foreach (KeyValuePair<float, BuffData> entity in priorityEndBuff)
            {
                if (entity.Key >= Time.time)
                {
                    EndBuff(entity.Value);
                    priorityEndBuff.Remove(entity.Key);
                }
            }
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