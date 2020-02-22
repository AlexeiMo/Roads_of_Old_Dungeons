using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies;
    public void Spawn()
    {
     Enemy enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, new Quaternion());
    }

    private void Update()
    {
        Enemy enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, new Quaternion());
        Destroy(gameObject);
    }

}
