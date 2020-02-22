using System;
using System.Collections.Generic;
using UnityEngine;

public class GenerationContoller : MonoBehaviour
{

    [SerializeField] private TileList chunkList;
    [SerializeField] private int sizeOfBlockX;
    [SerializeField] private int sizeOfBlockY;

    void Start()
    { 
        AlgorithmGeneration algorithmGeneration = new AlgorithmGeneration();

        algorithmGeneration.SetPath();
        int[,] levelMap = algorithmGeneration.Map;
        Debug.Log(algorithmGeneration.X);
        List<int> ksd = new List<int>();
        Debug.Log(algorithmGeneration.Y);
        for(int i=0;i< algorithmGeneration.X; ++i)
        {
            for (int j = 0; j < algorithmGeneration.Y; ++j)
            {
                if (levelMap[i, j] != 0)
                {
                    Instantiate(chunkList.GetValue(levelMap[i, j]), new Vector2(i * sizeOfBlockX, j * sizeOfBlockY), new Quaternion());
                }
            }
        }
    }
}

