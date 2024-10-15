using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MonsterSpawner : MonoBehaviour
{
    public SplineContainer spline;

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
            Monster monster = ObjectPooler.Instance.SpawnFromPool<Monster>("Monster", Vector3.zero);
            monster.splineAnimate.Container = spline;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
