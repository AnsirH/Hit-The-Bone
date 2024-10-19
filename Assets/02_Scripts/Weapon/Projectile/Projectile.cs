using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Properties;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Tower sender;
    public Monster receiver;

    [SerializeField]
    SphereCollider coll;
    [SerializeField]
    LayerMask monsterLayer;

    public float speed = 20f;
    // 공격자, 피격자
    // 투사체가 피격자에 도달하면 공격자의 스탯을 적용한 데미지를 피격자에게 가한다.

    private void OnEnable()
    {
        ProjectileManager.Instance.AddEntity(this);
        sender = null;
        receiver = null;
    }

    private void OnDisable()
    {
        ProjectileManager.Instance.RemoveEntity(this);

        ObjectPooler.Instance.ReturnToPool(gameObject);
        CancelInvoke();
    }

    private void Update()
    {
        if (sender == null || receiver == null)
        {
            return;
        }


        Vector3 direction = (receiver.transform.position - transform.position).normalized;

        transform.Translate(direction * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, receiver.transform.position) <= 0.1f)
        {
            if (receiver.gameObject.activeSelf == false)
            {
                gameObject.SetActive(false);
            }

            Monster[] collidedMonsters = ComponentManager.Instance.ManagedEntities.GetAroundMonsters(transform, coll.radius);

            if (collidedMonsters.Length == 0)
            {
                return;
            }
            foreach (Monster monster in collidedMonsters)
            {
                if (receiver == monster)
                {
                    CollisionAction();
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetData(Tower sender, Monster receiver)
    {
        this.sender = sender;
        this.receiver = receiver;
    }

    /// <summary>
    /// Projectile이 적과 충돌 시 동작할 액션을 정의합니다.
    /// </summary>
    protected virtual void CollisionAction()
    {
        receiver.Hit(sender);
    }
}