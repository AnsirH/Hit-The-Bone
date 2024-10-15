using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Monster : EntityBase
{
    public SplineAnimate splineAnimate;

    int hitCount = 0;

    private void Awake()
    {
        stat = new MonsterStats(10, 10, 1);
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
        // 스탯 초기화
        stat = new MonsterStats(10, 10, 1);

        // Spline Animate 초기화
        splineAnimate.MaxSpeed = stat.GetStat("Speed");
        splineAnimate.ElapsedTime = 0.0f;
        if (splineAnimate.Container != null && !splineAnimate.IsPlaying)
        {
            splineAnimate.Play();
        }
    }

    public void Hit(EntityBase sender)
    {
        gameObject.SetActive(false);
    }
}
