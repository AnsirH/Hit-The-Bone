using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAreaTile : EffectiveAreaTile
{
    public float effectApplyTerm = 0.75f;
    public float damageRatio = 0.05f;

    [SerializeField]
    LayerMask monsterLayer;
    protected override IEnumerator EffectCoroutine(EntityBase owner, float duration)
    {
        Tower ownerTower = owner as Tower;

        float timer = 0f;

        while (timer < duration)
        {
            timer += effectApplyTerm;

            Collider[] monsterCollider = Physics.OverlapBox(transform.position + Vector3.up * 0.5f, Vector3.one * 0.5f, Quaternion.identity, monsterLayer);

            foreach (Collider collider in monsterCollider)
            {
                Monster monster = ComponentManager.Instance.ManagedEntities.GetEntity(collider.gameObject) as Monster;
                monster.Stats.ApplyDot(ownerTower.gameObject.GetInstanceID(), ownerTower.Stat.GetStat("Attack") * damageRatio, 3f, 0.5f);
                // ���� stat�� �����ؼ� �����̻� �߰��ϴ� �ڵ� �ۼ��ϱ�
                // ���� �̹� �����̻� �ɷ��ִ� ���¶�� ���� �����̻� �����ϰ� ���� �����ϱ�
                // �����̻��� sender, ȿ�� ������Ʈ�� �Ѱ��ش�. �׷��� Ư�� ��ų�� �������� �ְ� �ִ°��� Ȯ���� �� �ְ� �Ѵ�.
            }

            yield return new WaitForSeconds(effectApplyTerm);
        }

        gameObject.SetActive(false);
    }
}
