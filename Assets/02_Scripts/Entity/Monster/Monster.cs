using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class Monster : EntityBase
{
    public SplineAnimate splineAnimate;

    [SerializeField]
    MonsterData data;

    private float dotDamageTickTimer = 0;

    private void Awake()
    {
        stat = new MonsterStats(data.Hp, data.Defense, data.Speed);
        if (splineAnimate.Container == null)
        {
            splineAnimate.enabled = false;
        }
        else
        {
            splineAnimate.enabled = true;
        }
    }
    // Update is called once per frame 업데이는 프레임 하나당 한 번 호출됩니다.
    protected override void Update()
    {
        base.Update();
        if (splineAnimate.NormalizedTime >= 1.0f)
        {
            gameObject.SetActive(false);
        }

        if (dotDamageTickTimer >= 0.5f)
        {
            stat.AddBuff(0, null, this, "Hp", false, stat.GetTotalDotDamage());
            //Hit()
            dotDamageTickTimer = 0;
        }
        else
        {
            dotDamageTickTimer += Time.deltaTime;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        splineAnimate.enabled = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        splineAnimate.enabled = true;
        splineAnimate.MaxSpeed = stat.GetStat("Speed");
        Debug.Log("몬스터 스피드 설정 : " + stat.GetStat("Speed"));
    }

    public override void Init()
    {
        base.Init();
        // 스탯 초기화 해버려
        stat = new MonsterStats(data.Hp, data.Defense, data.Speed);

        // Spline Animate 초기화 해버려
        splineAnimate.MaxSpeed = stat.GetStat("Speed");
        splineAnimate.ElapsedTime = 0.0f;
        if (splineAnimate.Container != null && !splineAnimate.IsPlaying)
        {
            splineAnimate.Play();
        }
    }

    public void Hit(Tower sender)
    {
        float damage = sender.Stat.GetStat("Attack") - stat.GetStat("Defense");

        if (damage < 0) damage = 1;

        stat.AddBuff(0, sender, this, "Hp", false, -damage);
        Debug.Log($"{gameObject.name}이 {sender.name}로부터 데미지 {damage}를 받았습니다.");

        if (stat.GetStat("Hp") <= 0)
        {
            Die();
        }
    }

    public void Hit(Tower sender, float ratio)
    {
        float damage = sender.Stat.GetStat("Attack") * ratio - stat.GetStat("Defense");

        if (damage < 0) damage = 1;

        stat.AddBuff(0, sender, this, "Hp", false, -damage);
        Debug.Log($"{gameObject.name}이 {sender.name}로부터 데미지 {damage}를 받았습니다.");

        if (stat.GetStat("Hp") <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
