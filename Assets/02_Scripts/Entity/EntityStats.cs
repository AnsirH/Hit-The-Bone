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
        // 기본 스탯
        protected Dictionary<string, float> defaultStats = new Dictionary<string, float>();
        // 증감 스탯
        protected Dictionary<string, float> addedStats = new Dictionary<string, float>();
        // 퍼센트 증감 스탯
        protected Dictionary<string, float> multipledStats = new Dictionary<string, float>();

        // 버프 데이터 리스트
        protected SortedDictionary<float, BuffData> priorityEndBuff;

        // 도드 데미지 데이터 리스트
        protected SortedDictionary<float, DotDamageData> priorityEndDotDamage;

        // 생성자로 스탯의 종류를 받지 말고 그냥 스탯 종류를 추가할 수 있는 함수를 만들어 추가할 것
        public EntityStats()
        {
            priorityEndBuff = new SortedDictionary<float, BuffData>();
            priorityEndDotDamage = new SortedDictionary<float, DotDamageData>();
        }

        /// <summary>
        /// 버프 생성
        /// </summary>
        /// <param name="duration">지속 시간</param>
        /// <param name="sender">시전자</param>
        /// <param name="receiver">대상자</param>
        /// <param name="statType">적용 스탯</param>
        /// <param name="isDotBuff">도트형 버프인지 확인</param>
        /// <param name="isPercentBuff">퍼센트 버프인지 확인</param>
        /// <param name="value">버프 값</param>
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
        /// 적용된 버프를 확인하고 지속시간이 지난 버프는 해제한다.
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