using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AlgorithmGeneration
{
    public int[,] Map { get { return map; } }

    private int[,] map;

    private int x;
    private int y;

    public int X { get { return x; } }

    private int startIndex;

    private int endIndex;

    public int Y { get { return y; } }


    public AlgorithmGeneration()
    {
        x = Random.Range(4, 9);
        y = Random.Range(6, 15);
        map = new int[x, y];
    }

    public void SetPath()
    {
        startIndex = Random.Range(0, x-1);
        endIndex = startIndex;
        for(int i = 0; i < x; ++i)
        {
            for(int j = 0;j<y;++j)
                   Map[startIndex, j] = 1;
        }
    }
}
