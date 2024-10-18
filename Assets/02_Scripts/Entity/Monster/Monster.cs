using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Monster : EntityBase
{
    public SplineAnimate splineAnimate;

    [SerializeField]
    MonsterData data;

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
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (splineAnimate.NormalizedTime >= 1.0f)
        {
            gameObject.SetActive(false);
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

    }

    public override void Init()
    {
        base.Init();
        // ���� �ʱ�ȭ
        stat = new MonsterStats(10, 10, 1);

        // Spline Animate �ʱ�ȭ
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

        stat.AddBuff(0, sender.name, name, "Hp", false, -damage);
        Debug.Log($"{gameObject.name}�� {sender.name}�κ��� ������ {damage}�� �޾ҽ��ϴ�.");

        if (stat.GetStat("Hp") <= 0)
        {
            Die();
        }
    }

    public void Hit(Tower sender, float ratio)
    {
        float damage = sender.Stat.GetStat("Attack") * ratio - stat.GetStat("Defense");

        if (damage < 0) damage = 1;

        stat.AddBuff(0, sender.name, name, "Hp", false, -damage);
        Debug.Log($"{gameObject.name}�� {sender.name}�κ��� ������ {damage}�� �޾ҽ��ϴ�.");

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
