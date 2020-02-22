using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    [SerializeField] private int offsetX = 0;
    [SerializeField] private int offsetY=0;

    [SerializeField] private AtributeOfChunk atribute;

    public int Atribute
    {
        get
        {
            return (int)atribute;
        }
    }


    [SerializeField] private List<EnemySpawn> enemySpawns;

    public int OffsetX { get; }

    public int OffsetY { get; }




    // Start is called before the first frame update       
    private void Start()
    {
         foreach(EnemySpawn enemySpawn in enemySpawns)
        {
            enemySpawn.Spawn();
        }
    }

    //Типы чанков
    public enum AtributeOfChunk
    {
        EmptyChunk = 0, Start = 1, Finish = 2, HorizontalStraightChunk = 3, VerticalStraightChuk = 4,
        UpTurnLeftChunk = 5, DownTurnLeftChunk = 6, UpTurnRightChunk = 7, DownTurnRightChunk = 8, StartUp = 9, StartDown = 10
    }
}
