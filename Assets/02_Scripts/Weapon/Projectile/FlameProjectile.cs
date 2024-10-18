using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameProjectile : Projectile
{
    public float flameRange = 2f;

    [SerializeField]
    LayerMask tileLayer;

    protected override void CollisionAction()
    {
        Collider[] tileColliders = Physics.OverlapSphere(transform.position, flameRange, tileLayer);

        foreach (Collider col in tileColliders)
        {
            if (col.CompareTag("WaypointTile"))
            {
                // 화염 지대 생성
                FlameAreaTile flameTile = ObjectPooler.Instance.SpawnFromPool<FlameAreaTile>("FlameAreaTile", col.transform.Find("Interaction Target Point").position);
                // 생성 지속 시간
                flameTile.StartEffect(sender, 5f);
                // 발생자( sender )
                // 설정 가능하도록 구현
            }
        }
    }
}
