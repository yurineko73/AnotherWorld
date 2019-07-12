using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
    public static MapController Instance = null;
    void Awake()
    {
        Instance = this;
    }

    public GameObject maptilePre;
    public List<Piece> mapList = new List<Piece>();
	
	void Start () {
        InitMap();
	}

    private void InitMap()
    {
        int[] maps = new int[]
        {
            1,0,1,
            0,1,0,
            1,0,1,
        };
        for (int k = 0; k < maps.Length; k++)
        {
            var go = Instantiate(maptilePre).GetComponent<Piece>();
            go.Init(maps[k], k);
            mapList.Add(go);
        }
    }

    public void LinkTurn(int index)
    {
        var origin = FindPiece(index);
        if (null == origin) print("没找到对应索引的地图块！");
        origin.TurnSelf();
        Vector2Int tp = new Vector2Int(index % GlobalValue.MAP_WIDTH, index / GlobalValue.MAP_WIDTH);
        for (int i = 0; i < GlobalValue.MAP_WIDTH; i++)
        {
            var temp = FindPiece(i * GlobalValue.MAP_WIDTH + tp.x);
            temp.TurnSelf();
            temp = FindPiece(i + tp.y * GlobalValue.MAP_WIDTH);
            temp.TurnSelf();
        }

    }
    //根据索引从地图列表里找到对应块
    private Piece FindPiece(int index)
    {
        foreach (var p in mapList)
            if (p.index == index) return p;
        return null;
    }
}
