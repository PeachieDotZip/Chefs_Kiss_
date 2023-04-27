using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    public void SpawnEnemy()
    {
        enemy.SetActive(true);
        enemy.transform.position = transform.position;
    }
    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
