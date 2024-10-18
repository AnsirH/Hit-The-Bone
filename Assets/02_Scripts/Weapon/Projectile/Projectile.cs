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
            Collider[] colliders = Physics.OverlapSphere(transform.position, coll.radius, monsterLayer);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Monster") && receiver == EntityManager.Instance.GetEntity(collider.gameObject))
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

    protected virtual void CollisionAction()
    {
        receiver.Hit(sender);
    }
}
