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
    // ������, �ǰ���
    // ����ü�� �ǰ��ڿ� �����ϸ� �������� ������ ������ �������� �ǰ��ڿ��� ���Ѵ�.

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
    /// Projectile�� ���� �浹 �� ������ �׼��� �����մϴ�.
    /// </summary>
    protected virtual void CollisionAction()
    {
        receiver.Hit(sender);
    }
}