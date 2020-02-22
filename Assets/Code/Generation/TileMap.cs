using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMap : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private List<EnemySpawn> enemySpawns;
    [SerializeField] float x;
    [SerializeField] float y;

    

    public Tilemap Had()
    {
        return tilemap;
    }

    public float X()
    {
        return x;
    }

    public float Y()
    {
        return y;
    }

    public void Spawn()
    {
        foreach(EnemySpawn spawn in enemySpawns)
        {
            spawn.Spawn();
        }
    }
}
