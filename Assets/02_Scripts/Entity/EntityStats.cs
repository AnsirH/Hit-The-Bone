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
        // 기본 스탯
        protected Dictionary<string, float> defaultStats = new Dictionary<string, float>();
        // 증감 스탯
        protected Dictionary<string, float> addedStats = new Dictionary<string, float>();
        // 퍼센트 증감 스탯
        protected Dictionary<string, float> multipledStats = new Dictionary<string, float>();

        // 버프 리스트
        protected SortedDictionary<float, BuffData> priorityEndBuff;

        // 생성자로 스탯의 종류를 받지 말고 그냥 스탯 종류를 추가할 수 있는 함수를 만들어 추가할 것
        public EntityStats()
        {
            priorityEndBuff = new SortedDictionary<float, BuffData>();
        }

        /// <summary>
        /// 버프 생성
        /// </summary>
        /// <param name="duration">지속 시간</param>
        /// <param name="sender">시전자</param>
        /// <param name="receiver">대상자</param>
        /// <param name="stat">적용 스탯</param>
        /// <param name="isPercentBuff">퍼센트 버프인지 확인</param>
        /// <param name="value">버프 값</param>
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
        /// 적용된 버프를 확인하고 지속시간이 지난 버프는 해제한다.
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