using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MonsterSpawner : MonoBehaviour
{
    public SplineContainer spline;

    public string spawnMonsterName;

    public float spawnDelay = 0.1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpawnMonsters(10));
        }
    }

    IEnumerator SpawnMonsters(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject monsterObj = ObjectPooler.Instance.SpawnFromPool(spawnMonsterName, Vector3.zero);
            Monster monster = EntityManager.Instance.GetEntity(monsterObj) as Monster;
            monster.splineAnimate.Container = spline;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
