using System;
using System.Collections.Generic;
using UnityEngine;

public class TileList : MonoBehaviour
{
    [SerializeField] private List<TileInfo> listTile;
    private List<List<TileInfo>> tileInfos;

    private void Awake()
    {
        //Создание списков доступных чанков в зависимоти от их типа
        Debug.Log("SizeOftribute" + Enum.GetValues(typeof(TileInfo.AtributeOfChunk)).Length);
        tileInfos = new List<List<TileInfo>>();
        for(int i=0;i< Enum.GetValues(typeof(TileInfo.AtributeOfChunk)).Length; i++)
        {
            tileInfos.Add(new List<TileInfo>());
        }
        Debug.Log(tileInfos.Count);
        foreach (TileInfo elem in listTile)
        {
            
            tileInfos[elem.Atribute].Add(elem);
        }
    }
    public TileInfo GetValue(int i)
    {
       // Debug.Log(i);
        Debug.Log("Atribute"+tileInfos[i][0].Atribute);
      
        return tileInfos[i][UnityEngine.Random.Range(0,tileInfos[i].Count-1)];
    }
}
