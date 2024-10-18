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
                // ȭ�� ���� ����
                FlameAreaTile flameTile = ObjectPooler.Instance.SpawnFromPool<FlameAreaTile>("FlameAreaTile", col.transform.Find("Interaction Target Point").position);
                // ���� ���� �ð�
                flameTile.StartEffect(sender, 5f);
                // �߻���( sender )
                // ���� �����ϵ��� ����
            }
        }
    }
}
