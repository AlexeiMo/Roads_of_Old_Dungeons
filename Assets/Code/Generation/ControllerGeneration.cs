using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ControllerGeneration : MonoBehaviour
{

    [SerializeField] List<TileMap> tilemaps;
    [SerializeField] Transform startLevel;
    [SerializeField] TileMap endLevel;



    void Start()
    {
        float x = startLevel.position.x;
        float y = startLevel.position.y;
        int max = Random.Range(2, 5);
        TileMap tile;
        int range = Random.Range(3, Random.Range(3, 100));
        for (int i = 0; i < 7; i++)
        {
            int e = Random.Range(0, tilemaps.Count);
            tile = Instantiate(tilemaps[e], new Vector2(x, y), new Quaternion());
           // tile.Spawn();
            x += tilemaps[e].X();
            y += tilemaps[e].Y();
        }
        tile = Instantiate(endLevel, new Vector2(x, y), new Quaternion());
    }
}
