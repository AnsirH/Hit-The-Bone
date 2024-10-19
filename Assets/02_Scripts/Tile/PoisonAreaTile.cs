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
                // 몬스터 stat에 접근해서 상태이상 추가하는 코드 작성하기
                // 만약 이미 상태이상에 걸려있는 상태라면 기존 상태이상 삭제하고 새로 적용하기
                // 상태이상은 sender, 효과 오브젝트를 넘겨준다. 그래서 특정 스킬이 데미지를 주고 있는건지 확인할 수 있게 한다.
            }

            yield return new WaitForSeconds(effectApplyTerm);
        }

        gameObject.SetActive(false);
    }
}
